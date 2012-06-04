namespace Roster
{
    partial class Roster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Roster));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.instructorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schoolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.periodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coursesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enrollmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentEnrollmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enrollmentTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.placesToolStripMenuItem,
            this.thingsToolStripMenuItem,
            this.rollsToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.txtSearch});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(973, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.backupSystemToolStripMenuItem,
            this.restoreDataToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // backupSystemToolStripMenuItem
            // 
            this.backupSystemToolStripMenuItem.Name = "backupSystemToolStripMenuItem";
            this.backupSystemToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.backupSystemToolStripMenuItem.Text = "Backup Data";
            this.backupSystemToolStripMenuItem.Click += new System.EventHandler(this.backupSystemToolStripMenuItem_Click);
            // 
            // restoreDataToolStripMenuItem
            // 
            this.restoreDataToolStripMenuItem.Name = "restoreDataToolStripMenuItem";
            this.restoreDataToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.restoreDataToolStripMenuItem.Text = "Restore Data";
            this.restoreDataToolStripMenuItem.Click += new System.EventHandler(this.restoreDataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instructorsToolStripMenuItem,
            this.studentsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(55, 23);
            this.toolStripMenuItem1.Text = "People";
            // 
            // instructorsToolStripMenuItem
            // 
            this.instructorsToolStripMenuItem.Name = "instructorsToolStripMenuItem";
            this.instructorsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.instructorsToolStripMenuItem.Text = "Instructors";
            this.instructorsToolStripMenuItem.Click += new System.EventHandler(this.instructorsToolStripMenuItem_Click);
            // 
            // studentsToolStripMenuItem
            // 
            this.studentsToolStripMenuItem.Name = "studentsToolStripMenuItem";
            this.studentsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.studentsToolStripMenuItem.Text = "Students";
            this.studentsToolStripMenuItem.Click += new System.EventHandler(this.studentsToolStripMenuItem_Click);
            // 
            // placesToolStripMenuItem
            // 
            this.placesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schoolsToolStripMenuItem,
            this.classRoomsToolStripMenuItem});
            this.placesToolStripMenuItem.Name = "placesToolStripMenuItem";
            this.placesToolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.placesToolStripMenuItem.Text = "Places";
            // 
            // schoolsToolStripMenuItem
            // 
            this.schoolsToolStripMenuItem.Name = "schoolsToolStripMenuItem";
            this.schoolsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.schoolsToolStripMenuItem.Text = "Schools";
            this.schoolsToolStripMenuItem.Click += new System.EventHandler(this.schoolsToolStripMenuItem_Click);
            // 
            // classRoomsToolStripMenuItem
            // 
            this.classRoomsToolStripMenuItem.Name = "classRoomsToolStripMenuItem";
            this.classRoomsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.classRoomsToolStripMenuItem.Text = "Class Rooms";
            this.classRoomsToolStripMenuItem.Click += new System.EventHandler(this.classRoomsToolStripMenuItem_Click);
            // 
            // thingsToolStripMenuItem
            // 
            this.thingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sessionsToolStripMenuItem,
            this.periodsToolStripMenuItem,
            this.coursesToolStripMenuItem,
            this.classesToolStripMenuItem,
            this.enrollmentsToolStripMenuItem,
            this.studentEnrollmentsToolStripMenuItem,
            this.enrollmentTagsToolStripMenuItem});
            this.thingsToolStripMenuItem.Name = "thingsToolStripMenuItem";
            this.thingsToolStripMenuItem.Size = new System.Drawing.Size(55, 23);
            this.thingsToolStripMenuItem.Text = "Things";
            // 
            // sessionsToolStripMenuItem
            // 
            this.sessionsToolStripMenuItem.Name = "sessionsToolStripMenuItem";
            this.sessionsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.sessionsToolStripMenuItem.Text = "Sessions";
            this.sessionsToolStripMenuItem.Click += new System.EventHandler(this.sessionsToolStripMenuItem_Click);
            // 
            // periodsToolStripMenuItem
            // 
            this.periodsToolStripMenuItem.Name = "periodsToolStripMenuItem";
            this.periodsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.periodsToolStripMenuItem.Text = "Periods";
            this.periodsToolStripMenuItem.Click += new System.EventHandler(this.periodsToolStripMenuItem_Click);
            // 
            // coursesToolStripMenuItem
            // 
            this.coursesToolStripMenuItem.Name = "coursesToolStripMenuItem";
            this.coursesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.coursesToolStripMenuItem.Text = "Courses";
            this.coursesToolStripMenuItem.Click += new System.EventHandler(this.coursesToolStripMenuItem_Click);
            // 
            // classesToolStripMenuItem
            // 
            this.classesToolStripMenuItem.Name = "classesToolStripMenuItem";
            this.classesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.classesToolStripMenuItem.Text = "Classes";
            this.classesToolStripMenuItem.Click += new System.EventHandler(this.classesToolStripMenuItem_Click);
            // 
            // enrollmentsToolStripMenuItem
            // 
            this.enrollmentsToolStripMenuItem.Name = "enrollmentsToolStripMenuItem";
            this.enrollmentsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.enrollmentsToolStripMenuItem.Text = "Enrollments By Class";
            this.enrollmentsToolStripMenuItem.Click += new System.EventHandler(this.enrollmentsToolStripMenuItem_Click);
            // 
            // studentEnrollmentsToolStripMenuItem
            // 
            this.studentEnrollmentsToolStripMenuItem.Name = "studentEnrollmentsToolStripMenuItem";
            this.studentEnrollmentsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.studentEnrollmentsToolStripMenuItem.Text = "Enrollments By Student";
            this.studentEnrollmentsToolStripMenuItem.Click += new System.EventHandler(this.studentEnrollmentsToolStripMenuItem_Click);
            // 
            // enrollmentTagsToolStripMenuItem
            // 
            this.enrollmentTagsToolStripMenuItem.Name = "enrollmentTagsToolStripMenuItem";
            this.enrollmentTagsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.enrollmentTagsToolStripMenuItem.Text = "Enrollment Tags";
            this.enrollmentTagsToolStripMenuItem.Click += new System.EventHandler(this.enrollmentTagsToolStripMenuItem_Click);
            // 
            // rollsToolStripMenuItem
            // 
            this.rollsToolStripMenuItem.Name = "rollsToolStripMenuItem";
            this.rollsToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.rollsToolStripMenuItem.Text = "Rolls";
            this.rollsToolStripMenuItem.Click += new System.EventHandler(this.rollsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtSearch.BackColor = System.Drawing.SystemColors.Info;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.Text = "Search...";
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dockPanel);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(973, 525);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(973, 552);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.BackColor = System.Drawing.Color.Transparent;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(973, 525);
            this.dockPanel.TabIndex = 0;
            this.dockPanel.ActiveContentChanged += new System.EventHandler(this.dockPanel_ActiveContentChanged);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sessionSummaryToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // sessionSummaryToolStripMenuItem
            // 
            this.sessionSummaryToolStripMenuItem.Name = "sessionSummaryToolStripMenuItem";
            this.sessionSummaryToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.sessionSummaryToolStripMenuItem.Text = "Session Summary";
            this.sessionSummaryToolStripMenuItem.Click += new System.EventHandler(this.sessionSummaryToolStripMenuItem_Click);
            // 
            // Roster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 552);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Roster";
            this.Text = "Roster";
            this.Load += new System.EventHandler(this.Roster_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Roster_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem instructorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem placesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schoolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sessionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem periodsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coursesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classRoomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enrollmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rollsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentEnrollmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enrollmentTagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sessionSummaryToolStripMenuItem;

    }
}