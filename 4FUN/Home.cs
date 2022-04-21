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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kewin\Desktop\4FUN\4FUN\WypozyczalniaProjektDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            this.Hide();
            Vehicle vehicle = new Vehicle();
            vehicle.Show(); 
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients clients = new Clients();
            clients.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rented rented = new Rented();
            rented.Show();  
        }

        private void Home_Load(object sender, EventArgs e)
        {
            string zapytanieAuto = "SELECT COUNT(*) from Auto";
            string zapytanieKlienci = "SELECT COUNT(*) from Tabela";
            string zapytanieDostepny = "SELECT COUNT(*) from Auto WHERE Dostepny = 'TAK'";
            SqlDataAdapter sda = new SqlDataAdapter(zapytanieAuto, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(zapytanieKlienci, con);
            SqlDataAdapter sda2 = new SqlDataAdapter(zapytanieDostepny, con);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            sda.Fill(dt);
            sda1.Fill(dt1);
            sda2.Fill(dt2);

            lblAuta.Text = @"Ilość aut 
w naszym systemie:
"+dt.Rows[0][0].ToString();
            lblKlienci.Text = @"Ilość klientów: 
" + dt1.Rows[0][0].ToString();
            lblDostepnosc.Text = @"Ilość dostępnych aut:
"+dt2.Rows[0][0].ToString();

        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }
    }
}
