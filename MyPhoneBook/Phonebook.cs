using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;                                                                     //helps us to have access to several tools for use in line 20 down
namespace MyPhoneBook
{
    public partial class Phonebook : Form
    {
        public Phonebook()
        {
            InitializeComponent();
        }
        string conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PhoneBook1DB;Integrated Security=True";
        SqlConnection con = new SqlConnection();                                                  //creating instances of class 
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr;
        DataTable dt = new DataTable();
        private void Phonebook_Load(object sender, EventArgs e)
        {
            clearAll();                                                                 // naming a method clearall and using it down
            LoadData();
        }

        void clearAll()
        {
            txtName.Clear();                                                            //making  sure by default the entries have no parameter
            txtEmail.Clear();
            txtAddress.Clear();
            txtTel.Clear();
            txtSearch.Clear();
            lblID.Text = "";
            txtName.Focus();

            txtSearch.Clear();
            // dgvPhoneBook.ClearSelection();
            //makes sure that when our appliction start running the name textbox is always focus
        }

        object LoadData()
        {

            string query = "Select * From tblPhonebook1";                            //to load data to our data grid view
            con = new SqlConnection(conString);                                              // creating a connection to our database 
            con.Open();
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);                                                                    //send all the infor from da into dt

            if (dt.Rows.Count > 0)
            {
                return dgvPhoneBook.DataSource = dt;

            }
            else
            {
                dt = null;
                return dt;
            }

        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtEmail.Text == "" || txtAddress.Text == "" || txtTel.Text == "")                  // this make sure users write all their information in the textbox
            {
                MessageBox.Show("Parameter missing", "status update", MessageBoxButtons.OK, MessageBoxIcon.Stop);    //here a box shows showing the missing parameter and the type of icon to show the error
            }
            else
            {
                string query = "Insert into tblPhoneBook1(Name,Tel,Email,Address) Values('" + txtName.Text + "','" + txtTel.Text + "','" + txtEmail.Text + "','" + txtAddress.Text + "')";            // allow users acces the dataBase using the insert query
                con = new SqlConnection(conString);                                                  //sqlConnection string is used to take the conString to be use
                con.Open();                                                                           // here we want to open the con 

                cmd = new SqlCommand(query, con);                                                      // Sqlcommand takes two paramaters query and con

                int i = cmd.ExecuteNonQuery();                                                      // manipulate the data, execute the data and store the result in the integer i

                if (i > 0)                                                                            //chech if i is greater than 0 and if it is the case it means data has been entered into our system for processing and now we can copy our query section
                {
                    MessageBox.Show("Successful insertion", "status update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else                                                                                // here i is not greater than 0
                {
                    MessageBox.Show("Error!", "status update", MessageBoxButtons.OK, MessageBoxIcon.Error);    //here a box shows showing the missing parameter and the type of icon to show the error            
                }
                clearAll();
                LoadData();
            }
        }

        private void btnDelelte_Click(object sender, EventArgs e)               // method to delete
        {
            {
                if (lblID.Text == "")
                {
                    MessageBox.Show("Null Parameter", "status update", MessageBoxButtons.OK, MessageBoxIcon.Information);    //here a box shows showing the missing parameter and the type of icon to show the error
                }
                else
                {
                    string query = " Delete From tblPhoneBook1 where PhoneBook1ID='" + lblID.Text + "'";
                    con = new SqlConnection(conString);                                                  //sqlConnection string is used to take the conString to be use
                    con.Open();                                                                           // here we want to open the con 

                    cmd = new SqlCommand(query, con);                                                      // Sqlcommand takes two paramaters query and con

                    int i = cmd.ExecuteNonQuery();                                                      // manipulate the data, execute the data and store the result in the integer i

                    if (i > 0)                                                                             //chech if i is greater than 0 and if it is the case it means data has been entered into our system for processing and now we can copy our query section
                    {
                        MessageBox.Show("Sucessful Delete", "status update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else                                                                                // here i is not greater than 0
                    {
                        MessageBox.Show("Failed Delet", "status update", MessageBoxButtons.OK, MessageBoxIcon.Error);    //here a box shows showing the missing parameter and the type of icon to show the error            
                    }
                    clearAll();                                                                                      // clear all the row of the data being deleted
                    LoadData();                                                                                     // after our successful delition the dat shoudl be loaded
                }

            }
        }

        private void dgvPhoneBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = dgvPhoneBook.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = dgvPhoneBook.SelectedRows[0].Cells[1].Value.ToString();
            txtTel.Text = dgvPhoneBook.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail.Text = dgvPhoneBook.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dgvPhoneBook.SelectedRows[0].Cells[4].Value.ToString();

            //clearAll();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            {
                {
                    if (lblID.Text == "")
                    {
                        MessageBox.Show("Null Parameter", "status update", MessageBoxButtons.OK, MessageBoxIcon.Information);    //here a box shows showing the missing parameter and the type of icon to show the error
                    }
                    else
                    {
                        string query = "Update tblPhoneBook1 set Name='" + txtName.Text + "', Tel='" + txtTel.Text + "', Email='" + txtSearch.Text + "', Address='" + txtAddress.Text + "'where PhoneBook1ID='" + lblID.Text + "'";
                        con = new SqlConnection(conString);                                                  //sqlConnection string is used to take the conString to be use
                        con.Open();                                                                           // here we want to open the con 

                        cmd = new SqlCommand(query, con);                                                      // Sqlcommand takes two paramaters query and con

                        int i = cmd.ExecuteNonQuery();                                                      // manipulate the data, execute the data and store the result in the integer i

                        if (i > 0)                                                                             //chech if i is greater than 0 and if it is the case it means data has been entered into our system for processing and now we can copy our query section
                        {
                            MessageBox.Show("Sucessful Update", "status update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        else                                                                                // here i is not greater than 0
                        {
                            MessageBox.Show("Failed Delet", "status update", MessageBoxButtons.OK, MessageBoxIcon.Error);    //here a box shows showing the missing parameter and the type of icon to show the error            
                        }
                        clearAll();                                                                                      // clear all the row of the data being deleted
                        LoadData();                                                                                     // after our successful delition the dat shoudl be loaded
                    }

                }
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text=="")
            {
                MessageBox.Show("Null parameter!","status update", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            else
            {
                 string query = "Select * From tblPhonebook1 where Name='"+ txtSearch.Text +"'";                            //to load data to our data grid view
                 con = new SqlConnection(conString);                                              // creating a connection to our database 
                 con.Open();
                 cmd = new SqlCommand(query, con);
                 da = new SqlDataAdapter(cmd);
                 dt = new DataTable();
                 da.Fill(dt);                                                                    //send all the infor from da into dt

                if (dt.Rows.Count > 0)
                {
                    dgvPhoneBook.DataSource = dt;
                    MessageBox.Show("Search Found", "status update", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
                else
                {
                    LoadData();
                    MessageBox.Show("Search not Found", "status update", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }


              

            }
         }
    }
}

