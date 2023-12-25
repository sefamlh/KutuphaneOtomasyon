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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kutuphane
{
    public partial class kitapEkle : Form
    {
        public kitapEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kitapAd = textBox1.Text.Trim();
            string kitapYazar = textBox2.Text.Trim();
            string kitapYayınevi = textBox3.Text.Trim();
            string kitapSayfasayisi = textBox4.Text.Trim();

            if (!string.IsNullOrEmpty(kitapAd) && !string.IsNullOrEmpty(kitapYazar) && !string.IsNullOrEmpty(kitapYayınevi) && !string.IsNullOrEmpty(kitapSayfasayisi))
            {
                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        mySqlConnection.Open();
                        string query = "INSERT INTO kitaplar (kitapAd, kitapYazar, kitapYayınevi, kitapSayfasayisi) VALUES (@kitapAd, @kitapYazar, @kitapYayınevi, @kitapSayfasayisi)";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);

                        command.Parameters.AddWithValue("@kitapAd", kitapAd);
                        command.Parameters.AddWithValue("@kitapYazar", kitapYazar);
                        command.Parameters.AddWithValue("@kitapYayınevi", kitapYayınevi);
                        command.Parameters.AddWithValue("@kitapSayfasayisi", kitapSayfasayisi);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Kitap başarıyla eklendi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bilgileri doldurmadan ekleme işlemi başarılı olmaz!");
            }
        }
    }
}
