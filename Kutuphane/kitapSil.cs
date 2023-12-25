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
    public partial class kitapSil : Form
    {
        public kitapSil()
        {
            InitializeComponent();
        }

        private void kitapSil_Load(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM kitaplar";
                    MySqlCommand command = new MySqlCommand(query, connection);
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
                string kitapId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM kitaplar WHERE kitapId = @kitapId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@kitapId", kitapId);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Kitap başarıyla silindi.");
                            // Burada DataGridView'den de silinmiş öğrenciyi kaldırabilirsiniz.
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        }
                        else
                        {
                            MessageBox.Show("Kitap silinirken bir hata oluştu.");
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
                MessageBox.Show("Lütfen silmek istediğiniz kitabı seçin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string arananKitap = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(arananKitap))
            {
                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "SELECT * FROM kitaplar WHERE kitapAd LIKE @kitapAd";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@kitapAd", "%" + arananKitap + "%");

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
            else
            {
                string connectionString = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "SELECT * FROM kitaplar";
                        MySqlCommand command = new MySqlCommand(query, connection);
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
        }
    }
}
