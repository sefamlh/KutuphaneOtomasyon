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
    public partial class ogrenciEkle : Form
    {
        public ogrenciEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string ogrenciAdi = textBox2.Text.Trim();
            string ogrenciSoyadi = textBox3.Text.Trim();
            string ogrenciSinif = textBox4.Text.Trim();
            string ogrenciOkulNo = textBox5.Text.Trim();
            string ogrenciSifre = textBox6.Text.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(ogrenciAdi) && !string.IsNullOrEmpty(ogrenciSoyadi) && !string.IsNullOrEmpty(ogrenciSinif) && !string.IsNullOrEmpty(ogrenciOkulNo) && !string.IsNullOrEmpty(ogrenciSifre))
            {
                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        mySqlConnection.Open();
                        string query = "INSERT INTO users (username, kullanici_ad, kullanici_soyad, kullanici_sinif, kullanici_no, kullanici_pass) VALUES (@username, @ogrenciAdi, @ogrenciSoyadi, @ogrenciSinif, @ogrenciOkulNo, @ogrenciSifre)";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);

                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@ogrenciAdi", ogrenciAdi);
                        command.Parameters.AddWithValue("@ogrenciSoyadi", ogrenciSoyadi);
                        command.Parameters.AddWithValue("@ogrenciSinif", ogrenciSinif);
                        command.Parameters.AddWithValue("@ogrenciOkulNo", ogrenciOkulNo);
                        command.Parameters.AddWithValue("@ogrenciSifre", ogrenciSifre);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Öğrenci başarıyla eklendi.");
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
