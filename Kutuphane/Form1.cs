using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=kutuphane; password=sefa1234";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            
            try
            {
                mySqlConnection.Open();
                string kullaniciAdi = Convert.ToString(textBox1.Text);
                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username = @kullaniciAdi", mySqlConnection);
                command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                MySqlDataReader reader = command.ExecuteReader();
                

                if (reader.Read())
                {
                    string pass = Convert.ToString(reader["kullanici_pass"]);
                    string reqPass = Convert.ToString(textBox2.Text);
                    if (pass == reqPass)
                    {
                        MessageBox.Show("Giriş başarılı yönlendiriliyorsunuz...");
                        LoginSuccess();
                    }
                    else
                    {
                        MessageBox.Show("Yanlış Şifre!");
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
        public void LoginSuccess()
        {
            Panel panel = new Panel(); // Burada 'Panel', yeni açılacak formunuzun adıdır.
            panel.FormClosed += Panel_FormClosed; // FormClosed olayını burada bağlıyoruz.
            panel.Show();
            this.Hide();
        }
        public void Panel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Panel kapandığında Form1'i tekrar göster.
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox2.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
