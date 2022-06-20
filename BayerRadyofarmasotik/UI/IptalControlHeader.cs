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
    public partial class UserControlIptalHeader : UserControl
    {
        public UserControlIptalHeader()
        {
            InitializeComponent();
        }

        private void btnIptalEkle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("iptal");
        }
    }
}
