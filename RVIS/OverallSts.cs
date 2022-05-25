using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RVIS
{
    public partial class OverallSts : Form
    {
        public OverallSts()
        {
            InitializeComponent();
        }

        public void ShowStatus(string str)
        {
            if(str == "PASS")
            {
                lblOverallResult.Text = "PASS";
                lblOverallResult.BackColor = Color.Lime;
            }
            else
            {
                lblOverallResult.Text = "FAIL";
                lblOverallResult.BackColor = Color.Red;
            }
        }
 
    }
}
