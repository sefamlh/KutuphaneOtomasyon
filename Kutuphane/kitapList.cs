using MySql.Data.MySqlClient;
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
    public partial class kitapList : Form
    {
        public kitapList()
        {
            InitializeComponent();
        }

        private void kitapList_Load(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM kitaplar", mySqlConnection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kitapEkle panel = new kitapEkle(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
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
            kitapSil panel = new kitapSil(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
            panel.FormClosed += Panel_FormClosed; // FormClosed olayını burada bağlıyoruz.
            panel.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM kitaplar", mySqlConnection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
