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
    public partial class menuForm : Form
    {
        public menuForm()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet dataDM = new DataSet();
        DataSet dataTD = new DataSet();

        void loadDataDM()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from DanhMuc order by MaDanhMuc";
            adapter.SelectCommand = command;
            dataDM.Clear();
            adapter.Fill(dataDM);
            dataGridViewDM.DataSource = dataDM.Tables[0];
        }

        void loadDataTD()
        {
            command = connection.CreateCommand();
            command.CommandText = "select MaMonAn, TenMonAn, GiaTien, DonViTinh, TenDanhMuc, TD.MaDanhMuc " +
                "from ThucDon TD,DanhMuc DM where TD.MaDanhMuc = DM.MaDanhMuc";
            adapter.SelectCommand = command;
            dataTD.Clear();
            adapter.Fill(dataTD);
            dataGridViewTD.DataSource = dataTD.Tables[0];
        }

        private void menuForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString.connectionString);
            connection.Open();
            loadDataDM();
            loadDataTD();
        }

        private void btAddDM_Click(object sender, EventArgs e)
        {
            if (txtMDM.ReadOnly == true)
                return;

            if (txtMDM.Text != "" && txtTDM.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "insert into DanhMuc values ('" + txtMDM.Text.ToString()
                    + "',N'" + txtTDM.Text.ToString() + "','"+txtGC.Text.ToString()+"')";
                try
                {
                    DialogResult dlr = MessageBox.Show("Bạn muốn thêm danh mục " + txtTDM.Text.ToString()
                        + " với mã: " + txtMDM.Text.ToString(), "Thêm danh mục", MessageBoxButtons.YesNo);
                    if (dlr == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        txtMDM.ReadOnly = true;
                        btAddDMz.Text = "+";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã danh mục đã tồn tại");
                }
                loadDataDM();
            }
            else
            {
                MessageBox.Show("Nhập tên và mã danh mục cần thêm");
            }

        }

        private void dataGridViewDM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGridViewDM.Rows[e.RowIndex];
            txtMDM.Text = row.Cells[0].Value.ToString();
            txtTDM.Text = row.Cells[1].Value.ToString();
            txtGC.Text = row.Cells[2].Value.ToString();
        }

        private void btDeleteDM_Click(object sender, EventArgs e)
        {
            if (txtMDM.ReadOnly == false)
                return;

            command = connection.CreateCommand();
            command.CommandText = "delete from DanhMuc where MaDanhMuc ='" + txtMDM.Text.ToString() + "'";
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn xóa danh mục có mã: " + txtMDM.Text.ToString(),
                    "Xóa danh mục", MessageBoxButtons.YesNo);
                if (dlr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    loadDataDM();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Danh mục có món ăn không thể xóa");
            }

        }

        private void btAddDMz_Click(object sender, EventArgs e)
        {
            if (txtMDM.ReadOnly == true)
            { 
                txtMDM.ReadOnly = false;
                btAddDMz.Text = "X";
            }
            else
            {
                txtMDM.ReadOnly = true;
                btAddDMz.Text = "+";
                txtMDM.Text = "";
            }
        }

        private void btUpdateDM_Click(object sender, EventArgs e)
        {
            if (txtMDM.ReadOnly == false)
                return;

            if (txtMDM.Text.ToString() != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "update DanhMuc set TenDanhMuc = N'" + txtTDM.Text.ToString()
                    + "', GhiChu= N'" + txtGC.Text.ToString()
                    + "' where MaDanhMuc = '" + txtMDM.Text.ToString() + "'";
                command.ExecuteNonQuery();
                loadDataDM();
            }
            else
            {
                MessageBox.Show("Nhập mã danh mục cần chỉnh sửa");
            }
        }

        private void btAddMA_Click(object sender, EventArgs e)
        {
            if (txtMMA.Text == "" || txtTMA.Text == "" || txtDVT.Text == "" || txtGT.Text == "")
            {
                MessageBox.Show("Điền đầy đủ thông tin món ăn");
                return;
            }

            if (txtMDM.Text == "")
            {
                MessageBox.Show("Chọn danh mục của món ăn cần thêm");
                return;
            }

            command = connection.CreateCommand();
            command.CommandText = "insert into ThucDon values ('" + txtMMA.Text.ToString() + "',N'"
                + txtTMA.Text.ToString() + "'," + txtGT.Text.ToString() + ",N'"
                + txtDVT.Text.ToString() + "','" + txtMDM.Text.ToString() + "',1)";
            try
            {
                DialogResult dlr = MessageBox.Show("        Bạn muốn thêm \nTên: " + txtTMA.Text.ToString() 
                    + "(Mã: " + txtMMA.Text.ToString()
                    + ")\nGiá: " + txtGT.Text.ToString() + "/" + txtDVT.Text.ToString()
                    + "\nDanh mục: " + txtTDM.Text.ToString(), "Thêm món ăn", MessageBoxButtons.YesNo);
                if (dlr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    txtMMA.ReadOnly = true;
                    btAddMAz.Text = "+";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mã Món Ăn đã tồn tại");
            }
            loadDataTD();

        }

        private void dataGridViewTD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGridViewTD.Rows[e.RowIndex];

            txtMMA.Text = row.Cells[0].Value.ToString();
            txtTMA.Text = row.Cells[1].Value.ToString();
            txtGT.Text = row.Cells[2].Value.ToString();
            txtDVT.Text = row.Cells[3].Value.ToString();
            txtTDM.Text = row.Cells[4].Value.ToString();
            txtMDM.Text = row.Cells[5].Value.ToString();
        }

        private void btDeleteMA_Click(object sender, EventArgs e)
        {
            if (txtMMA.ReadOnly == false)
                return;

            command = connection.CreateCommand();
            command.CommandText = "delete from ThucDon where MaMonAn = '" + txtMMA.Text.ToString() + "'";
            DialogResult dlr = MessageBox.Show("Bạn muốn xóa món có mã: " + txtMMA.Text.ToString(),
                "Xóa món ăn", MessageBoxButtons.YesNo);
            if (dlr == DialogResult.Yes)
            {
                command.ExecuteNonQuery();
            }
            loadDataTD();
        }

        private void btUpdateMA_Click(object sender, EventArgs e)
        {
            if (txtMMA.ReadOnly == false)
                return;

            if (txtMMA.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "update ThucDon set TenMonAn = N'" + txtTMA.Text.ToString()
                    + "', GiaTien = " + txtGT.Text.ToString()
                    + ", DonViTinh = N'" + txtDVT.Text.ToString()
                    + "', MaDanhMuc ='" + txtMDM.Text.ToString()
                    + "' where MaMonAn = '" + txtMMA.Text.ToString() + "'";
                command.ExecuteNonQuery();
                loadDataTD();
            }
            else MessageBox.Show("Nhập mã món ăn cần chỉnh sửa");
        }

        private void btAddMAz_Click(object sender, EventArgs e)
        {
            if (txtMMA.ReadOnly == true)
            {
                txtMMA.ReadOnly = false;
                btAddMAz.Text = "X";
            }
            else
            {
                txtMMA.ReadOnly = true;
                btAddMAz.Text = "+";
                txtMMA.Text = "";
            }

        }

        private void txtGT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
