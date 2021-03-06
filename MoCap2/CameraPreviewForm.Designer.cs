﻿namespace MoCap2
{
    partial class CameraPreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraPreviewForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GrayMI = new System.Windows.Forms.ToolStripMenuItem();
            this.BinMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowBlobsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.HideBlobsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ModePB = new System.Windows.Forms.PictureBox();
            this.StartB = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PictureBoxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModePB)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.PictureBoxMenu;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // PictureBoxMenu
            // 
            this.PictureBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GrayMI,
            this.BinMI,
            this.toolStripSeparator1,
            this.ShowBlobsMI,
            this.HideBlobsMI});
            this.PictureBoxMenu.Name = "contextMenuStrip1";
            this.PictureBoxMenu.Size = new System.Drawing.Size(159, 98);
            // 
            // GrayMI
            // 
            this.GrayMI.Name = "GrayMI";
            this.GrayMI.Size = new System.Drawing.Size(158, 22);
            this.GrayMI.Text = "Gray Image";
            // 
            // BinMI
            // 
            this.BinMI.Name = "BinMI";
            this.BinMI.Size = new System.Drawing.Size(158, 22);
            this.BinMI.Text = "Binarized Image";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // ShowBlobsMI
            // 
            this.ShowBlobsMI.Name = "ShowBlobsMI";
            this.ShowBlobsMI.Size = new System.Drawing.Size(158, 22);
            this.ShowBlobsMI.Text = "Show Blobs";
            // 
            // HideBlobsMI
            // 
            this.HideBlobsMI.Name = "HideBlobsMI";
            this.HideBlobsMI.Size = new System.Drawing.Size(158, 22);
            this.HideBlobsMI.Text = "Hide Blobs";
            // 
            // pictureBox2
            // 
            this.pictureBox2.ContextMenuStrip = this.PictureBoxMenu;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(652, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(640, 480);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1301, 511);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // ModePB
            // 
            this.ModePB.Location = new System.Drawing.Point(12, 36);
            this.ModePB.Name = "ModePB";
            this.ModePB.Size = new System.Drawing.Size(179, 41);
            this.ModePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ModePB.TabIndex = 3;
            this.ModePB.TabStop = false;
            // 
            // StartB
            // 
            this.StartB.BackColor = System.Drawing.Color.White;
            this.StartB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartB.ForeColor = System.Drawing.Color.Black;
            this.StartB.Location = new System.Drawing.Point(1222, 36);
            this.StartB.Name = "StartB";
            this.StartB.Size = new System.Drawing.Size(82, 23);
            this.StartB.TabIndex = 4;
            this.StartB.Text = "Undistort";
            this.StartB.UseVisualStyleBackColor = false;
            this.StartB.Click += new System.EventHandler(this.StartB_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1222, 69);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Show Calib";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 46F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Cornsilk;
            this.label1.Location = new System.Drawing.Point(537, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 71);
            this.label1.TabIndex = 6;
            this.label1.Text = "Preview";
            // 
            // CameraPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1325, 615);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.StartB);
            this.Controls.Add(this.ModePB);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CameraPreviewForm";
            this.Text = "Camera Preview";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CameraPreviewForm_FormClosed);
            this.Load += new System.EventHandler(this.CameraPreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.PictureBoxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ModePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox ModePB;
        private System.Windows.Forms.ContextMenuStrip PictureBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem GrayMI;
        private System.Windows.Forms.ToolStripMenuItem BinMI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ShowBlobsMI;
        private System.Windows.Forms.ToolStripMenuItem HideBlobsMI;
        private System.Windows.Forms.Button StartB;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
    }
}