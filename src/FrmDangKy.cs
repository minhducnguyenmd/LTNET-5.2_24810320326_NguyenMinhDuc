using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // 1. Dòng dùng thư viện MySQL

namespace BaiTap4_2_4.2_dangkyhocvien
{
    public partial class FrmDangKy : Form
{
    // 2. Chuỗi kết nối MySQL (thay MAT_KHAU_CUA_BAN bằng mật khẩu root của bạn)
    private string connectionString = "Server=localhost;Database=QuanLyHocVien;Uid=root;Pwd=MAT_KHAU_CUA_BAN;";

    public FrmDangKy()
    {
        InitializeComponent();
    }

    private void FrmDangKy_Load(object sender, EventArgs e)
    {
        // Nạp danh sách Khóa học từ CSDL MySQL lên ComboBox[cite: 5]
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT MaKH, TenKH FROM KhoaHoc";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboCourse.DataSource = dt;
                cboCourse.DisplayMember = "TenKH"; // Hiển thị tên[cite: 5]
                cboCourse.ValueMember = "MaKH";     // Giá trị ẩn là Mã[cite: 5]
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }
    }

    private void btnDangKy_Click(object sender, EventArgs e)
    {
        // Lưu dữ liệu vào CSDL MySQL[cite: 6]
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO HocVien (SoDienThoai, NgaySinh, MaKH) VALUES (@phone, @dob, @makh)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@phone", mtxtPhone.Text);
                    cmd.Parameters.AddWithValue("@dob", dtpBirthDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@makh", cboCourse.SelectedValue.ToString());

                    cmd.ExecuteNonQuery();

                    // Hiển thị thông báo tổng hợp như cũ[cite: 5]
                    string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
                    string online = chkOnline.Checked ? "Có" : "Không";
                    string thongTin = "Họ tên: " + txtHoTen.Text + "\n"
                        + "Giới tính: " + gioiTinh + "\n"
                        + "Ngày sinh: " + dtpBirthDate.Text + "\n"
                        + "SĐT: " + mtxtPhone.Text + "\n"
                        + "Khóa học: " + cboCourse.Text + " (" + cboCourse.SelectedValue + ")" + "\n"
                        + "Học online: " + online + "\n\n"
                        + "=> Đã lưu vào CSDL MySQL thành công!";

                    MessageBox.Show(thongTin, "Thông tin đăng ký");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }
    }
}
}