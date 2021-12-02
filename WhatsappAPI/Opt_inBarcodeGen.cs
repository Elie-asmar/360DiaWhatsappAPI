using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using static QRCoder.PayloadGenerator;
using System.Web;

namespace WhatsappAPI
{
    public partial class Opt_inBarcodeGen : Form
    {
        public Opt_inBarcodeGen()
        {
            InitializeComponent();
        }
        private static string number;
        private static string urlencodedtext;
        private string whatsapptext = $"https://wa.me/{number}?text={urlencodedtext}";

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMsg.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Text");
                    return;
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                number = txtnbr.Text.Trim();
                urlencodedtext = HttpUtility.UrlEncode(txtMsg.Text);
                whatsapptext = $"https://wa.me/{number}?text={urlencodedtext}";
                pictureBox1.Image = GenerateQR(whatsapptext);
                pictureBox1.Size = new Size(pictureBox1.Image.Size.Width, pictureBox1.Image.Size.Height);
             



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Bitmap GenerateQR(string str)
        {
            try
            {
                Url generator = new Url(str);
                string payload = generator.ToString();

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                Bitmap qrCodeImage = qrCode.GetGraphic(2);
                return qrCodeImage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Opt_inBarcodeGen_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                ((ToolStripMenuItem)this.Tag).Tag = null;
                ((ToolStripMenuItem)this.Tag).Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save("C:\\QR.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
