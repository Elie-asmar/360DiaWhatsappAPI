using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsappAPI
{
    public partial class menu : Form
    {
        private int childFormNumber = 0;

        public menu()
        {
            InitializeComponent();
        }


        private void mnuOptin_Click(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripMenuItem)sender).Tag == null)
                {

                    Opt_inBarcodeGen frm = new Opt_inBarcodeGen();


                    frm.MdiParent = this;
                    frm.Tag = sender;
                    ((ToolStripMenuItem)sender).Tag = frm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    ((ToolStripMenuItem)sender).Checked = true;

                }
                else
                {
                    ((Form)((ToolStripMenuItem)sender).Tag).WindowState = FormWindowState.Maximized;
                    ((Form)((ToolStripMenuItem)sender).Tag).Show();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuSendData_Click(object sender, EventArgs e)
        {
            try
            {
                if (((ToolStripMenuItem)sender).Tag == null)
                {

                    Form1 frm = new Form1();


                    frm.MdiParent = this;
                    frm.Tag = sender;
                    ((ToolStripMenuItem)sender).Tag = frm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    ((ToolStripMenuItem)sender).Checked = true;

                }
                else
                {
                    ((Form)((ToolStripMenuItem)sender).Tag).WindowState = FormWindowState.Maximized;
                    ((Form)((ToolStripMenuItem)sender).Tag).Show();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
