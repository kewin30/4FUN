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
    public partial class Rented : Form
    {
        public Rented()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kewin\Documents\WypozyczalniaProjektDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void wyswietl()
        {
            con.Open();
            string query = "select * from Wypozycz";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void UzupelnijVin()
        {
            con.Open();
            string zapytanie = "SELECT Vin FROM Auto WHERE Dostepny='" + "Tak" + "'";
            SqlCommand cmd = new SqlCommand(zapytanie,con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Vin",typeof(string));
            dt.Load(rdr);
            cbVin.ValueMember = "Vin";
            cbVin.DataSource = dt;
            con.Close();
        }
        private void ZmienDostepnosc()
        {
            con.Open();
            string zapytanie = "UPDATE Auto set Dostepny='" + "NIE" + "' where Vin='" + cbVin.Text.ToString() + "';";
            SqlCommand cmd = new SqlCommand(zapytanie, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void ZmienDostepnoscNaTak()
        {
            con.Open();
            string zapytanie = "UPDATE Auto set Dostepny='" + "TAK" + "' where Vin='" + cbVin.Text.ToString() + "';";
            SqlCommand cmd = new SqlCommand(zapytanie, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void UzupelnijPesel()
        {
            con.Open();
            string zapytanie = "SELECT Pesel FROM Tabela";
            SqlCommand cmd = new SqlCommand(zapytanie, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Pesel", typeof(Int64));
            dt.Load(rdr);
            cbPesel.ValueMember = "Pesel";
            cbPesel.DataSource = dt;
            con.Close();
        }
        private void znajdzImie()
        {
            con.Open();
            string zapytanie = "SELECT * FROM Tabela WHERE Pesel=" + cbPesel.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(zapytanie, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                txtImie.Text = item["Imie"].ToString();
            }
            con.Close();
        }
        private void Rented_Load(object sender, EventArgs e)
        {
            UzupelnijVin();
            UzupelnijPesel();
            wyswietl();
        }

        private void cbVin_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cbPesel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            znajdzImie();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtImie.Text == "" || txtNalezne.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    con.Open();
                    string zapytanie = "Insert into Wypozycz values(" + txtId.Text + ",'" + cbVin.SelectedValue.ToString()+ "','" + txtImie.Text + "','" + DataOd.Text + "','" + DataDo.Text+ "',"+txtNalezne.Text+")";
                    SqlCommand cmd = new SqlCommand(zapytanie, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Auto zostalo wypozyczone");
                    con.Close();
                    ZmienDostepnosc();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Brakuje numeru ID");
            }
            else
            {
                try
                {
                    con.Open();
                    string zapytanie = "DELETE from Wypozycz where Id='" + txtId.Text + "';";
                    SqlCommand cmd = new SqlCommand(zapytanie, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usunieto pomyslnie");
                    con.Close();
                    wyswietl();
                    ZmienDostepnoscNaTak();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            cbVin.Text = dataGridView1.CurrentRow.Cells["Vin"].Value.ToString();
            txtImie.Text = dataGridView1.CurrentRow.Cells["Imie"].Value.ToString();
            txtNalezne.Text = dataGridView1.CurrentRow.Cells["Nalezne"].Value.ToString();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }
    }
}
