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
    public partial class Calibration : Form
    {

        private static Calibration _instance;

        public static Calibration Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Calibration();
                return _instance;
            }
        }

        public Calibration()
        {
            InitializeComponent();
        }

        private void Calibration_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
