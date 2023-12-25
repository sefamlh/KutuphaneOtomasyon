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
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ogrenciList panel = new ogrenciList(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
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
            kitapList panel = new kitapList(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
            panel.FormClosed += Panel_FormClosed; // FormClosed olayını burada bağlıyoruz.
            panel.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            oduncteslim panel = new oduncteslim(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
            panel.FormClosed += Panel_FormClosed; // FormClosed olayını burada bağlıyoruz.
            panel.Show();
            this.Hide();
        }
    }
}
