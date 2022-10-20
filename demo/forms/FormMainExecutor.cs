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

namespace demo.forms
{
    public partial class FormMainExecutor : Form
    {
        int ID = 0;
        public FormMainExecutor(int global_id)
        {
            ID = global_id;
            InitializeComponent();
            string sql = @"Select ExecutorID, Title, Status, [Manager].ID  From [Task] "
                + "Inner join [Executor] on [Task].ExecutorID = [Executor].ID "
                + "Inner join [Manager] on [Executor].ManagerID = [Manager].ID "
                + "where ExecutorID = " + ID + " "
                + "Order by CreateDatetime DESC;";
            PopulateGrid(sql);

            using (SqlConnection conn = new SqlConnection(Modules.settings.connection))
            {
                string sql_cb = "Select distinct status from [Task]";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_cb, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CB.Items.Add(dr["status"].ToString());
                }
            }

        }

        private void PopulateGrid(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Modules.settings.connection))
            {
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                ada.Fill(ds);
                DGV.DataSource = ds.Tables[0];
            }
        }

        private void FormMainExecutor_Load(object sender, EventArgs e)
        {

        }

        private void CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = @"Select ExecutorID, Title, Status, [Manager].ID  From [Task] "
                + "Inner join [Executor] on [Task].ExecutorID = [Executor].ID "
                + "Inner join [Manager] on [Executor].ManagerID = [Manager].ID "
                + "where ExecutorID = " + ID + " and Status = '" + CB.Text + "'"
                + "Order by CreateDatetime DESC;";
            PopulateGrid(sql);
        }
    }
}
