using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static cls_Requests;

namespace WhatsappAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string TokenEncKey = "Ctserv TT-TY2020"; // AES Encryption Key of the JWT Token
        private string FrontEndinitVector = "Ctserv TT-TY2020"; // Iv of the AES 
        public string GetEncryptedUrl(string mrn, string caseNo, string babyNo, string resultId, string year)
        {
            QRInfo pl = new QRInfo();
            pl.MRN = mrn;
            pl.CaseNo = caseNo;
            pl.BabyNo = babyNo;
            pl.ResultID = resultId;
            pl.Year = year;
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            string ss = serializer1.Serialize(pl);
            ss = EncryptAES(ref ss, ref TokenEncKey);

            return "/QRlabResult/" + Convert.ToBase64String(Encoding.UTF8.GetBytes(ss));
        }
        private String EncryptAES(ref string clearText, ref string EncryptionKey)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] Mykey = Encoding.UTF8.GetBytes(EncryptionKey);
            byte[] Myiv = Encoding.UTF8.GetBytes(FrontEndinitVector);
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Padding = PaddingMode.PKCS7;
            encryptor.FeedbackSize = 128;
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = Mykey;
            encryptor.IV = Myiv;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
            clearText = Convert.ToBase64String(ms.ToArray());
            return clearText;
        }



        //private string GetURL()
        //{
        //    Dictionary<QRInfo, string> dict = new Dictionary<WhatsappAPI.QRInfo, string>();
        //    QRInfo currentItem = new QRInfo()
        //    {
        //        MRN = txtMFNo.Text.Trim(),
        //        CaseNo = txtCaseNo.Text.Trim(),
        //        BabyNo = txtBabNo.Text.Trim(),
        //        ResultID = txtResID.Text.Trim(),
        //        Year = txtYear.Text.Trim()
        //    };


        //    var url = getLabWebUrl();

        //    return url + GetEncryptedUrl(currentItem.MRN, currentItem.CaseNo, currentItem.BabyNo, currentItem.ResultID, currentItem.Year);

        //}



        //public string getLabWebUrl(string _connectionString = null)
        //{

        //    try
        //    {
        //        return @"http://183.183.183.122/labrad/#";



        //    }

        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                //JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                //_360DialogTextMessage msg = new _360DialogTextMessage() { to = txtnbr.Text.Trim(), text = new _360DialogTextMessagetext(txtMsg.Text) };

                //// string body = "{\"recipient_type\": \"individual\",\"to\": \"" + txtnbr.Text.Trim() + "\", \"type\": \"text\",\"text\": {\"body\": \""+txtMsg.Text+"\"}}";
                //// string body = "{\"recipient_type\": \"individual\",\"to\": \"96171190337\", \"type\": \"text\",\"text\": {\"body\": \"" + GetURL() + "\"}}";
                ////string body = "{\"recipient_type\": \"individual\",\"to\": \"96171190337\", \"type\": \"text\",\"text\": {\"body\": \"Your Lab Result: \n" + GetURL() + "\"}}";
                //string body = serializer1.Serialize(msg);

                //var msg1 = POST("https://waba.360dialog.io/v1/messages", body, new Dictionary<string, string>() { { "D360-API-KEY", "62kegb5RB8PsBFNwrdAXevXjAK" } });
             

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                var dlg = openFileDialog1.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    foreach (var file in openFileDialog1.FileNames)
                    {
                        Panel pnl = new Panel();
                        pnlUpload.Controls.Add(pnl);
                        pnl.Dock = DockStyle.Top;
                        pnl.BorderStyle = BorderStyle.FixedSingle;
                        pnl.Height = 20;
                        pnl.Width = pnlUpload.Width;
                        pnl.Tag = new ArrayList();
                        ((ArrayList)pnl.Tag).Add(file);
                        pnl.BringToFront();
                        pnl.Parent = pnlUpload;

                        TextBox txt = new TextBox();
                        txt.ReadOnly = true;
                        pnl.Controls.Add(txt);
                        txt.Dock = DockStyle.Left;

                        txt.Text = Path.GetFileName(file);

                        txt.Size = new Size(150, 20);


                        TextBox txtcaption = new TextBox();
                        txtcaption.ReadOnly = false;
                        pnl.Controls.Add(txtcaption);
                        ((ArrayList)pnl.Tag).Add(txtcaption);
                        txtcaption.Dock = DockStyle.Left;

                        txtcaption.Text = "(Insert Caption Here)";

                        txtcaption.Width = 250;
                        txtcaption.BringToFront();

                        Button btn = new Button();
                        btn.Text = "Delete";
                        btn.Click += Btn_Click;
                        btn.Parent = pnl;
                        btn.Dock = DockStyle.Left;
                        btn.BringToFront();

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).Parent.Parent.Controls.Remove((Control)(((Button)sender).Parent));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlUpload.Controls.Count == 0)
                {
                    MessageBox.Show("No Media Uploaded.");
                    return;
                }
                else
                {
                    var arl = (ArrayList)pnlUpload.Controls[0].Tag;
                    var flname = arl[0].ToString();
                    var capt = ((TextBox)arl[1]).Text;
                    _360DialogWrapper._360DialogWrapper._360DialogAPIkey = "62kegb5RB8PsBFNwrdAXevXjAK";
                    var msg= _360DialogWrapper._360DialogWrapper.SendMediaFile(txtphone2.Text.Trim(), _360DialogWrapper.mediatype.document_pdf, null, flname, capt);

                    //JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                    //var arl = (ArrayList)pnlUpload.Controls[0].Tag;
                    //var flname = arl[0].ToString();
                    //var capt = ((TextBox)arl[1]).Text;



                    //var mediaupload = POSTMediaFile("https://waba.360dialog.io/v1/media", flname, new Dictionary<string, string>() { { "D360-API-KEY", "62kegb5RB8PsBFNwrdAXevXjAK" } });
                    //var mediauploadresp = serializer1.Deserialize<_360DialogUploadedMediaResponse>(mediaupload);

                    //_360DialogMediaDocumentMessage msg = new _360DialogMediaDocumentMessage() { to = txtphone2.Text.Trim(), document = new _360DialogMediaDocumentMessagedocument(mediauploadresp.media[0].id, capt) };

                    //string body = serializer1.Serialize(msg);

                    //var rr = POST("https://waba.360dialog.io/v1/messages", body, new Dictionary<string, string>() { { "D360-API-KEY", "62kegb5RB8PsBFNwrdAXevXjAK" } });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


    public class QRInfo
    {
        public string MRN { get; set; }
        public string CaseNo { get; set; } // Eth_PatAdmNo
        public string BabyNo { get; set; } // Eth_PatAdmNoSub
        public string ResultID { get; set; } // Eth_SkeletonNo
        public string Year { get; set; } // Eth_Year
    }





}
