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

namespace QuanLyNhaHang
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        //SqlDataAdapter adapter = new SqlDataAdapter();
        //DataSet dataNhanVien = new DataSet();

        //const string userAdmin = "admin";
        //const string passwordAdmin = "admin123";
        //const string userNhanVien = "staff";
        //const string passwordNhanVien = "staff123";

        private void btLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text!="" && txtPassword.Text != "")
            {
                mainscreenForm mainscreenForm = new mainscreenForm();

                string username = txtUsername.Text.ToString();
                string password = txtPassword.Text.ToString();

                connection = new SqlConnection(ConnectionString.connectionString);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "select COUNT(*) from NhanVien where TenDangNhap = '" + username +
                    "' and MatKhauDangNhap = '" + password + "'";

                if ((int)command.ExecuteScalar() == 0)
                {
                    MessageBox.Show("Login fail");
                    return;
                }
                command.CommandText = "select nv.MaNhanVien from NhanVien nv where nv.TenDangNhap = '"+username+"'";
                string manhanvien = command.ExecuteScalar().ToString();
                if (manhanvien == "nv1")
                {
                    mainscreenForm.manhanvien = "admin";
                }
                else
                {
                    mainscreenForm.manhanvien = manhanvien;
                }
                this.Hide();
                mainscreenForm.ShowDialog();
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void loginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
