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
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void btnAlbums_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Searching Albums...");
        }

        private void btnSongs_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Searching Songs...");
            new FrmSongQuery().Show();
        }

        private void btnArtist_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Searching Artists...");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
