using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace DisconnectedStudent
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

        }
        private DataSet GetAllStudents()
        {
            da = new SqlDataAdapter("Select * from StudentNew", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds= new DataSet();
            da.Fill(ds, "StudentNew");
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds= GetAllStudents();
                DataRow row = ds.Tables["StudentNew"].NewRow();
                row["rollno"] = txtrollno.Text;
                row["name"] = txtname.Text;
                row["percentage"] = txtpercentage.Text;
                ds.Tables["StudentNew"].Rows.Add(row);
                int res = da.Update(ds.Tables["StudentNew"]);
                if (res >= 1)
                {
                    MessageBox.Show("Record Inserted");

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds= GetAllStudents();
                DataRow row = ds.Tables["Studentnew"].Rows.Find(txtid.Text);
                if(row != null)
                {
                    txtrollno.Text = row["rollno"].ToString();
                    txtname.Text = row["name"].ToString();
                    txtpercentage.Text = row["percentage"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds=GetAllStudents();
                DataRow row = ds.Tables["StudentNew"].Rows.Find(txtid.Text);
                if(row != null)
                {
                    row["rollno"] = txtrollno.Text;
                    row["name"] = txtname.Text;
                    row["percentage"]=txtpercentage.Text;
                }
                else
                {
                    MessageBox.Show("Record Updated");
                }



            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds=GetAllStudents();
                DataRow row = ds.Tables["StudentNew"].Rows.Find(txtid.Text);
                if(row != null)
                {
                    row.Delete();
                    int res = da.Update(ds.Tables["StudentNew"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record Deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnshowstudents_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllStudents();
                dataGridView1.DataSource = ds.Tables["StudentNew"];

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
