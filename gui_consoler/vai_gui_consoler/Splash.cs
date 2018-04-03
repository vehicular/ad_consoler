using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vehicular_simulation
{
    public partial class Splash : Form
    {
        public delegate void DelegateUpdate(int type, object para);

        System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Splash));

        public DelegateUpdate splashUpdate;
        
        public Splash( string imgFile, string msg )
        {
            InitializeComponent();
            splashUpdate = new DelegateUpdate(UpdateSplash);
            this.pictureBox_splash.Image = Image.FromFile(imgFile);
            this.label_loading.Text = msg;
        }

        public bool isVisible = true;

        private void UpdateSplash(int type, object para)
        {
            if (type == 0)
                this.label_loading.Text = (string)para;
            else if (type == 1)
            {
                this.progressBar_splash.Value = (int)para;
            }
            else if (type == 2)
            {
                this.Visible = false;
                isVisible = false;
            }
            this.Refresh();
        }

        public void SetImage(string imgFile)
        {
            this.pictureBox_splash.Image = Image.FromFile(imgFile);
                //((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox_splash.Refresh();
        }

        
    }
}