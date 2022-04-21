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

namespace _4FUN
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kewin\Documents\WypozyczalniaProjektDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void wyswietl()
        {
            con.Open();
            string query = "select * from Tabela";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtImie.Text == "" || txtNazwisko.Text == "" || txtEmail.Text == "" || txtTelefon.Text == "" || txtAddress.Text == "" || txtPesel.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    con.Open();
                    string zapytanie = $@"Insert into Tabela values({txtPesel.Text},'{txtImie.Text}','{txtNazwisko.Text}','{txtEmail.Text}','{txtTelefon.Text}','{txtAddress.Text}')";
                    SqlCommand cmd = new SqlCommand(zapytanie, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uzytkownik dodany pomyslnie");
                    con.Close();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            wyswietl();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtPesel.Text=="")
            {
                MessageBox.Show("Brakuje PESEL'u");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from Tabela where Pesel=" + txtPesel.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uzytkownik skasowany.");
                    con.Close();
                    wyswietl();
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtImie.Text == "" || txtNazwisko.Text == "" || txtEmail.Text == "" || txtPesel.Text == "" || txtTelefon.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    con.Open();
                    string zapytanie = "UPDATE Tabela set Imie='" + txtImie.Text + "', Nazwisko='" + txtNazwisko.Text + "', Email='" + txtEmail.Text + "', telefon='" + txtTelefon.Text + "', Adres='" + txtAddress.Text + "'where Pesel=" + txtPesel.Text + ";";
                    SqlCommand cmd = new SqlCommand(zapytanie, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uzytkownik zmieniony pomyslnie");
                    con.Close();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }
    }
}
