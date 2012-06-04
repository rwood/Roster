namespace Roster.Forms
{
    partial class ClassDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAsNew = new System.Windows.Forms.Button();
            this.ddlPeriod = new System.Windows.Forms.ComboBox();
            this.ddlSession = new System.Windows.Forms.ComboBox();
            this.ddlClassRoom = new System.Windows.Forms.ComboBox();
            this.ddlInstructor = new System.Windows.Forms.ComboBox();
            this.ddlCourse = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Instructor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Class Room:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Session:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Period:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 141);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(62, 141);
            this.btnSaveAsNew.Name = "btnSaveAsNew";
            this.btnSaveAsNew.Size = new System.Drawing.Size(90, 23);
            this.btnSaveAsNew.TabIndex = 12;
            this.btnSaveAsNew.Text = "Save As New";
            this.btnSaveAsNew.UseVisualStyleBackColor = true;
            this.btnSaveAsNew.Click += new System.EventHandler(this.btnSaveAsNew_Click);
            // 
            // ddlPeriod
            // 
            this.ddlPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPeriod.FormattingEnabled = true;
            this.ddlPeriod.Location = new System.Drawing.Point(108, 114);
            this.ddlPeriod.Name = "ddlPeriod";
            this.ddlPeriod.Size = new System.Drawing.Size(128, 21);
            this.ddlPeriod.TabIndex = 20;
            // 
            // ddlSession
            // 
            this.ddlSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSession.FormattingEnabled = true;
            this.ddlSession.Location = new System.Drawing.Point(108, 87);
            this.ddlSession.Name = "ddlSession";
            this.ddlSession.Size = new System.Drawing.Size(128, 21);
            this.ddlSession.TabIndex = 21;
            // 
            // ddlClassRoom
            // 
            this.ddlClassRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlClassRoom.FormattingEnabled = true;
            this.ddlClassRoom.Location = new System.Drawing.Point(108, 60);
            this.ddlClassRoom.Name = "ddlClassRoom";
            this.ddlClassRoom.Size = new System.Drawing.Size(128, 21);
            this.ddlClassRoom.TabIndex = 22;
            // 
            // ddlInstructor
            // 
            this.ddlInstructor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlInstructor.FormattingEnabled = true;
            this.ddlInstructor.Location = new System.Drawing.Point(108, 33);
            this.ddlInstructor.Name = "ddlInstructor";
            this.ddlInstructor.Size = new System.Drawing.Size(128, 21);
            this.ddlInstructor.TabIndex = 23;
            // 
            // ddlCourse
            // 
            this.ddlCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCourse.FormattingEnabled = true;
            this.ddlCourse.Location = new System.Drawing.Point(108, 6);
            this.ddlCourse.Name = "ddlCourse";
            this.ddlCourse.Size = new System.Drawing.Size(128, 21);
            this.ddlCourse.TabIndex = 24;
            // 
            // ClassDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 174);
            this.Controls.Add(this.ddlCourse);
            this.Controls.Add(this.ddlInstructor);
            this.Controls.Add(this.ddlClassRoom);
            this.Controls.Add(this.ddlSession);
            this.Controls.Add(this.ddlPeriod);
            this.Controls.Add(this.btnSaveAsNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClassDetails";
            this.TabText = "Class Details";
            this.Text = "Class Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAsNew;
        private System.Windows.Forms.ComboBox ddlPeriod;
        private System.Windows.Forms.ComboBox ddlSession;
        private System.Windows.Forms.ComboBox ddlClassRoom;
        private System.Windows.Forms.ComboBox ddlInstructor;
        private System.Windows.Forms.ComboBox ddlCourse;
    }
}