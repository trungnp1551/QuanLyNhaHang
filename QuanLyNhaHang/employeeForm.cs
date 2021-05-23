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
    public partial class employeeForm : Form
    {
        public employeeForm()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet dataNhanVien = new DataSet();


        void loadDataNhanVien()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from NhanVien";
            adapter.SelectCommand = command;
            dataNhanVien.Clear();
            adapter.Fill(dataNhanVien);
            dataGridViewNhanVien.DataSource = dataNhanVien.Tables[0];
        }

        private void employeeForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString.connectionString);
            connection.Open();
            loadDataNhanVien();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            txtMSNV.Text = "";
            txtHoTenNV.Text = "";
            cbGioiTinh.Text = "";
            txtCMND.Text = "";
            txtChucVu.Text = "";
            txtLuong.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtTenDN.Text = "";
            txtMatKhauDN.Text = "";
            dtmNgaySinh.Value = DateTime.Now;
            dtmNgayVaoLam.Value = DateTime.Now;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (txtMSNV.Text == "" || txtHoTenNV.Text == "" || cbGioiTinh.Text == ""
                || txtCMND.Text == "" || txtChucVu.Text == "" || txtLuong.Text == ""
                || txtSDT.Text == "" || txtDiaChi.Text == "" || txtEmail.Text == ""
                || txtTenDN.Text == "" || txtMatKhauDN.Text == "") 
            {
                MessageBox.Show("Điền đầy đủ thông tin");
                return;
            }
            command = connection.CreateCommand();
            //command.CommandText = "select count(*) from NhanVien";
            //string manv = "nv" + command.ExecuteScalar().ToString();

            string manv = txtMSNV.Text.ToString();
            string hotennv = txtHoTenNV.Text.ToString();
            string gioitinh = cbGioiTinh.Text.ToString();
            string cmnd = txtCMND.Text.ToString();
            string chucvu = txtChucVu.Text.ToString();
            string luong = txtLuong.Text.ToString();
            string sdt = txtSDT.Text.ToString();
            string diachi = txtDiaChi.Text.ToString();
            string email = txtEmail.Text.ToString();
            string ngaysinh = dtmNgaySinh.Text.ToString();
            string ngayvaolam = dtmNgayVaoLam.Text.ToString();
            string tendangnhap = txtTenDN.Text.ToString();
            string mkdangnhap = txtMatKhauDN.Text.ToString();

            command.CommandText = "insert into NhanVien values('" + manv + "',N'" + hotennv + "',N'" +
                gioitinh + "','" + ngaysinh + "','" + cmnd + "',N'" + chucvu + "','" + ngayvaolam + "'," +
                luong + ",'" + sdt + "',N'" + diachi + "','" + email + "','" + tendangnhap + "','" + mkdangnhap + "')";

            try
            {
                command.ExecuteNonQuery();
                loadDataNhanVien();
            }
            catch(Exception)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại");
            }

        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            if (txtMSNV.Text == "" || txtHoTenNV.Text == "" || cbGioiTinh.Text == ""
                || txtCMND.Text == "" || txtChucVu.Text == "" || txtLuong.Text == ""
                || txtSDT.Text == "" || txtDiaChi.Text == "" || txtEmail.Text == ""
                || txtTenDN.Text == "" || txtMatKhauDN.Text == "") 
            {
                MessageBox.Show("Điền đầy đủ thông tin");
                return;
            }
            command = connection.CreateCommand();

            string manv = txtMSNV.Text.ToString();
            string hotennv = txtHoTenNV.Text.ToString();
            string gioitinh = cbGioiTinh.Text.ToString();
            string cmnd = txtCMND.Text.ToString();
            string chucvu = txtChucVu.Text.ToString();
            string luong = txtLuong.Text.ToString();
            string sdt = txtSDT.Text.ToString();
            string diachi = txtDiaChi.Text.ToString();
            string email = txtEmail.Text.ToString();
            string ngaysinh = dtmNgaySinh.Text.ToString();
            string ngayvaolam = dtmNgayVaoLam.Text.ToString();
            string tendangnhap = txtTenDN.Text.ToString();
            string mkdangnhap = txtMatKhauDN.Text.ToString();

            command.CommandText = "update NhanVien set HoTenNhanVien = N'" + hotennv + "',GioiTinh = N'" +
                gioitinh + "',NgaySinh = '" + ngaysinh + "',CMND = '" + cmnd + "',ChucVu =N'" + chucvu +
                "',NgayVaoLam = '" + ngayvaolam + "',LuongThang = '" + luong + "',SoDienThoai = '" +
                sdt + "',DiaChi=N'" + diachi + "',Email = '" + email + "',TenDangNhap = '" + tendangnhap
                + "', MatKhauDangNhap = '" + mkdangnhap + "' where MaNhanVien = '" + manv + "'";
            command.ExecuteNonQuery();
            loadDataNhanVien();

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if(txtMSNV.Text == "")
            {
                MessageBox.Show("Chọn nhân viên cần xóa");
                return;
            }
            string manv = txtMSNV.Text.ToString();
            command = connection.CreateCommand();
            command.CommandText = "delete from NhanVien where MaNhanVien = '" + manv + "'";
            command.ExecuteNonQuery();
            loadDataNhanVien();
            btReset_Click(sender, e);
        }

        private void dataGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGridViewNhanVien.Rows[e.RowIndex];

            txtMSNV.Text = row.Cells["columnMaNhanVien"].Value.ToString();
            txtHoTenNV.Text = row.Cells["columnHoTenNhanVien"].Value.ToString();
            cbGioiTinh.Text = row.Cells["columnGioiTinh"].Value.ToString();
            txtCMND.Text = row.Cells["columnCMND"].Value.ToString();
            txtChucVu.Text = row.Cells["columnChucVu"].Value.ToString();
            txtLuong.Text = row.Cells["columnLuongThang"].Value.ToString();
            txtSDT.Text = row.Cells["columnSoDienThoai"].Value.ToString();
            txtDiaChi.Text = row.Cells["columnDiaChi"].Value.ToString();
            txtEmail.Text = row.Cells["columnEmail"].Value.ToString();
            txtTenDN.Text = row.Cells["columnTenDangNhap"].Value.ToString();
            txtMatKhauDN.Text = row.Cells["columnMatKhau"].Value.ToString();
            dtmNgaySinh.Value = Convert.ToDateTime(row.Cells["columnNgaySinh"].Value.ToString());
            dtmNgayVaoLam.Value = Convert.ToDateTime(row.Cells["columnNgayVaoLam"].Value.ToString());
        }

        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
