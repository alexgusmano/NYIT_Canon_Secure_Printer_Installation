using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace AutoPrintP1
{
    public partial class Recommended_Restart : Form
    {
        public Recommended_Restart()
        {
            InitializeComponent();
        }

        private void Recommended_Restart_Load(object sender, EventArgs e)
        {
            DialogResult restart = MessageBox.Show("If you have any questions, please contact Service Central at\n(516) 686-1400.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (restart == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
                Application.Exit();
            }
            
        }

        private void Recommended_Restart_Click(object sender, EventArgs e)
        {
            
        }

        private void Recommended_Restart_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            DialogResult dbcheck = MessageBox.Show("You will not be able to print using this printer until you restart.\nAre you sure you won't restart?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dbcheck == DialogResult.No)
            {
                e.Cancel = true;
            }
            */
        }

        
    }
}
