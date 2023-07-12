using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ouyangxu
{
    public partial class oyx_main : Form
    {
        public oyx_main()
        {
            InitializeComponent();
        }
        public oyx_main(string filename)
        {
            InitializeComponent();
            fullname = filename;
        }

        Bitmap img = null;
        string fullname = null;
        int tmp_width, tmp_height;

        DashStyle line_type;
        LineCap Start_cap, End_cap;
        int colortype;
        Color line_color, start_color, end_color, front_color, back_color;
        Bitmap fill_img = null;
        LinearGradientMode linear_gradient_mode;
        HatchStyle hatch_style;
        int lineheight;
        Pen pen;
        SolidBrush solid_brush;
        LinearGradientBrush linear_gradient_brush;
        HatchBrush hatch_brush;
        TextureBrush texture_brush;
        int drawselect = 0;
        Point start_point;

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.jpg;*.png;*.bmp;*.gif;*.tiff;*.icon";
            openFileDialog.Title = "打开图片";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fullname = openFileDialog.FileName;
                openPicture();
            }
            openFileDialog.Dispose();
        }

        private void openPicture()
        {
            try
            {
                Bitmap bitmap = new Bitmap(fullname);
                img = new Bitmap(bitmap.Width, bitmap.Height);
                Graphics draw = Graphics.FromImage(img);
                draw.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.Image = img;
                draw.Dispose();
                bitmap.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("打开图片失败！" + ee.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            打开ToolStripMenuItem_Click(sender, e);
        }

        private void oyx_main_Load(object sender, EventArgs e)
        {
            if (fullname == null || fullname == "")
            {
                img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics draw = Graphics.FromImage(img);
                draw.Clear(Color.White);
                pictureBox1.Image = img;
            }
            else openPicture();
        }

        private void oyx_main_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void oyx_main_DragDrop(object sender, DragEventArgs e)
        {
            string[] ss = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (ss.Length == 0) return;
            foreach (string s in ss)
            {
                string ex = s.Substring(s.LastIndexOf('.') + 1).ToLower();
                if (ex.Equals("bmp") || ex.Equals("jpg") || ex.Equals("gif") || ex.Equals("png") || ex.Equals("tiff") || ex.Equals("icon")) { 
                    oyx_main f = new oyx_main(s);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("不支持的文件格式！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        bool domousemove = false;
        ArrayList array_point = new ArrayList();
        string draw_string = "";
        Font font = new Font("宋体", 12);

        public int ImageWidth
        {
            get { return img.Width; }
            set { tmp_width = value; }
        }

        public int ImageHeight
        {
            get { return img.Height; }
            set { tmp_height = value; }
        }
    }
}
