using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Roster.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Roster
{
    public partial class Roster : Form
    {
        public Roster()
        {
            InitializeComponent();
            Central.MainForm = this;
        }

        #region AddPanel
        public void AddPanel(DockContent form, DockState dockState, bool AllowMultiple)
        {
            DockContent joinBefore = null;
            DockPane joinPane = null;
            foreach (DockContent content in dockPanel.Contents)
            {
                if (content.GetType() == form.GetType() && !AllowMultiple)
                    return;
                if (content.GetType() == form.GetType() || (
                    content.DockState == DockState.Document && dockState == DockState.Document))
                {
                    joinBefore = content;
                    joinPane = content.Pane;
                }
            }
            form.DockPanel = dockPanel;
            form.DockState = dockState;
            if (joinPane != null)
                form.Show(joinPane, joinBefore);
            else
                form.Show();
        }

        public DockContentCollection Contents
        {
            get { return dockPanel.Contents; }
        }
        #endregion

        #region Persist Methods
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == "Roster.Forms.StudentDetails" || 
                persistString == "Roster.Forms.InstructorDetails" || 
                persistString.EndsWith("Details"))
                return null;
            object obj = Activator.CreateInstance(this.GetType().Assembly.FullName, persistString).Unwrap();
            return (IDockContent)obj;
        }

        private void Roster_Load(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                FileInfo config = new FileInfo(Central.SaveDir.FullName + "\\Dock.config");
                if (!config.Exists)
                    config.Create();
                else
                {
                    fs = config.Open(FileMode.Open, FileAccess.Read);
                    dockPanel.LoadFromXml(fs, GetContentFromPersistString, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        private void Roster_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileStream fs = null; ;
            try
            {
                FileInfo config = new FileInfo(Central.SaveDirPath + "\\Dock.config");
                if (!config.Exists)
                    config.Create();
                fs = config.Open(FileMode.Truncate, FileAccess.ReadWrite);
                dockPanel.SaveAsXml(fs, Encoding.Default);
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        private static T LoadObject<T>(string fileName)
        {
            FileInfo config = new FileInfo(Central.SaveDirPath + "\\" + fileName);
            T destination = default(T);
            if (!config.Exists)
                config.Create();

            using (FileStream fs = config.Open(FileMode.Open, FileAccess.Read))
            {
                if (fs.Length < 1)
                    return default(T);
                using (BinaryReader br = new BinaryReader(fs))
                {
                    try
                    {
                        byte[] arr = br.ReadBytes((Int32)fs.Length);
                        destination = (T)Deserializer(arr);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        br.Close();
                    }
                }
                fs.Close();
            }
            return destination;
        }

        private static void SaveObject(string fileName, object source)
        {
            try
            {
                FileInfo config = new FileInfo(Central.SaveDirPath + "\\" + fileName);
                if (!config.Exists)
                    config.Create();
                using (FileStream fs = config.Open(FileMode.Truncate, FileAccess.Write))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        try
                        {
                            byte[] arr = Serialize(source);
                            writer.Write(arr);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            writer.Close();
                        }
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static byte[] Serialize(object obj)
        {
            MemoryStream msClone = new MemoryStream();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(msClone, obj);
                msClone.Position = 0;
                return msClone.ToArray();
            }
            catch (SerializationException ex)
            {
                throw new SerializationException("[Error While Serializing] " + ex.Message, ex);
            }
        }

        public static object Deserializer(byte[] serializedObject)
        {
            object retval;
            try
            {
                MemoryStream ms = new MemoryStream(serializedObject);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Binder = new Base20To35Deserializer();
                formatter.SurrogateSelector = new Base20To35Deserializer();
                retval = formatter.Deserialize(ms, null);
            }
            catch (SerializationException ex)
            {
                throw new SerializationException("[Error While Deserializing] " + ex.Message, ex);
            }
            return retval;
        }

        sealed protected class Base20To35Deserializer : SerializationBinder, ISurrogateSelector
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                return null;
            }

            #region ISurrogateSelector Members

            public void ChainSelector(ISurrogateSelector selector)
            {

            }

            public ISurrogateSelector GetNextSelector()
            {
                return null;
            }

            public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
            {
                selector = null;
                return null;
            }

            #endregion
        }
        #endregion

        #region Search Box
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search...")
                txtSearch.Text = string.Empty;
            else
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
            }

        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
                txtSearch.Text = "Search...";
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
                ShowSearch(txtSearch.Text);
        }

        private void ShowSearch(string p)
        {
            if (dockPanel != null && dockPanel.ActiveDocument != null && dockPanel.ActiveDocument.SearchFilterTerm != null)
                dockPanel.ActiveDocument.SearchFilterTerm = p;
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Students(), DockState.Document, true);
        }

        private void instructorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Instructors(), DockState.Document, true);
        }

        private void schoolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Schools(), DockState.Document, true);
        }

        private void sessionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Sessions(), DockState.Document, true);
        }

        private void periodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Periods(), DockState.Document, true);
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Courses(), DockState.Document, true);
        }

        private void classRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new ClassRooms(), DockState.Document, true);
        }

        private void classesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Classes(), DockState.Document, true);
        }

        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Enrollments(), DockState.Document, false);
        }

        private void rollsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new Rolls(), DockState.Document, false);
        }

        private void studentEnrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new StudentEnrollments(), DockState.Document, false);
        }

        private void backupSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "sqlite";
            sfd.FileName = "Roster";
            sfd.Title = "Select Backup Location";
            DialogResult dr = sfd.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            File.Copy(Central.SaveDir.FullName + "\\Roster.sqlite", sfd.FileName, true);
        }

        private void restoreDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult warn = MessageBox.Show("This action will erase all your current data, would you like to continue?", "Warning", MessageBoxButtons.YesNo);
            if (warn != DialogResult.Yes)
                return;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "sqlite";
            ofd.FileName = "Roster";
            ofd.Title = "Select file to restore";
            DialogResult dr = ofd.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            File.Copy(ofd.FileName, Central.SaveDir.FullName + "\\Roster.sqlite", true);

        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            if(dockPanel != null && dockPanel.ActiveDocument != null && dockPanel.ActiveDocument.SearchFilterTerm != null)
                txtSearch.Text = dockPanel.ActiveDocument.SearchFilterTerm;
        }

        private void enrollmentTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new EnrollmentTags(), DockState.Document, false);
        }

        private void sessionSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new SessionEndReport(), DockState.Document, false);
        }
    }
}
