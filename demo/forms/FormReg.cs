using demo.forms;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    public partial class FormReg : Form
    {
        public FormReg()
        {
            InitializeComponent();
        }

        private void FormReg_Load(object sender, EventArgs e)
        {

        }
        public static string GetHash(string passwd)
        {
            using (var hash = SHA256.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(passwd)));
            }
        }
        private void btn_enter_Click(object sender, EventArgs e)
        {
            string sql = @"Select [ID], [Password] from [User] where Login = @login";
            string pwd = "";
            string pwd_enter = GetHash(tb_passwd.Text);
            int id = 0;

            using (SqlConnection conn = new SqlConnection(Modules.settings.connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("login", tb_login.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["ID"]);
                    pwd = dr["Password"].ToString();
                }
            }

            if (pwd == pwd_enter)
            {
                sql = @"Select [ID] from [Executor]";
                int id_t = 0;
                bool Executor = false;
                using (SqlConnection conn = new SqlConnection(Modules.settings.connection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        id_t = Convert.ToInt32(dr["ID"]);
                        if (id == id_t)
                        {
                            Executor = true;
                        }
                    }
                }
                if (Executor)
                {
                    FormMainExecutor formMain = new FormMainExecutor(id);
                    formMain.Show();
                }
                else
                {
                    FormMainManager formMainManager = new FormMainManager(id);
                    formMainManager.Show();
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль введен неверно");
            }
        }
    }
}
