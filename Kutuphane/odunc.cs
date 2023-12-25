using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class odunc : Form
    {
        public odunc()
        {
            InitializeComponent();
        }

        private void odunc_Load(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT u.oduncId, us.kullanici_ad, us.kullanici_soyad, k.kitapAd FROM odunc u inner join users us on us.kullanici_Id = u.oduncAlanKullanici inner join kitaplar k on u.oduncAldigiKitap = k.kitapId where u.oduncDurumu = 1", mySqlConnection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    DoldurOgrenciNumaralari();
                    DoldurKitapIsimleri();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void DoldurOgrenciNumaralari()
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT kullanici_no FROM users ORDER BY kullanici_no";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["kullanici_no"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void DoldurKitapIsimleri()
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT kitapAd FROM kitaplar ORDER BY kitapAd";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader["kitapAd"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir öğrenci numarası seçin.");
                return; // Eğer seçim yapılmadıysa işlemi burada sonlandır
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir kitap adı seçin.");
                return; // Eğer seçim yapılmadıysa işlemi burada sonlandır
            }
            string secilenOgrenciNo = comboBox1.SelectedItem.ToString();
            string secilenKitapIsmi = comboBox2.SelectedItem.ToString();

            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcı ID'sini çek
                    string dbKID = "SELECT kullanici_id FROM users WHERE kullanici_no = @ogrenciNo";
                    MySqlCommand commandKullanici = new MySqlCommand(dbKID, connection);
                    commandKullanici.Parameters.AddWithValue("@ogrenciNo", secilenOgrenciNo);
                    var kullaniciId = commandKullanici.ExecuteScalar();

                    // Kitap ID'sini çek
                    string dbKI = "SELECT kitapId FROM kitaplar WHERE kitapAd = @kitapAd";
                    MySqlCommand commandKitap = new MySqlCommand(dbKI, connection);
                    commandKitap.Parameters.AddWithValue("@kitapAd", secilenKitapIsmi);
                    var kitapId = commandKitap.ExecuteScalar();
                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                        if (kullaniciId != null && kitapId != null)
                    {
                        // Ödünç verme kaydını yap
                        string query = "INSERT INTO odunc (oduncAlanKullanici, oduncAldigiKitap, oduncAldigiTarih, oduncDurumu) VALUES (@ogrenciId, @kitapId, @odunAldigiTarih, true)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ogrenciId", kullaniciId);
                        command.Parameters.AddWithValue("@kitapId", kitapId);
                        command.Parameters.AddWithValue("@odunAldigiTarih", DateTime.Now);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kitap ödünç verme işlemi başarılı.");
                        mySqlConnection.Open();
                            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT u.oduncId, us.kullanici_ad, us.kullanici_soyad, k.kitapAd FROM odunc u inner join users us on us.kullanici_Id = u.oduncAlanKullanici inner join kitaplar k on u.oduncAldigiKitap = k.kitapId where u.oduncDurumu = 1", mySqlConnection);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    else
                    {
                        MessageBox.Show("Kullanıcı veya kitap bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
