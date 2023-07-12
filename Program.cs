using System;
using System.Windows.Forms;

namespace ouyangxu
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string filename = null;
            if (args.Length > 0)
            {
                filename = string.Join(" ", args);
                string ex = filename.Substring(filename.LastIndexOf('.') + 1).ToLower();
                if (ex.Equals("bmp") || ex.Equals("jpg") || ex.Equals("gif") || ex.Equals("png") || ex.Equals("tiff") || ex.Equals("icon"))
                    Application.Run(new oyx_main(filename));
                else
                {
                    MessageBox.Show("不支持的文件格式！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            else Application.Run(new oyx_main());
        }
    }
}
