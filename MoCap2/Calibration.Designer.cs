namespace MoCap2
{
    partial class Calibration
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
            this.CalibrationTB = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Apply = new System.Windows.Forms.Button();
            this.CalculateB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PointsBuffTB = new System.Windows.Forms.TextBox();
            this.BufferL = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.WWidthTB = new System.Windows.Forms.TextBox();
            this.ResetB = new System.Windows.Forms.Button();
            this.StartB = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CalibrationTB.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CalibrationTB
            // 
            this.CalibrationTB.Controls.Add(this.tabPage2);
            this.CalibrationTB.Controls.Add(this.tabPage1);
            this.CalibrationTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalibrationTB.Location = new System.Drawing.Point(0, 0);
            this.CalibrationTB.Name = "CalibrationTB";
            this.CalibrationTB.SelectedIndex = 0;
            this.CalibrationTB.Size = new System.Drawing.Size(360, 747);
            this.CalibrationTB.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 721);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calibration";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Controls.Add(this.Apply);
            this.groupBox4.Controls.Add(this.CalculateB);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.ForeColor = System.Drawing.Color.Honeydew;
            this.groupBox4.Location = new System.Drawing.Point(3, 308);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(346, 410);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Calibration Data";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(3, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(340, 347);
            this.dataGridView1.TabIndex = 3;
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.White;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Apply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Apply.ForeColor = System.Drawing.Color.Black;
            this.Apply.Location = new System.Drawing.Point(267, 31);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(73, 23);
            this.Apply.TabIndex = 2;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            // 
            // CalculateB
            // 
            this.CalculateB.BackColor = System.Drawing.Color.White;
            this.CalculateB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CalculateB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CalculateB.ForeColor = System.Drawing.Color.Black;
            this.CalculateB.Location = new System.Drawing.Point(8, 31);
            this.CalculateB.Name = "CalculateB";
            this.CalculateB.Size = new System.Drawing.Size(103, 23);
            this.CalculateB.TabIndex = 1;
            this.CalculateB.Text = "Calculate";
            this.CalculateB.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.ResetB);
            this.groupBox1.Controls.Add(this.StartB);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.ForeColor = System.Drawing.Color.Honeydew;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 299);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "3-Marker Wand Calibration";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PointsBuffTB);
            this.groupBox3.Controls.Add(this.BufferL);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(6, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 110);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advanced";
            // 
            // PointsBuffTB
            // 
            this.PointsBuffTB.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PointsBuffTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PointsBuffTB.ForeColor = System.Drawing.Color.Honeydew;
            this.PointsBuffTB.Location = new System.Drawing.Point(202, 29);
            this.PointsBuffTB.Name = "PointsBuffTB";
            this.PointsBuffTB.Size = new System.Drawing.Size(100, 23);
            this.PointsBuffTB.TabIndex = 5;
            this.PointsBuffTB.Text = "100";
            // 
            // BufferL
            // 
            this.BufferL.AutoSize = true;
            this.BufferL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BufferL.ForeColor = System.Drawing.Color.Honeydew;
            this.BufferL.Location = new System.Drawing.Point(24, 32);
            this.BufferL.Name = "BufferL";
            this.BufferL.Size = new System.Drawing.Size(101, 17);
            this.BufferL.TabIndex = 4;
            this.BufferL.Text = "Frame buffer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.WWidthTB);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(6, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 112);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(221, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Custom";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(121, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "3-point wand";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(27, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "2-point wand";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Honeydew;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wand width, mm";
            // 
            // WWidthTB
            // 
            this.WWidthTB.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.WWidthTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WWidthTB.ForeColor = System.Drawing.Color.Honeydew;
            this.WWidthTB.Location = new System.Drawing.Point(202, 28);
            this.WWidthTB.Name = "WWidthTB";
            this.WWidthTB.Size = new System.Drawing.Size(100, 23);
            this.WWidthTB.TabIndex = 2;
            this.WWidthTB.Text = "500";
            // 
            // ResetB
            // 
            this.ResetB.BackColor = System.Drawing.Color.White;
            this.ResetB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ResetB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResetB.ForeColor = System.Drawing.Color.Black;
            this.ResetB.Location = new System.Drawing.Point(279, 22);
            this.ResetB.Name = "ResetB";
            this.ResetB.Size = new System.Drawing.Size(55, 23);
            this.ResetB.TabIndex = 1;
            this.ResetB.Text = "Reset";
            this.ResetB.UseVisualStyleBackColor = false;
            this.ResetB.Click += new System.EventHandler(this.ResetB_Click);
            // 
            // StartB
            // 
            this.StartB.BackColor = System.Drawing.Color.White;
            this.StartB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartB.ForeColor = System.Drawing.Color.Black;
            this.StartB.Location = new System.Drawing.Point(6, 22);
            this.StartB.Name = "StartB";
            this.StartB.Size = new System.Drawing.Size(104, 23);
            this.StartB.TabIndex = 0;
            this.StartB.Text = "Start Calibration";
            this.StartB.UseVisualStyleBackColor = false;
            this.StartB.Click += new System.EventHandler(this.StartB_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 721);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ground Plane";
            // 
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 747);
            this.Controls.Add(this.CalibrationTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Calibration";
            this.Text = "Calibration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Calibration_FormClosed);
            this.CalibrationTB.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl CalibrationTB;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ResetB;
        private System.Windows.Forms.Button StartB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WWidthTB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox PointsBuffTB;
        private System.Windows.Forms.Label BufferL;
        private System.Windows.Forms.Button CalculateB;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}