using BayerRadyofarmasotik.Connectors;
using BayerRadyofarmasotik.Entities;
using BayerRadyofarmasotik.FileHelper;
using BayerRadyofarmasotik.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BayerRadyofarmasotik.UI
{
    public partial class Iptal : Form
    {
        Form1 istekFrm;
        SaglikTokenApiClient saglikTokenApiClient = new SaglikTokenApiClient(new FileLoggerService());
        public string accessToken;
        System.Net.HttpStatusCode status;
        public Dictionary<string, string> configData;

        public Iptal()
        {
            InitializeComponent();
            Assign(this);
        }

        private void Iptal_Load(object sender, EventArgs e)
        {
            dgvProducts.Location = new Point(13, 142);

            configData = Operations.ReadFile("config.txt");

            var user = SetUser(configData);

            LoginResponse loginResponse;
            if (configData["environment"].Substring(1, configData["environment"].Length - 2).ToLower() == "production")
                loginResponse = saglikTokenApiClient.LoginToProd(user, out status);
            else
                loginResponse = saglikTokenApiClient.Login(user, out status);

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

        private void lblİptal_MouseEnter(object sender, EventArgs e)
        {
            lblİptal.Font = new Font(lblİptal.Font.Name, 14);
        }

        private void lblİptal_MouseLeave(object sender, EventArgs e)
        {
            lblİptal.Font = new Font(lblİptal.Font.Name, 12);
        }

        private void lblIstek_MouseEnter(object sender, EventArgs e)
        {
            lblIstek.Font = new Font(lblIstek.Font.Name, 14);
        }

        private void lblIstek_MouseLeave(object sender, EventArgs e)
        {
            lblIstek.Font = new Font(lblIstek.Font.Name, 12);
        }

        private bool CheckOpened(string formName)
        {
            FormCollection formCollection = Application.OpenForms;
            formCollection[0].Show();
            foreach (Form form in formCollection)
            {
                if (form.Text == formName)
                {
                    return true;
                }
            }
            return false;
        }

        private void FormSet(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.Dock = DockStyle.Fill;
            form.Show();
            form.Focus();
        }

        private void lblIstek_Click(object sender, EventArgs e)
        {
            FormCollection formCollection = Application.OpenForms;
            formCollection[0].Show();
            formCollection[0].Focus();
            //if (istekFrm == null || istekFrm.Text == "")
            //{
            //    istekFrm = new Form1();
            //    istekFrm.Show();
            //}
            //else if (CheckOpened(istekFrm.Text))
            //    FormSet(istekFrm);
        }

        private void btnIptalYeniSatir_Click(object sender, EventArgs e)
        {
            dgvProducts.Location = new Point(13, 385);
            pnlIptalBody.Visible = true;
        }

        private void btnIptalKaydet_Click(object sender, EventArgs e)
        {
            var product = GetValueToProduct();
            if (!String.IsNullOrEmpty(product.gtin) && !String.IsNullOrEmpty(product.bn))
            {
                pnlIptalBody.Visible = false;
                dgvProducts.Location = new Point(13, 142);

                int n = dgvProducts.Rows.Add();
                dgvProducts.Rows[n].Cells[1].Value = product.gtin;
                dgvProducts.Rows[n].Cells[2].Value = product.bn;
                dgvProducts.Rows[n].Cells[3].Value = product.loadDate;
                ClearIptalText();
                ClearPicBox();
            }
            else
                MessageBox.Show("Lütfen zorunlu alanları doldurun.");
        }

        private ProductIptal GetValueToProduct()
        {
            ProductIptal product = new ProductIptal
            {
                gtin = txtIstekGTIN.Texts,
                bn = TxtIstekBN.Texts,
                loadDate = dtpCikisTarihi.Value.ToString("yyyy-MM-dd")
            };
            return product;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducts.Columns[e.ColumnIndex].Name == "columnIslemler")
                if (e.RowIndex >= 0)
                    dgvProducts.Rows.RemoveAt(e.RowIndex);
        }

        private void dgvProducts_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvProducts.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void ClearIptalText()
        {
            txtIstekGTIN.Texts = "";
            TxtIstekBN.Texts = "";
        }

        private void ClearPicBox()
        {
            pictureBoxUlkeKodu.Image = null;
            pictureBoxToGln.Image = null;
            pictureBoxIstekGtin.Image = null;
            pictureBoxIstekBN.Image = null;
        }

        private void btnIptalTemizle_Click(object sender, EventArgs e)
        {
            ClearIptalText();
            ClearPicBox();
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
                    Assign(ctrl);
            }
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (txtIstekGTIN.Texts.Length > 0)
                pictureBoxIstekGtin.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxIstekGtin.Image = Properties.Resources.close__1_;
            if (TxtIstekBN.Texts.Length > 0)
                pictureBoxIstekBN.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxIstekBN.Image = Properties.Resources.close__1_;
            if (txtIstekTOGLN.Texts.Length > 0)
                pictureBoxToGln.Image = Properties.Resources.check_mark__1_;
            else
                pictureBoxToGln.Image = Properties.Resources.close__1_;
        }

        ProductsResponse response;
        private void btnIptalGonder_Click(object sender, EventArgs e)
        {
            if (dgvProducts.Rows.Count >0)
            {
                // TOGLN textbox'ı kontrol edilecek
                if (!String.IsNullOrEmpty(txtIstekTOGLN.Texts))
                {
                    ProductsIptal products = SetProductFromDGV();

                    if (configData["environment"].Substring(1, configData["environment"].Length - 2).ToLower() == "production")
                    {
                        SaglikProdBildiriApiClient saglikBildiriApiClient = new SaglikProdBildiriApiClient(new FileLoggerService());
                        response = saglikBildiriApiClient.IptalBildiri(products, accessToken);
                    }
                    //TEST API
                    else
                    {
                        SaglikTestBildiriApiClient saglikBildiriApiClient = new SaglikTestBildiriApiClient(new FileLoggerService());
                        response = saglikBildiriApiClient.IptalBildiri(products, accessToken);
                    }

                    var htmlString = Operations.CreateHtmlContent(response);

                    var responseMessage = GetBildiriMessage(response);
                    DialogResult dialogResult = MessageBox.Show($"{responseMessage} \nPdf olarak kaydetmek ister misin?", "Bilgilendirme", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.FileName = $"{ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").Replace("-", string.Empty)}";
                        save.Filter = "|*.pdf";
                        save.OverwritePrompt = true;
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText("response.html", htmlString);
                            var pdfBytes = PdfGenerator.HtmlToPdf(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), save.FileName, "response.html", new string[] { });

                            dgvProducts.Rows.Clear();
                            dgvProducts.Refresh();
                        }
                    }
                    else
                        dialogResult = DialogResult.Cancel;
                }
                else
                    MessageBox.Show("TOGLN bilgisi girin.");
            }
            else
                MessageBox.Show("Ürün bilgisi girin.");
            
        }

        private ProductsIptal SetProductFromDGV()
        {
            ProductsIptal products = new ProductsIptal()
            {
                productList = new List<ProductIptal>()
                {

                }
            };
            products.toGln = txtIstekTOGLN.Texts;
            for (int i = 0; i < dgvProducts.Rows.Count; i++)
            {
                ProductIptal product = new ProductIptal
                {
                    gtin = dgvProducts.Rows[i].Cells[1].Value.ToString(),
                    bn = dgvProducts.Rows[i].Cells[2].Value.ToString(),
                    loadDate = dgvProducts.Rows[i].Cells[3].Value.ToString()
                };
                products.productList.Add(product);
            }
            return products;
        }

        private string GetBildiriMessage(ProductsResponse response)
        {
            string requestMessage = "";
            for (int i = 0; i < response.productList.Count; i++)
            {
                switch (response.productList[i].uc)
                {
                    case "00000":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Urun uzerinde gerceklestirilen islem basarilidir.\n";
                        break;
                    case "11005":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Bu urune ait bildirim yapma yetkiniz yok.\n";
                        break;
                    case "10204":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Belirtilen urun daha once satılmıs.\n";
                        break;
                    case "10202":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Urunun Son Kullanma Tarihi gecmistir.\n";
                        break;
                    case "11004":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Yanlis GTIN numarasi.\n";
                        break;
                    case "10206":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Veri tabani kayit hatasi.\n";
                        break;
                    case "10201":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Belirtilen urun sistemde kayitli degil.\n";
                        break;
                    case "15029":
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Gecersiz (Kalibrasyon/Yüklenen Aktivite) birim degeri.\n";
                        break;
                    default:
                        requestMessage += $"GTIN: {response.productList[i].gtin}, Bilinmeyen durum! UC kodu: {response.productList[i].uc}.\n";
                        break;
                }
            }
            return requestMessage;
        }
    }
}
