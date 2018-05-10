namespace MoCap2
{
    partial class Scene3D
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
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.WandB = new System.Windows.Forms.Button();
            this.StartB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl1.DrawFPS = true;
            this.openGLControl1.FrameRate = 35;
            this.openGLControl1.Location = new System.Drawing.Point(0, 0);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(1281, 944);
            this.openGLControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.WandB);
            this.panel1.Controls.Add(this.StartB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1281, 58);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(1117, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 58);
            this.button1.TabIndex = 3;
            this.button1.Text = "All Points";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WandB
            // 
            this.WandB.BackColor = System.Drawing.Color.White;
            this.WandB.Dock = System.Windows.Forms.DockStyle.Right;
            this.WandB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.WandB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WandB.ForeColor = System.Drawing.Color.Black;
            this.WandB.Location = new System.Drawing.Point(1199, 0);
            this.WandB.Name = "WandB";
            this.WandB.Size = new System.Drawing.Size(82, 58);
            this.WandB.TabIndex = 2;
            this.WandB.Text = "Wand Test";
            this.WandB.UseVisualStyleBackColor = false;
            this.WandB.Click += new System.EventHandler(this.WandB_Click);
            // 
            // StartB
            // 
            this.StartB.BackColor = System.Drawing.Color.White;
            this.StartB.Dock = System.Windows.Forms.DockStyle.Left;
            this.StartB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartB.ForeColor = System.Drawing.Color.Black;
            this.StartB.Location = new System.Drawing.Point(0, 0);
            this.StartB.Name = "StartB";
            this.StartB.Size = new System.Drawing.Size(132, 58);
            this.StartB.TabIndex = 1;
            this.StartB.Text = "Start Triangulation";
            this.StartB.UseVisualStyleBackColor = false;
            this.StartB.Click += new System.EventHandler(this.StartB_Click);
            // 
            // Scene3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 944);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Scene3D";
            this.Text = "Scene3D";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Scene3D_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button WandB;
        private System.Windows.Forms.Button StartB;
    }
}