namespace BaiTap4_2_4.2_dangkyhocvien
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmDangKy());
        }
    }
}
