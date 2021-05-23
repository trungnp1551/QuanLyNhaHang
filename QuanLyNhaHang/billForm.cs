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
    public partial class billForm : Form
    {
        public billForm()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet dataHD = new DataSet();
        DataSet dataCTHD = new DataSet();

        void loadDataHD()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from HoaDon order by ThoiGian";
            adapter.SelectCommand = command;
            dataHD.Clear();
            adapter.Fill(dataHD);
            dataGridViewHD.DataSource = dataHD.Tables[0];
        }

        void loadDataCTHD(string mahoadon)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from ChiTietHoaDon where MaHoaDon = '" + mahoadon + "'";
            adapter.SelectCommand = command;
            dataCTHD.Clear();
            adapter.Fill(dataCTHD);
            dataGridViewCTHD.DataSource = dataCTHD.Tables[0];
        }

        private void billForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString.connectionString);
            connection.Open();
            loadDataHD();
        }

        private void dataGridViewHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGridViewHD.Rows[e.RowIndex];

            string mahoadon = row.Cells["HDColumnMaHoaDon"].Value.ToString();
            lblTotal.Text = row.Cells["HDColumnTongHoaDon"].Value.ToString() + " VNĐ";
            loadDataCTHD(mahoadon);
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            string ngay = dtmNgay.Text;
            string manhanvien;
            string giatrihoadon;

            if (chkNgay.Checked == true && chkMaNV.Checked == true && chkGiaTriHD.Checked == true) 
            {
                manhanvien = txtMaNhanVien.Text.ToString();
                giatrihoadon = txtGiaTriHD.Text.ToString();
                command.CommandText = "select * from HoaDon where ThoiGian >'" + ngay + " 0:0:0' " +
                    "and ThoiGian < '" + ngay + " 23:59:59' and MaNhanVien = '" + manhanvien + "' " +
                    "and TongHoaDon>'" + giatrihoadon + "'";

                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;
            }

            if(chkNgay.Checked == true && chkMaNV.Checked == true)
            {
                manhanvien = txtMaNhanVien.Text.ToString();
                command.CommandText = "select * from HoaDon where ThoiGian >'" + ngay + " 0:0:0' " +
                    "and ThoiGian < '" + ngay + " 23:59:59' and MaNhanVien = '" + manhanvien + "'";
                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;
            }
            if(chkNgay.Checked == true && chkGiaTriHD.Checked == true)
            {
                giatrihoadon = txtGiaTriHD.Text.ToString();
                command.CommandText = "select * from HoaDon where ThoiGian >'" + ngay + " 0:0:0' " +
                    "and ThoiGian < '" + ngay + " 23:59:59' and TongHoaDon>'" + giatrihoadon + "'";

                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;
            }

            if(chkMaNV.Checked == true && chkGiaTriHD.Checked == true)
            {
                giatrihoadon = txtGiaTriHD.Text.ToString();
                manhanvien = txtMaNhanVien.Text.ToString();
                command.CommandText = "select * from HoaDon where MaNhanVien = '" + manhanvien + "'" +
                    "and TongHoaDon>'" + giatrihoadon + "'";
                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;
            }

            if (chkNgay.Checked == true)
            {
                command.CommandText = "select * from HoaDon where ThoiGian >'" + ngay + " 0:0:0' " +
                    "and ThoiGian < '" + ngay + " 23:59:59'";
                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;
            }

            if (chkMaNV.Checked == true)
            {
                manhanvien = txtMaNhanVien.Text.ToString();
                command.CommandText = "select * from HoaDon where MaNhanVien = '" + manhanvien + "'";
                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;
            }

            if (chkGiaTriHD.Checked == true)
            {
                giatrihoadon = txtGiaTriHD.Text.ToString();
                command.CommandText = "select * from HoaDon where TongHoaDon>'" + giatrihoadon + "'";
                adapter.SelectCommand = command;
                dataHD.Clear();
                adapter.Fill(dataHD);
                dataGridViewHD.DataSource = dataHD.Tables[0];
                return;

            }

        }

        private void btReset_Click(object sender, EventArgs e)
        {
            dtmNgay.Value = DateTime.Now;
            txtGiaTriHD.Text = "";
            txtMaNhanVien.Text = "";
            loadDataHD();
        }

        private void txtGiaTriHD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
