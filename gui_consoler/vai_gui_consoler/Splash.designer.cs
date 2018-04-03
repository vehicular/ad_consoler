namespace vehicular_simulation
{
    partial class Splash
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
            this.pictureBox_splash = new System.Windows.Forms.PictureBox();
            this.label_loading = new System.Windows.Forms.Label();
            this.progressBar_splash = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_splash)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_splash
            // 
            this.pictureBox_splash.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_splash.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_splash.Name = "pictureBox_splash";
            this.pictureBox_splash.Size = new System.Drawing.Size(400, 325);
            this.pictureBox_splash.TabIndex = 0;
            this.pictureBox_splash.TabStop = false;
            // 
            // label_loading
            // 
            this.label_loading.AutoSize = true;
            this.label_loading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_loading.Location = new System.Drawing.Point(8, 327);
            this.label_loading.Name = "label_loading";
            this.label_loading.Size = new System.Drawing.Size(80, 16);
            this.label_loading.TabIndex = 1;
            this.label_loading.Text = "Loading ...";
            // 
            // progressBar_splash
            // 
            this.progressBar_splash.Location = new System.Drawing.Point(11, 347);
            this.progressBar_splash.Name = "progressBar_splash";
            this.progressBar_splash.Size = new System.Drawing.Size(377, 11);
            this.progressBar_splash.TabIndex = 2;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(400, 365);
            this.Controls.Add(this.progressBar_splash);
            this.Controls.Add(this.label_loading);
            this.Controls.Add(this.pictureBox_splash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TransparencyKey = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_splash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_splash;
        private System.Windows.Forms.Label label_loading;
        private System.Windows.Forms.ProgressBar progressBar_splash;
    }
}