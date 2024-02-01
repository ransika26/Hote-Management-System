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

namespace Hotel_Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountRooms();
            CountCustomers();
            SumAmount();
            GetCustomers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\HotelDbase\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountRooms()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from RoomTbl",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            RoomsLbl.Text = dt.Rows[0][0].ToString()+"  Rooms";
            Con.Close();
        }
        private void CountCustomers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from CustomerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustLbl.Text = dt.Rows[0][0].ToString() + "  Customers";
            Con.Close();
        }
        private void SumAmount()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(Cost) from BookingTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookingLbl.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void SumDaily()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(Cost) from BookingTbl where BookDate='"+BDate.Value.Date+"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DIncomeLbl.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumByCustomer()
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select sum(Cost) from BookingTbl where Customer=" + CustomerCb.SelectedValue.ToString() + "", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                IncomeByCustLbl.Text = "Rs " + dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
            
        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(rdr);
            CustomerCb.ValueMember = "CustNum";
            CustomerCb.DataSource = dt;
            Con.Close();

        }
        private void BDate_ValueChanged(object sender, EventArgs e)
        {
            SumDaily();
        }

        private void CustomerCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SumByCustomer();
        }

        private void label6_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void label5_MouseClick(object sender, MouseEventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
