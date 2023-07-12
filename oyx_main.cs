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
                try
                {
                    fullname = openFileDialog.FileName;
                    Bitmap bitmap = new Bitmap(fullname);
                    img = new Bitmap(bitmap.Width, bitmap.Height);
                    Graphics draw = Graphics.FromImage(img);
                    draw.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.Image = img;
                    openFileDialog.Dispose();
                    draw.Dispose();
                    bitmap.Dispose();
                }
                catch(Exception ee)
                {
                    MessageBox.Show("打开图片失败！" + ee.Message);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            打开ToolStripMenuItem_Click(sender, e);
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
