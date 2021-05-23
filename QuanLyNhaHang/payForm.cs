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
    public partial class payForm : Form
    {
        public payForm()
        {
            InitializeComponent();
        }

        public string maban;
        public string[] strMonAn = new string[30];
        public string[] strSoLuong = new string[30];
        public string[] strGiaTien = new string[30];

        SqlConnection connection;
        SqlCommand command;
        //SqlDataAdapter adapter = new SqlDataAdapter();
        //DataSet dataMenu = new DataSet();

        private void payForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString.connectionString);
            connection.Open();
            int tongtien = 0;

            lblMaBan.Text = "Bàn " + maban;
            for (int i = 0; i < strMonAn.Length; i++)
            {
                rtxtMonAn.AppendText(strMonAn[i] + "\n");
                rtxtSoLuong.AppendText(strSoLuong[i] + "\n");
                rtxtGiaTien.AppendText(strGiaTien[i] + "\n");

                tongtien += int.Parse(strGiaTien[i]) * int.Parse(strSoLuong[i]);

                if (strMonAn[i + 1] == null)
                    break;
            }
            txtTongTien.Text = tongtien + "";
        }

        private void txtTienTra_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                int tientra = int.Parse(txtTienTra.Text);
                int tongtien = int.Parse(txtTongTien.Text);
                if (tongtien > tientra)
                {
                    MessageBox.Show("Không đủ tiền trả");
                    return;
                }
                txtTienThua.Text = (tientra - tongtien) + "";
            }
        }

        private void btXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (txtTienThua.Text == "")
            {
                MessageBox.Show("Chưa thanh toán");
                return;
            }
            command = connection.CreateCommand();
            command.CommandText = "update ThongTinTamThoi set TrangThaiThanhToan = 1 where MaBan ='" + maban + "'";
            command.ExecuteNonQuery();
            this.Close();
        }

        private void txtTienTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
