using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFruitBar8000
{
    public partial class FrmSongMgmt : Form
    {
        public FrmSongMgmt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除?","警告",MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}
