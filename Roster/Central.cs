using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Roster
{
    public static class Central
    {
        private static Roster m_MainForm;

        public static Roster MainForm
        {
            get { return Central.m_MainForm; }
            set { Central.m_MainForm = value; }
        }

        private static string m_SaveDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Roster";

        public static string SaveDirPath
        {
            get { return Central.m_SaveDirPath; }
        }

        private static DirectoryInfo m_SaveDir = new DirectoryInfo(SaveDirPath);

        public static DirectoryInfo SaveDir
        {
            get { InitSaveDir(); return Central.m_SaveDir; }
        }

        public static void InitSaveDir()
        {
            if (!Directory.Exists(SaveDirPath))
            {
                DirectoryInfo di = new DirectoryInfo(SaveDirPath);
                di.Create();
                di.Attributes |= FileAttributes.Hidden;

            }
        }
    }
}
