using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class teslim : Form
    {
        public teslim()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ogrenciNo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(ogrenciNo))
            {
                MessageBox.Show("Lütfen bir okul numarası girin.");
                return;
            }

            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Öğrenci ID'sini bul
                    string ogrenciIdSorgu = "SELECT kullanici_id FROM users WHERE kullanici_no = @ogrenciNo";
                    MySqlCommand ogrenciIdCommand = new MySqlCommand(ogrenciIdSorgu, connection);
                    ogrenciIdCommand.Parameters.AddWithValue("@ogrenciNo", ogrenciNo);
                    var ogrenciId = ogrenciIdCommand.ExecuteScalar();

                    if (ogrenciId != null)
                    {
                        // Öğrencinin ödünç aldığı kitapları listele
                        // "SELECT u.oduncId, us.kullanici_ad, us.kullanici_soyad, k.kitapAd FROM odunc u inner join users us on u.oduncAlanKullanici = us.kullanici_id inner join kitaplar k on k.kitapId = u.oduncAldigiKitap WHERE u.oduncAlanKullanici = @ogrenciId AND u.oduncDurumu = 1
                        string kitapSorgu = "SELECT * FROM odunc u WHERE u.oduncAlanKullanici = @ogrenciId AND u.oduncDurumu = 1";
                        MySqlDataAdapter adapter = new MySqlDataAdapter(kitapSorgu, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Bu okul numarasına sahip bir öğrenci bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string oduncId = dataGridView1.SelectedRows[0].Cells["oduncId"].Value.ToString(); // "kitapIdSutunu" gerçek sütun adınızla değiştirin

                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE odunc SET oduncDurumu = 0, teslimTarihi = @teslimTarihi WHERE oduncId = @oduncId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@oduncId", oduncId);
                        command.Parameters.AddWithValue("@teslimTarihi", DateTime.Now);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kitap başarıyla teslim edildi.");

                        // DataGridView'den ilgili satırı kaldır
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen teslim edilecek kitabı seçin.");
            }
        }
    }
}
