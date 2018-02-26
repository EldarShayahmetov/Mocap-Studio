namespace MoCap2
{
    partial class CamerasForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MoreB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ThrTB = new System.Windows.Forms.TrackBar();
            this.ExpTB = new System.Windows.Forms.TrackBar();
            this.FpsTB = new System.Windows.Forms.TrackBar();
            this.NameL = new System.Windows.Forms.Label();
            this.ImagePB = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CamerasGrid = new System.Windows.Forms.DataGridView();
            this.EN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Camera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.IntrisicsB = new System.Windows.Forms.Button();
            this.IntrisicsL = new System.Windows.Forms.Label();
            this.ResolutionCB = new System.Windows.Forms.ComboBox();
            this.CodecL = new System.Windows.Forms.Label();
            this.ResolutionLL = new System.Windows.Forms.Label();
            this.CodecLL = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThrTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpsTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePB)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CamerasGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.MoreB);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ThrTB);
            this.panel1.Controls.Add(this.ExpTB);
            this.panel1.Controls.Add(this.FpsTB);
            this.panel1.Controls.Add(this.NameL);
            this.panel1.Controls.Add(this.ImagePB);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 227);
            this.panel1.TabIndex = 0;
            // 
            // MoreB
            // 
            this.MoreB.Location = new System.Drawing.Point(224, 189);
            this.MoreB.Name = "MoreB";
            this.MoreB.Size = new System.Drawing.Size(75, 23);
            this.MoreB.TabIndex = 8;
            this.MoreB.Text = "More...";
            this.MoreB.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Honeydew;
            this.label4.Location = new System.Drawing.Point(260, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "THR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Honeydew;
            this.label3.Location = new System.Drawing.Point(210, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "EXP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Honeydew;
            this.label2.Location = new System.Drawing.Point(160, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "FPS";
            // 
            // ThrTB
            // 
            this.ThrTB.Location = new System.Drawing.Point(254, 26);
            this.ThrTB.Name = "ThrTB";
            this.ThrTB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ThrTB.Size = new System.Drawing.Size(45, 157);
            this.ThrTB.TabIndex = 4;
            this.ThrTB.TickFrequency = 20;
            this.ThrTB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // ExpTB
            // 
            this.ExpTB.Location = new System.Drawing.Point(203, 26);
            this.ExpTB.Name = "ExpTB";
            this.ExpTB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ExpTB.Size = new System.Drawing.Size(45, 157);
            this.ExpTB.TabIndex = 3;
            this.ExpTB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // FpsTB
            // 
            this.FpsTB.Enabled = false;
            this.FpsTB.Location = new System.Drawing.Point(152, 26);
            this.FpsTB.Name = "FpsTB";
            this.FpsTB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.FpsTB.Size = new System.Drawing.Size(45, 157);
            this.FpsTB.TabIndex = 2;
            this.FpsTB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // NameL
            // 
            this.NameL.AutoSize = true;
            this.NameL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameL.ForeColor = System.Drawing.Color.Honeydew;
            this.NameL.Location = new System.Drawing.Point(25, 166);
            this.NameL.Name = "NameL";
            this.NameL.Size = new System.Drawing.Size(104, 17);
            this.NameL.TabIndex = 1;
            this.NameL.Text = "CameraName";
            // 
            // ImagePB
            // 
            this.ImagePB.Location = new System.Drawing.Point(18, 26);
            this.ImagePB.Name = "ImagePB";
            this.ImagePB.Size = new System.Drawing.Size(128, 128);
            this.ImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePB.TabIndex = 0;
            this.ImagePB.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.CamerasGrid);
            this.panel2.Location = new System.Drawing.Point(12, 245);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 359);
            this.panel2.TabIndex = 1;
            // 
            // CamerasGrid
            // 
            this.CamerasGrid.AllowUserToAddRows = false;
            this.CamerasGrid.AllowUserToDeleteRows = false;
            this.CamerasGrid.AllowUserToResizeColumns = false;
            this.CamerasGrid.AllowUserToResizeRows = false;
            this.CamerasGrid.BackgroundColor = System.Drawing.SystemColors.WindowFrame;
            this.CamerasGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CamerasGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CamerasGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EN,
            this.Camera,
            this.FPS,
            this.EXP,
            this.THR});
            this.CamerasGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CamerasGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.CamerasGrid.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CamerasGrid.Location = new System.Drawing.Point(0, 0);
            this.CamerasGrid.MultiSelect = false;
            this.CamerasGrid.Name = "CamerasGrid";
            this.CamerasGrid.RowHeadersVisible = false;
            this.CamerasGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CamerasGrid.Size = new System.Drawing.Size(307, 355);
            this.CamerasGrid.TabIndex = 0;
            // 
            // EN
            // 
            this.EN.FalseValue = "false";
            this.EN.HeaderText = "EN";
            this.EN.Name = "EN";
            this.EN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EN.TrueValue = "true";
            this.EN.Width = 30;
            // 
            // Camera
            // 
            this.Camera.HeaderText = "Camera";
            this.Camera.Name = "Camera";
            this.Camera.ReadOnly = true;
            this.Camera.Width = 95;
            // 
            // FPS
            // 
            this.FPS.HeaderText = "FPS";
            this.FPS.Name = "FPS";
            this.FPS.ReadOnly = true;
            this.FPS.Width = 60;
            // 
            // EXP
            // 
            this.EXP.HeaderText = "EXP";
            this.EXP.Name = "EXP";
            this.EXP.ReadOnly = true;
            this.EXP.Width = 60;
            // 
            // THR
            // 
            this.THR.HeaderText = "THR";
            this.THR.Name = "THR";
            this.THR.ReadOnly = true;
            this.THR.Width = 60;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.IntrisicsB);
            this.panel3.Controls.Add(this.IntrisicsL);
            this.panel3.Controls.Add(this.ResolutionCB);
            this.panel3.Controls.Add(this.CodecL);
            this.panel3.Controls.Add(this.ResolutionLL);
            this.panel3.Controls.Add(this.CodecLL);
            this.panel3.Location = new System.Drawing.Point(12, 610);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(311, 205);
            this.panel3.TabIndex = 2;
            // 
            // IntrisicsB
            // 
            this.IntrisicsB.Location = new System.Drawing.Point(163, 105);
            this.IntrisicsB.Name = "IntrisicsB";
            this.IntrisicsB.Size = new System.Drawing.Size(121, 23);
            this.IntrisicsB.TabIndex = 9;
            this.IntrisicsB.Text = "Load...";
            this.IntrisicsB.UseVisualStyleBackColor = true;
            // 
            // IntrisicsL
            // 
            this.IntrisicsL.AutoSize = true;
            this.IntrisicsL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IntrisicsL.ForeColor = System.Drawing.Color.Honeydew;
            this.IntrisicsL.Location = new System.Drawing.Point(5, 105);
            this.IntrisicsL.Name = "IntrisicsL";
            this.IntrisicsL.Size = new System.Drawing.Size(124, 17);
            this.IntrisicsL.TabIndex = 4;
            this.IntrisicsL.Text = "Camera Intrisics";
            // 
            // ResolutionCB
            // 
            this.ResolutionCB.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ResolutionCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResolutionCB.ForeColor = System.Drawing.Color.Honeydew;
            this.ResolutionCB.FormattingEnabled = true;
            this.ResolutionCB.Items.AddRange(new object[] {
            "640x480",
            "800x600",
            "1280x720",
            "1920x1080"});
            this.ResolutionCB.Location = new System.Drawing.Point(163, 64);
            this.ResolutionCB.Name = "ResolutionCB";
            this.ResolutionCB.Size = new System.Drawing.Size(121, 24);
            this.ResolutionCB.TabIndex = 3;
            // 
            // CodecL
            // 
            this.CodecL.AutoSize = true;
            this.CodecL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CodecL.ForeColor = System.Drawing.Color.Honeydew;
            this.CodecL.Location = new System.Drawing.Point(160, 36);
            this.CodecL.Name = "CodecL";
            this.CodecL.Size = new System.Drawing.Size(48, 17);
            this.CodecL.TabIndex = 2;
            this.CodecL.Text = "YUY2";
            // 
            // ResolutionLL
            // 
            this.ResolutionLL.AutoSize = true;
            this.ResolutionLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResolutionLL.ForeColor = System.Drawing.Color.Honeydew;
            this.ResolutionLL.Location = new System.Drawing.Point(44, 67);
            this.ResolutionLL.Name = "ResolutionLL";
            this.ResolutionLL.Size = new System.Drawing.Size(85, 17);
            this.ResolutionLL.TabIndex = 1;
            this.ResolutionLL.Text = "Resolution";
            // 
            // CodecLL
            // 
            this.CodecLL.AutoSize = true;
            this.CodecLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CodecLL.ForeColor = System.Drawing.Color.Honeydew;
            this.CodecLL.Location = new System.Drawing.Point(76, 36);
            this.CodecLL.Name = "CodecLL";
            this.CodecLL.Size = new System.Drawing.Size(53, 17);
            this.CodecLL.TabIndex = 0;
            this.CodecLL.Text = "Codec";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            // 
            // CamerasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(335, 827);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CamerasForm";
            this.Text = "Cameras";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CamerasForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThrTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpsTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePB)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CamerasGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label NameL;
        private System.Windows.Forms.PictureBox ImagePB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar ThrTB;
        private System.Windows.Forms.TrackBar ExpTB;
        private System.Windows.Forms.TrackBar FpsTB;
        private System.Windows.Forms.DataGridView CamerasGrid;
        private System.Windows.Forms.Button MoreB;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Camera;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXP;
        private System.Windows.Forms.DataGridViewTextBoxColumn THR;
        private System.Windows.Forms.ComboBox ResolutionCB;
        private System.Windows.Forms.Label CodecL;
        private System.Windows.Forms.Label ResolutionLL;
        private System.Windows.Forms.Label CodecLL;
        private System.Windows.Forms.Button IntrisicsB;
        private System.Windows.Forms.Label IntrisicsL;
    }
}