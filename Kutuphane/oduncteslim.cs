using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class oduncteslim : Form
    {
        public oduncteslim()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            odunc panel = new odunc(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
            panel.FormClosed += Panel_FormClosed; // FormClosed olayını burada bağlıyoruz.
            panel.Show();
            this.Hide();
        }
        public void Panel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Panel kapandığında Form1'i tekrar göster.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            teslim panel = new teslim(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
            panel.FormClosed += Panel_FormClosed; // FormClosed olayını burada bağlıyoruz.
            panel.Show();
            this.Hide();
        }
    }
}
