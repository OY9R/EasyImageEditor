using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
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
        LineCap start_cap, end_cap;
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
                open();
            }
            openFileDialog.Dispose();
        }

        private void open()
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
                this.Text = fullname.Substring(fullname.LastIndexOf(@"\") + 1) + " " + img.Width.ToString() + "*" + img.Width.ToString();
                tmp_width = img.Width;
                tmp_height = img.Height;
            }
            catch (Exception ee)
            {
                MessageBox.Show("打开图片失败！" + ee.Message);
            }
        }

        private void newf()
        {
            oyx_main f = new oyx_main();
            f.Show();
            exit(false);
        }

        private void exit(bool quit)
        {
            if(MessageBox.Show("是否保存更改?", "保存更改?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                save(false);
            }
            else
            {
                if (quit) System.Environment.Exit(0);
                else this.Hide();
            }
        }

        private void save(bool other)
        {
            if(img != null)
            {
                if(fullname == null || other) {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png|TIF File|*.tiff|Icon File|*.icon";
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.Title = "保存/另存为图像";
                    saveFileDialog.ValidateNames = true;
                    saveFileDialog.RestoreDirectory = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            switch(saveFileDialog.FilterIndex)
                            {
                                case 1: img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                                case 2: img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp); break;
                                case 3: img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Gif); break;
                                case 4: img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                                case 5: img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Tiff); break;
                                case 6: img.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Icon); break;
                                default: MessageBox.Show("不支持的文件格式！\n保存图片失败！"); break;
                            }
                            fullname = saveFileDialog.FileName;
                            this.Text = fullname.Substring(fullname.LastIndexOf(@"\") + 1);
                        }
                        catch(Exception ee)
                        {
                            MessageBox.Show("保存图片失败!\n" + ee.Message);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    img.Save(fullname);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("保存图片失败!\n" + ee.Message);
                }
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
            else open();
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

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(false);
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            save(false);
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newf();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newf();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit(true);
        }

        private void oyx_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit(true);
        }

        private void 实际大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( img == null) return;
            pictureBox1.Height = (int)Math.Ceiling(pictureBox1.Height * 1.1);
            pictureBox1.Width = (int)Math.Ceiling(pictureBox1.Width * 1.1);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            pictureBox1.Height = (int)Math.Ceiling(pictureBox1.Height * 0.9);
            pictureBox1.Width = (int)Math.Ceiling(pictureBox1.Width * 0.9);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void 适合窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            pictureBox1.Height = (int)Math.Ceiling((decimal)flowLayoutPanel1.Height);
            pictureBox1.Width = (int)Math.Ceiling((decimal)flowLayoutPanel1.Width);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            适合窗口ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            实际大小ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            放大ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            缩小ToolStripMenuItem_Click(sender, e);
        }

        private void 图像大小设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(img == null) return;
            tmp_width = img.Width;
            tmp_height = img.Height;
            oyx_setsize newform = new oyx_setsize();
            newform.Owner = this;
            newform.ShowDialog();
            if (tmp_height != img.Width || tmp_height != img.Height)
            {
                Bitmap bitmap = new Bitmap(tmp_width, tmp_height);
                Graphics draw = Graphics.FromImage(bitmap);
                draw.DrawImage(img, 0, 0, bitmap.Width, bitmap.Height);
                img = bitmap;
                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            this.Text = fullname.Substring(fullname.LastIndexOf(@"\") + 1) + " " + img.Width.ToString() + "*" + img.Width.ToString();
        }

        private void 水平翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(img != null)
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = img;
            }
        }

        private void 垂直翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img != null)
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox1.Image = img;
            }
        }

        private void 顺时针旋转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img != null)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = img;
            }
        }

        private void 逆时针旋转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img != null)
            {
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Image = img;
            }
        }

        private void 反色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            Color c_temp = new Color();
            for(int i=0;i<img.Width;i++)
            {
                for(int j = 0; j < img.Height; j++)
                {
                    c_temp = img.GetPixel(i, j);
                    Color c_new = Color.FromArgb(255-c_temp.R, 255-c_temp.G, 255-c_temp.B);
                    img.SetPixel(i,j,c_new);
                }
            }
            pictureBox1.Image = img;
        }

        private void 浮雕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            Bitmap myBitmap = new Bitmap(pictureBox1.Image);//创建Bitmap对象：提取像素信息，转化为二维数组
            for (int i = 0; i < myBitmap.Width - 1; i++)
            {
                for (int j = 0; j < myBitmap.Height - 1; j++)
                {
                    Color Color1 = myBitmap.GetPixel(i, j);//调用GetPixel方法获得像素点颜色
                    Color Color2 = myBitmap.GetPixel(i + 1, j + 1);
                    int red = Math.Abs(Color1.R - Color2.R + 128); //调用绝对值Abs函数
                    //颜色处理
                    int green = Math.Abs(Color1.G - Color2.G + 128);
                    int blue = Math.Abs(Color1.B - Color2.B + 128);
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;
                    //用SetPixel()方法设置像素点颜色
                    myBitmap.SetPixel(i, j, Color.FromArgb(red, green, blue));
                }
            }
            pictureBox1.Image = myBitmap;
        }

        private void 黑白ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            Color c_temp = new Color();
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    c_temp = img.GetPixel(i, j);
                    int gray = (c_temp.R + c_temp.G + c_temp.B) / 3;
                    int black_white = gray > 128 ? 255 : 0;
                    Color c_new = Color.FromArgb(black_white,black_white,black_white);
                    img.SetPixel(i, j, c_new);
                }
            }
            pictureBox1.Image = img;
        }

        private void 灰度化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            Color c_temp = new Color();
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    c_temp = img.GetPixel(i, j);
                    int gray = (c_temp.R + c_temp.G + c_temp.B) / 3;
                    Color c_new = Color.FromArgb(gray, gray, gray);
                    img.SetPixel(i, j, c_new);
                }
            }
            pictureBox1.Image = img;
        }

        private void Laplacian(double[] Laplacian)
        {
            Bitmap myBitmap = new Bitmap(pictureBox1.Image);
            //建立拉普拉斯模板
            Color pixel;
            //这里注意边界的像素暂不处理，否则超出数组范围
            for (int i = 1; i < myBitmap.Width - 1; i++)
            {
                for (int j = 1; j < myBitmap.Height - 1; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    int index = 0;
                    for (int col = -1; col <= 1; col++) //3*3处理
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = myBitmap.GetPixel(i + row, j + col);
                            red += (int)(pixel.R * Laplacian[index]);
                            green += (int)(pixel.G * Laplacian[index]);
                            blue += (int)(pixel.B * Laplacian[index]);
                            index++;
                        }
                    }
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;
                    myBitmap.SetPixel(i - 1, j - 1, Color.FromArgb((int)red, (int)green, (int)blue)); //这里注意是i-1,j-1，否则效果很糟糕
                }
            }
            pictureBox1.Image = myBitmap;
        }

        private void 柔化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] Lap = { 0.1, 0.1, 0.1, 0.1, 0.2, 0.1, 0.1, 0.1, 0.1 };
            Laplacian(Lap);
        }

        private void 锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] Lap = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            Laplacian(Lap);
        }

        private void 雾化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myBitmap = new Bitmap(pictureBox1.Image);
            Random random = new Random();
            Color pixel;
            //这里注意边界的像素暂不处理，否则超出数组范围
            for (int i = 3; i < myBitmap.Width - 3; i++)
            {
                for (int j = 3; j < myBitmap.Height - 3; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    pixel = myBitmap.GetPixel(i + random.Next()%7 - 3, j + random.Next() % 7 - 3);
                    red = pixel.R; green = pixel.G; blue = pixel.B;
                    myBitmap.SetPixel(i - 1, j - 1, Color.FromArgb((int)red, (int)green, (int)blue)); //这里注意是i-1,j-1，否则效果很糟糕
                }
            }
            pictureBox1.Image = myBitmap;
        }

        private void 马赛克效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myBitmap = new Bitmap(pictureBox1.Image);
            Random random = new Random();
            Color pixel;
            //这里注意边界的像素暂不处理，否则超出数组范围
            for (int i = 3; i < myBitmap.Width-3; i+=7)
            {
                for (int j = 3; j < myBitmap.Height-3; j+=7)
                {
                    int red = 0, green = 0, blue = 0;
                    pixel = myBitmap.GetPixel(i , j);
                    red = pixel.R; green = pixel.G; blue = pixel.B;
                    for (int k = -3; k <= 3; k++)
                    {
                        for (int l = -3; l <= 3; l++)
                        {
                            myBitmap.SetPixel(i + k, j + l, Color.FromArgb((int)red, (int)green, (int)blue)); //这里注意是i-1,j-1，否则效果很糟糕
                        }
                    }
                }
            }
            pictureBox1.Image = myBitmap;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            反色ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            锐化ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            马赛克效果ToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            灰度化ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            line_type = DashStyle.Solid;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            line_type = DashStyle.DashDot;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            line_type = DashStyle.DashDotDot;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            line_type = DashStyle.Dash;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            line_type = DashStyle.Dot;
        }

        private void 无ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start_cap = LineCap.NoAnchor;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            start_cap = LineCap.ArrowAnchor;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            start_cap = LineCap.DiamondAnchor;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            start_cap = LineCap.SquareAnchor;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            start_cap = LineCap.Triangle;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            start_cap = LineCap.RoundAnchor;
        }

        private void 无ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            end_cap = LineCap.NoAnchor;
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            end_cap = LineCap.ArrowAnchor;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            end_cap = LineCap.DiamondAnchor;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            end_cap = LineCap.SquareAnchor;
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            end_cap = LineCap.Triangle;
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            end_cap = LineCap.RoundAnchor;
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
