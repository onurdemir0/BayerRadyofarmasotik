using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BayerRadyofarmasotik.UI
{
    public partial class UserControlIstekHeader : UserControl
    {
        public UserControlIstekHeader()
        {
            InitializeComponent();
        }

        private void UserControlIstekHeader_Load(object sender, EventArgs e)
        {
            dgvProducts.Location = new Point(13, 109);
        }

        private void btnIstekYeniSatir_Click(object sender, EventArgs e)
        {
            dgvProducts.Location = new Point(13, 669);
            pnlIstekBody.Visible = true;
        }

        private void btnIstekYeniUrunKaydet_Click(object sender, EventArgs e)
        {
            pnlIstekBody.Visible = false;
            dgvProducts.Location = new Point(13, 109);
        }

        private void btnIstekGonder_Click(object sender, EventArgs e)
        {
            
        }
    }
}
