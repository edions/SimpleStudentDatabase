using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentDatabaseApp
{
    public partial class Main : Form
    {
        private readonly SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Acer\\source\\repos\\CRUD-App\\Database1.mdf;Integrated Security=True");
        int ID = 0;

        public Main()
        {
            InitializeComponent();
            comboBox1.Items.Add("(CTE) College of Teacher Education");
            comboBox1.Items.Add("(COC) College of Commerce");
            comboBox1.Items.Add("(CJE) College of Criminal Justice Education");
            comboBox1.Items.Add("(CCS) College of Computer Studies");
            comboBox1.Items.Add("(Psych) Psychology Department");
        }

        //Get data from Database
        public void DisplayData()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from StudentDB ORDER BY Id DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        //Clear Data from textbox
        private void ClearData()
        {
            textBox1.Text = string.Empty;
            comboBox1.Text = string.Empty;
        }

        //Display Data on formload
        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        //Get the selected data from dataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
            }
        }

        //INSERT Data button
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into StudentDB values('" + textBox1.Text + "','" + comboBox1.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            ClearData();
            DisplayData();
            MessageBox.Show("Record Inserted");
        }

        //DELETE Data button
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete this item ?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from StudentDB where name='" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                ClearData();
                DisplayData();
                MessageBox.Show("Record Deleted");
            }
        }

        //UPDATE Data button
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update StudentDB set name='" + textBox1.Text + "',course='" + comboBox1.Text + "'where id=@id";
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.ExecuteNonQuery();
            con.Close();
            ClearData();
            DisplayData();
            MessageBox.Show("Record Updated");
        }
    }
}
