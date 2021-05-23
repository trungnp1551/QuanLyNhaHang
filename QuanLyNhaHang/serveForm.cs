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
    public partial class serveForm : Form
    {
        public serveForm()
        {
            InitializeComponent();
        }
        public string manhanvien;

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet dataMenu = new DataSet();
        DataSet dataInfo = new DataSet();

        void loadDataMenu()
        {
            command = connection.CreateCommand();
            command.CommandText = "select MaMonAn, TenMonAn, GiaTien, DonViTinh, TenDanhMuc, TD.MaDanhMuc, SoLuong " +
                "from ThucDon TD,DanhMuc DM where TD.MaDanhMuc = DM.MaDanhMuc";
            adapter.SelectCommand = command;
            dataMenu.Clear();
            adapter.Fill(dataMenu);
            dataGridViewMenu.DataSource = dataMenu.Tables[0];
        }

        void loadDataInfo(string maban)
        {
            command = connection.CreateCommand();
            command.CommandText = "select td.MaMonAn, tttt.TenMonAn, tttt.SoLuong, tttt.GiaTien, td.DonViTinh " +
                "from ThongTinTamThoi tttt, ThucDon td " +
                "where td.TenMonAn = tttt.TenMonAn and tttt.MaBan = '" + maban + "'";
            adapter.SelectCommand = command;
            dataInfo.Clear();
            adapter.Fill(dataInfo);
            dataGridViewInfo.DataSource = dataInfo.Tables[0];
        }

        private void serveForm_Load(object sender, EventArgs e)
        {
            lblMaNV.Text = "Mã nhân viên: " + manhanvien;
            connection = new SqlConnection(ConnectionString.connectionString);
            connection.Open();
            loadDataMenu();
            setColorTable(1);
        }

        private void dataGridViewMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGridViewMenu.Rows[e.RowIndex];
            //MessageBox.Show(row.Cells["Soluong"].Value.ToString());

            if (e.ColumnIndex == 6)
            {
                command = connection.CreateCommand();
                int soluong = (row.Cells["menuColumnSoluong"].Value.ToString() == "True") ? 0 : 1;
                string ma = row.Cells["menuColumnMaMonAn"].Value.ToString();
                //MessageBox.Show(soluong + " " + ma);

                command.CommandText = "update ThucDon set SoLuong = " + soluong + " where MaMonAn = '" + ma + "'";
                command.ExecuteNonQuery();
                loadDataMenu();
            }

        }

        void setColorTable(int floor)
        {
            //string maban;
            command = connection.CreateCommand();
            for (int i = 1; i <= 5; i++)
            {
                //maban = floor + i;
                command.CommandText = "select count (distinct tttt.MaMonAn) from ThongTinTamThoi tttt where tttt.MaBan = '" + floor + i + "'";
                if ((int)command.ExecuteScalar() != 0)
                {
                    switch (i)
                    {
                        case 1:
                            btTable1.BackColor = Color.Red;
                            break;
                        case 2:
                            btTable2.BackColor = Color.Red;
                            break;
                        case 3:
                            btTable3.BackColor = Color.Red;
                            break;
                        case 4:
                            btTable4.BackColor = Color.Red;
                            break;
                        case 5:
                            btTable5.BackColor = Color.Red;
                            break;
                    };
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            btTable1.BackColor = Color.FromArgb(192, 255, 192);
                            break;
                        case 2:
                            btTable2.BackColor = Color.FromArgb(192, 255, 192);
                            break;
                        case 3:
                            btTable3.BackColor = Color.FromArgb(192, 255, 192);
                            break;
                        case 4:
                            btTable4.BackColor = Color.FromArgb(192, 255, 192);
                            break;
                        case 5:
                            btTable5.BackColor = Color.FromArgb(192, 255, 192);
                            break;
                    };

                }
            }

        }

        private void btFloor1_Click(object sender, EventArgs e)
        {
            txtFloor.Text = "1";
            txtTable.Text = "";
            setColorTable(1);
        }

        private void btFloor2_Click(object sender, EventArgs e)
        {
            txtFloor.Text = "2";
            txtTable.Text = "";
            setColorTable(2);
        }

        private void btFloorVIP_Click(object sender, EventArgs e)
        {
            txtFloor.Text = "3";
            txtTable.Text = "";
            setColorTable(3);
        }

        private void btTable1_Click(object sender, EventArgs e)
        {
            txtTable.Text = "1";
            string maban = txtFloor.Text + txtTable.Text;
            loadDataInfo(maban);
        }

        private void btTable2_Click(object sender, EventArgs e)
        {
            txtTable.Text = "2";
            string maban = txtFloor.Text + txtTable.Text;
            loadDataInfo(maban);
        }

        private void btTable3_Click(object sender, EventArgs e)
        {
            txtTable.Text = "3";
            string maban = txtFloor.Text + txtTable.Text;
            loadDataInfo(maban);
        }

        private void btTable4_Click(object sender, EventArgs e)
        {
            txtTable.Text = "4";
            string maban = txtFloor.Text + txtTable.Text;
            loadDataInfo(maban);
        }

        private void btTable5_Click(object sender, EventArgs e)
        {
            txtTable.Text = "5";
            string maban = txtFloor.Text + txtTable.Text;
            loadDataInfo(maban);
        }

        private void dataGridViewMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (txtFloor.Text == "" || txtTable.Text == "")
            {
                MessageBox.Show("Chọn khu vực và bàn bạn muốn");
                return;
            }

            DataGridViewRow row = dataGridViewMenu.Rows[e.RowIndex];
            string soluong = row.Cells["menuColumnSoLuong"].Value.ToString();
            if(soluong == "False")
            {
                MessageBox.Show("Món đã hết hàng");
                return;
            }
            string maban = txtFloor.Text + txtTable.Text;
            string tenmonan = row.Cells["menuColumnTenMonAn"].Value.ToString();
            string mamonan = row.Cells["menuColumnMaMonAn"].Value.ToString();
            string giatien = row.Cells["menuColumnGiaTien"].Value.ToString();

            command = connection.CreateCommand();
            command.CommandText = "DECLARE @soluong int set @soluong = (" +
                "select tttt.SoLuong from ThongTinTamThoi tttt where tttt.MaMonAn = '" + mamonan + "' and tttt.MaBan = '" + maban + "') " +
                "if @soluong > 0 update ThongTinTamThoi set SoLuong = @soluong + 1 where MaMonAn = '" + mamonan + "' and MaBan = '" + maban + "'" +
                "else insert into ThongTinTamThoi values('" + maban + "','" + mamonan + "',N'" + tenmonan + "',1," + giatien + ",0)";
            command.ExecuteNonQuery();
            loadDataInfo(maban);

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtFloor.Text == "" || txtTable.Text == "")
            {
                MessageBox.Show("Chọn khu vực và bàn bạn muốn");
                return;
            }
            command = connection.CreateCommand();
            //bật màu bàng hiện tại
            setColorTable(int.Parse(txtFloor.Text));

        }

        private void btReset_Click(object sender, EventArgs e)
        {
            if (txtFloor.Text == "" || txtTable.Text == "")
            {
                MessageBox.Show("Chọn khu vực và bàn bạn muốn");
                return;
            }
            string maban = txtFloor.Text + txtTable.Text;

            command = connection.CreateCommand();

            command.CommandText = "select count(*) from ThongTinTamThoi where MaBan = '" + maban + "'";
            if ((int)command.ExecuteScalar() == 0) return;

            command.CommandText = "select TrangThaiThanhToan from ThongTinTamThoi where MaBan ='" + maban + "'";
            if (command.ExecuteScalar().ToString() == "False")
            {
                if (MessageBox.Show("Bàn chưa được thanh toán, bạn có muốn reset?", "Thông báo", MessageBoxButtons.YesNo) != DialogResult.Yes) 
                {
                    return;
                }
            }
            else
            {
                string tenmonan;
                int soluong;
                int giatien;
                string donvitinh;

                //////add hoa don, chi tiet hoa don
                command.CommandText = "select COUNT(*) from HoaDon";
                string hd = "hd" + ((int)command.ExecuteScalar() + 1);
                //maban = txtFloor.Text + txtTable.Text;
                int tongtien = 0;

                int soluongrow = (int)dataGridViewInfo.RowCount;
                for (int i = 0; i < soluongrow; i++)
                {
                    tenmonan = dataGridViewInfo.Rows[i].Cells["infoColumnTenMonAn"].Value.ToString();
                    soluong = (int)dataGridViewInfo.Rows[i].Cells["infoColumnSoLuong"].Value;
                    giatien = (int)dataGridViewInfo.Rows[i].Cells["infoColumnGiaTien"].Value;
                    donvitinh = dataGridViewInfo.Rows[i].Cells["infoColumnDonViTinh"].Value.ToString();
                    command.CommandText = "insert into ChiTietHoaDon values ('" + hd + "',N'" +
                        tenmonan + "'," + soluong + ",'" + giatien + "',N'" + donvitinh + "')";
                    command.ExecuteNonQuery();
                    tongtien += giatien * soluong;
                }

                command.CommandText = "insert into HoaDon values ('" + hd + "','" +
                    maban + "','" + manhanvien + "'," + tongtien + ",getdate())";
                command.ExecuteNonQuery();

            }

            command = connection.CreateCommand();
            command.CommandText = "delete from	ThongTinTamThoi where MaBan = '" + maban + "'";
            command.ExecuteNonQuery();
            loadDataInfo(maban);
            setColorTable(int.Parse(txtFloor.Text));
        }

        private void btPay_Click(object sender, EventArgs e)
        {
            if (txtFloor.Text == "" || txtTable.Text == "")
            {
                MessageBox.Show("Chọn khu vực và bàn bạn muốn");
                return;
            }
            string maban = txtFloor.Text + txtTable.Text;
            command = connection.CreateCommand();
            command.CommandText = "select count (distinct tttt.MaMonAn) from ThongTinTamThoi tttt where tttt.MaBan = '" + maban + "'";
            if ((int)command.ExecuteScalar() == 0) return;

            if (rdoATM.Checked == false && rdoCash.Checked == false && rdoMomo.Checked == false)
            {
                MessageBox.Show("Chọn hình thức bạn muốn thanh toán");
                return;
            }

            payForm form = new payForm();
            form.maban = txtFloor.Text + txtTable.Text;
            int soluongrow = (int)dataGridViewInfo.RowCount;
            for (int i = 0; i < soluongrow; i++)
            {
                form.strMonAn[i] = dataGridViewInfo.Rows[i].Cells["infoColumnTenMonAn"].Value.ToString();
                form.strGiaTien[i] = dataGridViewInfo.Rows[i].Cells["infoColumnGiaTien"].Value.ToString();
                form.strSoLuong[i] = dataGridViewInfo.Rows[i].Cells["infoColumnSoLuong"].Value.ToString();
            }            
            form.Show();
        }

        private void dataGridViewInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (txtFloor.Text == "" || txtTable.Text == "")
            {
                MessageBox.Show("Chọn khu vực và bàn bạn muốn");
                return;
            }
            DataGridViewRow row = dataGridViewInfo.Rows[e.RowIndex];

            string maban = txtFloor.Text + txtTable.Text;
            string mamonan = row.Cells["infoColumnMaMonAn"].Value.ToString();

            command = connection.CreateCommand();
            command.CommandText = "DECLARE @soluong int set @soluong = (" +
                "select tttt.SoLuong from ThongTinTamThoi tttt where tttt.MaMonAn = '" + mamonan + "' and tttt.MaBan = '" + maban + "')" +
                "if @soluong = 1 delete from ThongTinTamThoi where MaBan = '" + maban + "' and MaMonAn = '" + mamonan + "'" +
                "else update ThongTinTamThoi set SoLuong = @soluong - 1 where MaMonAn = '" + mamonan + "' and MaBan = '" + maban + "'";
            command.ExecuteNonQuery();
            loadDataInfo(maban);
        }
    }
}
