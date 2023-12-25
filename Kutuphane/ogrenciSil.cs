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
    public partial class ogrenciSil : Form
    {
        public ogrenciSil()
        {
            InitializeComponent();
        }

        private void ogrenciSil_Load(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT u.kullanici_sinif FROM users u GROUP BY u.kullanici_sinif"; // 'sinifAdi', sinif isimlerini tutan sütunun adı
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["kullanici_sinif"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilenSinif = comboBox1.SelectedItem.ToString();
            ListeleOgrenciler(secilenSinif);
        }
        private void ListeleOgrenciler(string sinif)
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT kullanici_id,kullanici_ad,kullanici_soyad,kullanici_sinif,kullanici_no FROM users WHERE kullanici_sinif = @sinif";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@sinif", sinif);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırdaki öğrenci ID'sini al
                string ogrenciId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM users WHERE kullanici_id = @ogrenciId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ogrenciId", ogrenciId);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Öğrenci başarıyla silindi.");
                            // Burada DataGridView'den de silinmiş öğrenciyi kaldırabilirsiniz.
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                            comboBox1.Items.Clear();
                            string query2 = "SELECT u.kullanici_sinif FROM users u GROUP BY u.kullanici_sinif"; // 'sinifAdi', sinif isimlerini tutan sütunun adı
                            MySqlCommand command2 = new MySqlCommand(query2, connection);
                            MySqlDataReader reader = command2.ExecuteReader();

                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["kullanici_sinif"].ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Öğrenci silinirken bir hata oluştu.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz öğrenciyi seçin.");
            }
        }
    }
}
