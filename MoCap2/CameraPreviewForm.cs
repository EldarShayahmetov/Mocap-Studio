using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoCap2
{
    public partial class CameraPreviewForm : Form
    {
        private static CameraPreviewForm _instance;

        public static CameraPreviewForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CameraPreviewForm();
                return _instance;
            }
        }

        public CameraPreviewForm()
        {
            InitializeComponent();
        }

        private void CameraPreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
