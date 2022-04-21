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
    public partial class Vehicle : Form
    {
        public Vehicle()
        {
            InitializeComponent();
        }
       
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kewin\Documents\WypozyczalniaProjektDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void wyswietl()
        {
            con.Open();
            string query = "select * from Auto";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string raddio = "";
            if (rdbYes.Checked)
            {
                raddio = rdbYes.Text;
            }
            else if (rdbNo.Checked)
            {
                raddio = rdbNo.Text;
            }

                if (txtVin.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("Brakuje informacji");
                }
                else
                {
                    try
                    {
                        con.Open();
                        var zapytanie = "Insert into Auto values('" + txtVin.Text + "','" + txtBrand.Text + "','" + txtModel.Text + "','" + txtPrice.Text + "','" + raddio + "')";
                        //string zapytanie = $@"Insert into Auto values({txtPrice.Text},'{txtBrand.Text}','{txtModel.Text}','{txtVin.Text}','{raddio}')";
                        SqlCommand cmd = new SqlCommand(zapytanie, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Auto dodane pomyslnie");
                        con.Close();
                        wyswietl();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            

        }

        private void Vehicle_Load(object sender, EventArgs e)
        {
            wyswietl();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtVin.Text=="")
            {
                MessageBox.Show("Brakuje numeru VIN");
            }
            else
            {
                try
                {
                    con.Open();
                    string zapytanie = "DELETE from Auto where Vin='" + txtVin.Text + "';";
                    SqlCommand cmd = new SqlCommand(zapytanie, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usunieto pomyslnie");
                    con.Close();
                    wyswietl();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string raddio = "";
            if (rdbYes.Checked)
            {
                raddio = rdbYes.Text;
            }
            else if (rdbNo.Checked)
            {
                raddio = rdbNo.Text;
            }

            if (txtVin.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    con.Open();
                    string zapytanie = "UPDATE Auto set Marka='" + txtBrand.Text + "', Model='" + txtModel.Text + "', Cena='" + txtPrice.Text + "', Dostepny='" + raddio + "'where Vin='" + txtVin.Text + "';";
                    SqlCommand cmd = new SqlCommand(zapytanie, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Auto zmienione pomyslnie");
                    con.Close();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtVin.Text = dataGridView1.CurrentRow.Cells["Vin"].Value.ToString();
            txtBrand.Text = dataGridView1.CurrentRow.Cells["Marka"].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells["Model"].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells["Cena"].Value.ToString();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }
    }
}
