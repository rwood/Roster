namespace Roster.Forms
{
    partial class ClassRoomDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassRoomDetails));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAsNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlSchool = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxStudents = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(193, 159);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(95, 159);
            this.btnSaveAsNew.Name = "btnSaveAsNew";
            this.btnSaveAsNew.Size = new System.Drawing.Size(90, 23);
            this.btnSaveAsNew.TabIndex = 11;
            this.btnSaveAsNew.Text = "Save As New";
            this.btnSaveAsNew.UseVisualStyleBackColor = true;
            this.btnSaveAsNew.Click += new System.EventHandler(this.btnSaveAsNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "School:";
            // 
            // ddlSchool
            // 
            this.ddlSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSchool.FormattingEnabled = true;
            this.ddlSchool.Location = new System.Drawing.Point(96, 6);
            this.ddlSchool.Name = "ddlSchool";
            this.ddlSchool.Size = new System.Drawing.Size(172, 21);
            this.ddlSchool.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Room Number:";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(96, 36);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(172, 20);
            this.txtRoomNumber.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Max Students:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Notes:";
            // 
            // txtMaxStudents
            // 
            this.txtMaxStudents.Location = new System.Drawing.Point(96, 62);
            this.txtMaxStudents.Name = "txtMaxStudents";
            this.txtMaxStudents.Size = new System.Drawing.Size(172, 20);
            this.txtMaxStudents.TabIndex = 18;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(96, 88);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(172, 65);
            this.txtNotes.TabIndex = 19;
            // 
            // ClassRoomDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 201);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtMaxStudents);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRoomNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlSchool);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveAsNew);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClassRoomDetails";
            this.TabText = "ClassRoom Details";
            this.Text = "ClassRoom Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAsNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlSchool;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaxStudents;
        private System.Windows.Forms.TextBox txtNotes;
    }
}