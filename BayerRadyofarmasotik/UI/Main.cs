using BayerRadyofarmasotik.ButtonControls;
using BayerRadyofarmasotik.Connectors;
using BayerRadyofarmasotik.Entities;
using BayerRadyofarmasotik.FileHelper;
using BayerRadyofarmasotik.Logger;
using BayerRadyofarmasotik.Properties;
using BayerRadyofarmasotik.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BayerRadyofarmasotik
{
    public partial class Form1 : Form
    {
        SaglikTokenApiClient saglikTokenApiClient = new SaglikTokenApiClient(new FileLoggerService());
        public string accessToken;
        private static string baseAddress = Resources.ApiServiceEndPoint;
        System.Net.HttpStatusCode status;

        public Form1()
        {
            InitializeComponent();
            Assign(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userControlIptalHeader1.Hide();
            //userControlIstekHeader1.Show();
            dgvProducts.Location = new Point(13, 142);

            var configData = Operations.ReadFile("config.txt");

            var user = SetUser(configData);
            var loginResponse = saglikTokenApiClient.Login(user, out status);
            if (status == System.Net.HttpStatusCode.OK)
            {
                lblUserLogin.ForeColor = Color.Green;
                lblUserLogin.Text = $"Kullanıcı Bilgisi Girildi ({configData["environment"].ToUpper()} Sistem)";
                accessToken = loginResponse.access_token;
            }
            else
            {
                lblUserLogin.ForeColor = Color.Red;
                lblUserLogin.Text = $"Kullanıcı Girişi Yapılamadı!";
            }
        }

        private UserForLogin SetUser(Dictionary<string, string> value)
        {
            UserForLogin user = new UserForLogin
            {
                username = value["username"].Substring(1, value["username"].Length - 2),
                password = value["password"].Substring(1, value["password"].Length - 2)
            };
            return user;
        }

        private void istekBildirimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Istek istek = new Istek();
            istek.Show();
        }

        private void lblIstek_MouseEnter(object sender, EventArgs e)
        {
            lblIstek.Font = new Font(lblIstek.Font.Name, 14);
        }

        private void lblIstek_MouseLeave(object sender, EventArgs e)
        {
            lblIstek.Font = new Font(lblIstek.Font.Name, 12);
            this.Cursor = Cursors.Default;
        }

        private void lblİptal_MouseEnter(object sender, EventArgs e)
        {
            lblİptal.Font = new Font(lblİptal.Font.Name, 14);
        }

        private void lblİptal_MouseLeave(object sender, EventArgs e)
        {
            lblİptal.Font = new Font(lblİptal.Font.Name, 12);
            this.Cursor = Cursors.Default;
        }

        private void lblIstek_Click(object sender, EventArgs e)
        {
            lblİptal.ForeColor = Color.Gray;
            lblIstek.ForeColor = Color.Black;
            userControlIptalHeader1.Hide();
            pnlIstekHeader.Visible = true;
            dgvProducts.Visible = true;
            pnlIstekHeader.BringToFront();
            if (pnlIstekBody.Visible == false)
                dgvProducts.Location = new Point(13, 142);
            //userControlIstekHeader1.Show();
            //userControlIstekHeader1.BringToFront();
        }

        private void lblİptal_Click(object sender, EventArgs e)
        {
            lblİptal.ForeColor = Color.Black;
            lblIstek.ForeColor = Color.Gray;
            pnlIstekHeader.Visible = false;
            pnlIstekBody.Visible = false;
            dgvProducts.Visible = false;
            //userControlIstekHeader1.Hide();
            userControlIptalHeader1.Show();
            userControlIptalHeader1.BringToFront();
        }

        private void lblIstek_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lblİptal_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void btnIstekYeniSatir_Click(object sender, EventArgs e)
        {
            dgvProducts.Location = new Point(13, 717);
            pnlIstekBody.Visible = true;
            ComboboxItemSetting();
        }

        private void btnIstekYeniUrunKaydet_Click(object sender, EventArgs e)
        {
            var product = GetValueToProduct();
            //if (product)
            //{

            //}
            pnlIstekBody.Visible = false;
            dgvProducts.Location = new Point(13, 142);

            int n = dgvProducts.Rows.Add();
            dgvProducts.Rows[n].Cells[1].Value = product.gtin;
            dgvProducts.Rows[n].Cells[2].Value = product.bn;
            dgvProducts.Rows[n].Cells[3].Value = product.productionIdentifier;
            dgvProducts.Rows[n].Cells[4].Value = product.loadedActivity;
            dgvProducts.Rows[n].Cells[5].Value = product.loadedUnitId;
            dgvProducts.Rows[n].Cells[6].Value = product.calibrationActivity;
            dgvProducts.Rows[n].Cells[7].Value = product.calibrationUnitId;
            dgvProducts.Rows[n].Cells[8].Value = product.loadDate;
            dgvProducts.Rows[n].Cells[9].Value = product.dt;
            dgvProducts.Rows[n].Cells[10].Value = product.countryCode;
            dgvProducts.Rows[n].Cells[11].Value = product.xd;
            ClearIstekText();
            ClearPicBox();
        }

        private Product GetValueToProduct()
        {
            Product product = new Product
            {
                gtin = txtIstekGTIN.Texts,
                bn = TxtIstekBN.Texts,
                productionIdentifier = txtUretimTesisTanimlayici.Texts,
                loadedActivity = txtYuklenenAktivite.Texts,
                loadedUnitId = comboBoxHedeflenenAktiviteBirim.Text.Substring(0, 1),
                calibrationActivity = txtHedefAktivite.Texts,
                calibrationUnitId = comboBoxHedeflenenAktiviteBirim.Text.Substring(0, 1),
                dt = comboBoxDT.Text.Substring(comboBoxDT.Text.Length - 2, 1),
                countryCode = txtUlkeKodu.Texts,
                loadDate = dtpCikisTarihi.Value.ToString("yyyy-MM-dd"),
                xd = dtpSkt.Value.ToString("yyyy-MM-dd")
            };
            return product;
        }

        private void dgvProducts_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvProducts.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducts.Columns[e.ColumnIndex].Name == "columnIslemler")
                if (e.RowIndex >= 0)
                    dgvProducts.Rows.RemoveAt(e.RowIndex);
        }

        private void ClearIstekText()
        {
            txtIstekGTIN.Texts = "";
            TxtIstekBN.Texts = "";
            txtUretimTesisTanimlayici.Texts = "";
            txtYuklenenAktivite.Texts = "";
            dtpCikisTarihi.Text = DateTime.Now.ToString();
            txtYuklenenAktivite.Texts = "";
            txtHedefAktivite.Texts = "";
            txtUlkeKodu.Texts = "";
            dtpSkt.Text = DateTime.Now.ToString();
        }

        private void ClearPicBox()
        {
            pictureBoxUlkeKodu.Image = null;
            pictureBoxIstekGtin.Image = null;
            pictureBoxIstekBN.Image = null;
            pictureBoxHedefAktivite.Image = null;
            pictureBoxUretimTesisTanimlayici.Image = null;
            pictureBoxYuklenenAktivite.Image = null;
        }

        private void ComboboxItemSetting()
        {
            comboBoxDT.SelectedIndex = 0;
            comboBoxHedeflenenAktiviteBirim.SelectedIndex = 0;
            comboBoxYuklenenAktiviteBirim.SelectedIndex = 0;
        }

        private void btnIstekTemizle_Click(object sender, EventArgs e)
        {
            ClearIstekText();
            ClearPicBox();
            pictureBoxTOGLN.Image = null;
        }

        void Assign(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)ctrl;
                    tb.TextChanged += new EventHandler(tb_TextChanged);
                }
                else
                {
                    Assign(ctrl);
                }
            }
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (txtIstekTOGLN.Texts.Length > 0)
                pictureBoxTOGLN.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxTOGLN.Image = Properties.Resources.close__1_;
            if (txtIstekGTIN.Texts.Length > 0)
                pictureBoxIstekGtin.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxIstekGtin.Image = Properties.Resources.close__1_;
            if (TxtIstekBN.Texts.Length > 0)
                pictureBoxIstekBN.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxIstekBN.Image = Properties.Resources.close__1_;
            if (txtUretimTesisTanimlayici.Texts.Length > 0)
                pictureBoxUretimTesisTanimlayici.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxUretimTesisTanimlayici.Image = Properties.Resources.close__1_;
            if (txtHedefAktivite.Texts.Length > 0)
                pictureBoxHedefAktivite.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxHedefAktivite.Image = Properties.Resources.close__1_;
            if (txtYuklenenAktivite.Texts.Length > 0)
                pictureBoxYuklenenAktivite.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxYuklenenAktivite.Image = Properties.Resources.close__1_;
            if (txtUlkeKodu.Texts.Length > 0)
                pictureBoxUlkeKodu.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxUlkeKodu.Image = Properties.Resources.close__1_;
        }

        private void btnIstekGonder_Click(object sender, EventArgs e)
        {
            if (dgvProducts.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(txtIstekTOGLN.Texts))
                {
                    Products products = SetProductFromDGV();
                    SaglikBildiriApiClient saglikBildiriApiClient = new SaglikBildiriApiClient(new FileLoggerService());
                    //var response = saglikBildiriApiClient.Bildiri(products, accessToken);


                    string htmlString = @"<html>
                                            <head>
                                                <style>
                                                    body{
                                                        font-family: Arial, Helvetica, sans-serif;
                                                        font-size: 18;
                                                    }
                                                </style>
                                            </head>";
                    htmlString += $@"<body>
                                        <h2>RadyoFarmasotik Bildirim</h2>
                                         <h3> Computer ID: lenovo</h3>
                                         <h3> Bildirim Zaman: {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace(".", "/")}</h3>
                                         </br>";

                    for (int i = 0; i < dgvProducts.Rows.Count; i++)
                    {
                        htmlString += $"<h3>Bildirim Urun :#{i + 1}</h3><a>BN: {dgvProducts.Rows[i].Cells[2].Value}</a></br><a>GTIN: {dgvProducts.Rows[i].Cells[1].Value}</a></br></br>";
                    }
                    htmlString += "</body></html>";

                    DialogResult dialogResult = MessageBox.Show("Bildiri başarılı, Pdf olarak kaydetmek ister misin?", "Bilgilendirme", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.FileName = $"{ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").Replace("-", string.Empty)}";
                        save.Filter = "|*.pdf";
                        save.OverwritePrompt = true;
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                            var pdfBytes = htmlToPdf.GeneratePdf(htmlString);
                            System.IO.File.WriteAllBytes($"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").Replace("-", string.Empty)}.pdf", pdfBytes);
                        }
                    }
                    else
                    {
                        dialogResult = DialogResult.Cancel;
                    }
                }
                else
                    MessageBox.Show("TOGLN bilgisi girin.");
            }
            else
                MessageBox.Show("Ürün bilgisi girin.");
        }

        private Products SetProductFromDGV()
        {
            Products products = new Products()
            {
                productList = new List<Product>()
                {

                }
            };
            products.toGln = txtIstekTOGLN.Texts;
            for (int i = 0; i < dgvProducts.Rows.Count; i++)
            {
                Product product = new Product
                {
                    gtin = dgvProducts.Rows[i].Cells[1].Value.ToString(),
                    bn = dgvProducts.Rows[i].Cells[2].Value.ToString(),
                    productionIdentifier = dgvProducts.Rows[i].Cells[3].Value.ToString(),
                    loadedActivity = dgvProducts.Rows[i].Cells[4].Value.ToString(),
                    loadedUnitId = dgvProducts.Rows[i].Cells[5].Value.ToString(),
                    calibrationActivity = dgvProducts.Rows[i].Cells[6].Value.ToString(),
                    calibrationUnitId = dgvProducts.Rows[i].Cells[7].Value.ToString(),
                    loadDate = dgvProducts.Rows[i].Cells[8].Value.ToString(),
                    dt = dgvProducts.Rows[i].Cells[9].Value.ToString(),
                    countryCode = dgvProducts.Rows[i].Cells[10].Value.ToString(),
                    xd = dgvProducts.Rows[i].Cells[11].Value.ToString()
                };
                products.productList.Add(product);
            }
            return products;
        }

        private void GetBildiriResultByUC(ProductsResponse response)
        {
            for (int i = 0; i < response.productResponseList.Count; i++)
            {
                switch (response.productResponseList[i].uc)
                {
                    case "00000":
                        MessageBox.Show("Urun uzerinde gerceklestirilen islem basarilidir");
                        break;
                    case "11005":
                        MessageBox.Show("Bu urune ait bildirim yapma yetkiniz yok");
                        break;
                    case "10204":
                        MessageBox.Show("Belirtilen urun daha once satılmıs");
                        break;
                    case "10202":
                        MessageBox.Show("Urunun Son Kullanma Tarihi gecmistir");
                        break;
                    case "11004":
                        MessageBox.Show("Yanlis GTIN numarasi");
                        break;
                    case "10206":
                        MessageBox.Show("Veri tabani kayit hatasi");
                        break;
                    case "10201":
                        MessageBox.Show("Belirtilen urun sistemde kayitli degil");
                        break;
                    case "15029":
                        MessageBox.Show("Gecersiz (Kalibrasyon/Yüklenen Aktivite) birim degeri");
                        break;
                    default:
                        MessageBox.Show("Bilinmeyen durum!");
                        break;
                }
            }
        }
    }
}
