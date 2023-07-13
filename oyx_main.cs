using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            if (MessageBox.Show("是否保存更改?", "保存更改?", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
            if (img != null)
            {
                if (fullname == null || other)
                {
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
                            switch (saveFileDialog.FilterIndex)
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
                        catch (Exception ee)
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
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
                if (ex.Equals("bmp") || ex.Equals("jpg") || ex.Equals("gif") || ex.Equals("png") || ex.Equals("tiff") || ex.Equals("icon"))
                {
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
            if (img == null) return;
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
            if (img == null) return;
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
            if (img != null)
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
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    c_temp = img.GetPixel(i, j);
                    Color c_new = Color.FromArgb(255 - c_temp.R, 255 - c_temp.G, 255 - c_temp.B);
                    img.SetPixel(i, j, c_new);
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
                    Color c_new = Color.FromArgb(black_white, black_white, black_white);
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
                    pixel = myBitmap.GetPixel(i + random.Next() % 7 - 3, j + random.Next() % 7 - 3);
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
            for (int i = 3; i < myBitmap.Width - 3; i += 7)
            {
                for (int j = 3; j < myBitmap.Height - 3; j += 7)
                {
                    int red = 0, green = 0, blue = 0;
                    pixel = myBitmap.GetPixel(i, j);
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

        private void 线条颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.Color = colorshow.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorshow.BackColor = colorDialog.Color;
                line_color = colorDialog.Color;
                front_color = colorDialog.Color;
            }
        }

        private void colorshow_Click(object sender, EventArgs e)
        {
            线条颜色ToolStripMenuItem_Click(sender, e);
        }

        private void 填充色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.Color = colorfill.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorfill.Text = "";
                colorfill.BackColor = colorDialog.Color;
                back_color = colorDialog.Color;
                colortype = 1;
            }
        }

        private void colorfill_Click(object sender, EventArgs e)
        {
            填充色ToolStripMenuItem_Click(sender, e);
        }

        bool domousemove = false;
        ArrayList array_point = new ArrayList();
        string draw_string = "";
        Font font = new Font("宋体", 12);

        private void 纹理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oyx_sethatch hatchform = new oyx_sethatch();
            hatchform.Owner = this;
            hatchform.ShowDialog();
            if (colortype == 3)
            {
                colorfill.Text = "纹理";
                colorfill.BackColor = Color.White;
            }
        }

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

        public Color startcolor
        {
            get { return start_color; }
            set { start_color = value; }
        }

        private void 渐变色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oyx_setgradient setgradient = new oyx_setgradient();
            setgradient.Owner = this;
            setgradient.ShowDialog();
            if (colortype == 2)
            {
                colorfill.Text = "渐变";
                colorfill.BackColor = Color.White;
            }
        }

        private void 图片填充ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件(*.jpg,*.bmp,*.png)|*.jpg;*.bmp;*.png";
            openFileDialog.Title = "打开图片文件";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fill_img = (Bitmap)Image.FromFile(openFileDialog.FileName);
                colorfill.Text = "图片";
                colorfill.BackColor = Color.White;
                colortype = 4;
            }
        }

        private void uncheckallbutten()
        {
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton12.Checked = false;
            toolStripButton13.Checked = false;
            toolStripButton14.Checked = false;
            toolStripButton15.Checked = false;
            toolStripButton18.Checked = false;
            toolStripButton19.Checked = false;
            toolStripButton20.Checked = false;
            toolStripButton21.Checked = false;
            toolStripButton22.Checked = false;
            toolStripButton23.Checked = false;
            toolStripButton24.Checked = false;
            toolStripButton25.Checked = false;
            toolStripButton26.Checked = false;
            toolStripButton27.Checked = false;
            toolStripButton28.Checked = false;
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            drawselect = 1;
            uncheckallbutten();
            toolStripButton19.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            drawselect = 0;
            uncheckallbutten();
            Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawselect == 0) return;
            start_point = new Point(e.X, e.Y);
            array_point.Add(start_point);
            domousemove = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (domousemove)
            {
                pictureBox1.Image = (Bitmap)img.Clone();
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                switch (drawselect)
                {
                    case 1:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        break;
                    case 2:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        Point[] drawPoint = new Point[array_point.Count + 1];
                        int i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        drawPoint[i] = new Point(e.X, e.Y);
                        //array_point.Add(drawPoint[i]);
                        g.DrawLines(pen, drawPoint);
                        break;
                    case 3:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        drawPoint = new Point[array_point.Count + 1];
                        i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        drawPoint[i] = new Point(e.X, e.Y);
                        //array_point.Add(drawPoint[i]);
                        g.DrawCurve(pen, drawPoint);
                        break;
                    case 4:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        drawPoint = new Point[array_point.Count + 1];
                        i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        drawPoint[i] = new Point(e.X, e.Y);
                        array_point.Add(drawPoint[i]);
                        g.DrawLines(pen, drawPoint);
                        break;
                    case 7:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            if (colortype == 1)
                            {
                                solid_brush = new SolidBrush(backcolor);
                                g.FillEllipse(solid_brush, rec);
                            }
                            else if (colortype == 2)
                            {
                                linear_gradient_brush = new LinearGradientBrush(rec, frontcolor, backcolor, linear_gradient_mode);
                                g.FillEllipse(linear_gradient_brush, rec);
                            }
                            else if (colortype == 3)
                            {
                                hatch_brush = new HatchBrush(hatch_style, frontcolor, backcolor);
                                g.FillEllipse(hatch_brush, rec);
                            }
                            else if (colortype == 4)
                            {
                                texture_brush = new TextureBrush(fill_img);
                                g.FillEllipse(texture_brush, rec);
                            }
                            g.DrawEllipse(pen, rec);
                        }
                        break;
                    case 8:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            g.DrawEllipse(pen, rec);
                        }
                        break;
                    case 9:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            if (colortype == 1)
                            {
                                solid_brush = new SolidBrush(backcolor);
                                g.FillRectangle(solid_brush, rec);
                            }
                            else if (colortype == 2)
                            {
                                linear_gradient_brush = new LinearGradientBrush(rec, frontcolor, backcolor, linear_gradient_mode);
                                g.FillRectangle(linear_gradient_brush, rec);
                            }
                            else if (colortype == 3)
                            {
                                hatch_brush = new HatchBrush(hatch_style, frontcolor, backcolor);
                                g.FillRectangle(hatch_brush, rec);
                            }
                            else if (colortype == 4)
                            {
                                texture_brush = new TextureBrush(fill_img);
                                g.FillRectangle(texture_brush, rec);
                            }
                            g.DrawRectangle(pen, rec);
                        }
                        break;
                    case 10:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            g.DrawRectangle(pen, rec);
                        }
                        break;
                    case 11:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        drawPoint = new Point[array_point.Count + 1];
                        i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        drawPoint[i] = new Point(e.X, e.Y);
                        g.DrawLines(pen, drawPoint);
                        break;
                    case 12:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        drawPoint = new Point[array_point.Count + 1];
                        i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        drawPoint[i] = new Point(e.X, e.Y);
                        g.DrawLines(pen, drawPoint);
                        break;
                        // TODO
                }
                g.Dispose();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (domousemove)
            {
                pictureBox1.Image = img;
                Graphics g = Graphics.FromImage(img);
                switch (drawselect)
                {
                    case 1:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        domousemove = false;
                        array_point.Clear();
                        break;
                    case 4:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        Point[] drawPoint = new Point[array_point.Count + 1];
                        int i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        drawPoint[i] = new Point(e.X, e.Y);
                        g.DrawLines(pen, drawPoint);
                        domousemove = false;
                        array_point.Clear();
                        break;
                    case 7:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            if (colortype == 1)
                            {
                                solid_brush = new SolidBrush(backcolor);
                                g.FillEllipse(solid_brush, rec);
                            }
                            else if (colortype == 2)
                            {
                                linear_gradient_brush = new LinearGradientBrush(rec, frontcolor, backcolor, linear_gradient_mode);
                                g.FillEllipse(linear_gradient_brush, rec);
                            }
                            else if (colortype == 3)
                            {
                                hatch_brush = new HatchBrush(hatch_style, frontcolor, backcolor);
                                g.FillEllipse(hatch_brush, rec);
                            }
                            else if (colortype == 4)
                            {
                                texture_brush = new TextureBrush(fill_img);
                                g.FillEllipse(texture_brush, rec);
                            }
                            g.DrawEllipse(pen, rec);
                        }
                        domousemove = false;
                        array_point.Clear();
                        break;
                    case 8:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            g.DrawEllipse(pen, rec);
                        }
                        domousemove = false;
                        array_point.Clear();
                        break;
                    case 9:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            if (colortype == 1)
                            {
                                solid_brush = new SolidBrush(backcolor);
                                g.FillRectangle(solid_brush, rec);
                            }
                            else if (colortype == 2)
                            {
                                linear_gradient_brush = new LinearGradientBrush(rec, frontcolor, backcolor, linear_gradient_mode);
                                g.FillRectangle(linear_gradient_brush, rec);
                            }
                            else if (colortype == 3)
                            {
                                hatch_brush = new HatchBrush(hatch_style, frontcolor, backcolor);
                                g.FillRectangle(hatch_brush, rec);
                            }
                            else if (colortype == 4)
                            {
                                texture_brush = new TextureBrush(fill_img);
                                g.FillRectangle(texture_brush, rec);
                            }
                            g.DrawRectangle(pen, rec);
                        }
                        domousemove = false;
                        array_point.Clear();
                        break;
                    case 10:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        if (e.X == start_point.X || e.Y == start_point.Y)
                        {
                            g.DrawLine(pen, start_point, new Point(e.X, e.Y));
                        }
                        else
                        {
                            int x, y;
                            x = e.X > start_point.X ? start_point.X : e.X;
                            y = e.Y > start_point.Y ? start_point.Y : e.Y;
                            Rectangle rec = new Rectangle(x, y, Math.Abs(e.X - start_point.X), Math.Abs(e.Y - start_point.Y));
                            g.DrawRectangle(pen, rec);
                        }
                        domousemove = false;
                        array_point.Clear();
                        break;
                        // TODO
                }
                g.Dispose();
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            drawselect = 2;
            uncheckallbutten();
            toolStripButton20.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (domousemove)
            {
                pictureBox1.Image = img;
                Graphics g = Graphics.FromImage(img);
                switch (drawselect)
                {
                    case 2:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        Point[] drawPoint = new Point[array_point.Count];
                        int i = -1;
                        foreach (Point p in array_point) drawPoint[++i] = p;
                        g.DrawLines(pen, drawPoint);
                        break;
                    case 3:
                        pen = new Pen(line_color, float.Parse(toolStripSplitButton1.Text));
                        pen.DashStyle = line_type;
                        pen.StartCap = start_cap;
                        pen.EndCap = end_cap;
                        drawPoint = new Point[array_point.Count];
                        i = -1;
                        foreach (Point p in array_point) drawPoint[++i] = p;
                        g.DrawCurve(pen, drawPoint);
                        break;
                    case 11:
                        array_point.RemoveAt(array_point.Count - 1);
                        drawPoint = new Point[array_point.Count];
                        i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        g.DrawPolygon(pen, drawPoint);
                        if (colortype == 1)
                        {
                            solid_brush = new SolidBrush(back_color);
                            g.FillPolygon(solid_brush, drawPoint);
                        }
                        else if (colortype == 2)
                        {
                            int minx=0,miny=0,maxx=0,maxy=0;
                            foreach(Point p in array_point)
                            {
                                minx = Math.Min(minx, p.X);
                                miny = Math.Min(miny, p.Y);
                                maxx = Math.Max(maxx, p.X);
                                maxy = Math.Max(maxy, p.Y);
                            }
                            Rectangle rec = new Rectangle(minx,miny,maxx-minx,maxy-miny);
                            linear_gradient_brush = new LinearGradientBrush(rec, frontcolor, backcolor, linear_gradient_mode);
                            g.FillPolygon(linear_gradient_brush, drawPoint);
                        }
                        else if (colortype == 3)
                        {
                            hatch_brush = new HatchBrush(hatch_style, frontcolor, backcolor);
                            g.FillPolygon(hatch_brush, drawPoint);
                        }
                        else if (colortype == 4)
                        {
                            texture_brush = new TextureBrush(fill_img);
                            g.FillPolygon(texture_brush, drawPoint);
                        }
                        break;
                    case 12:
                        array_point.RemoveAt(array_point.Count - 1);
                        drawPoint = new Point[array_point.Count];
                        i = 0;
                        foreach (Point p in array_point) drawPoint[i++] = p;
                        g.DrawPolygon(pen, drawPoint);
                        break;
                        // TODO
                }
                domousemove = false;
                array_point.Clear();
                g.Dispose();
            }
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            drawselect = 3;
            uncheckallbutten();
            toolStripButton21.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            柔化ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            水平翻转ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            垂直翻转ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            顺时针旋转90度ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            逆时针旋转90度ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            drawselect = 4;
            uncheckallbutten();
            toolStripButton22.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            drawselect = 7;
            uncheckallbutten();
            toolStripButton23.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            drawselect = 8;
            uncheckallbutten();
            toolStripButton24.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            drawselect = 9;
            uncheckallbutten();
            toolStripButton25.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            drawselect = 10;
            uncheckallbutten();
            toolStripButton26.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            drawselect = 11;
            uncheckallbutten();
            toolStripButton27.Checked = true;
            Cursor = Cursors.Cross;
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            drawselect = 12;
            uncheckallbutten();
            toolStripButton28.Checked = true;
            Cursor = Cursors.Cross;
        }

        public Color endcolor
        {
            get { return end_color; }
            set { end_color = value; }
        }

        public Color frontcolor
        {
            get { return front_color; }
            set { front_color = value; }
        }

        public Color backcolor
        {
            get { return back_color; }
            set { back_color = value; }
        }

        public HatchStyle hatchStyle
        {
            get { return hatch_style; }
            set { hatch_style = value; }
        }

        public int colorType
        {
            get { return colortype; }
            set { colortype = value; }
        }

        public LinearGradientMode linearGradientMode
        {
            get { return linear_gradient_mode; }
            set { linear_gradient_mode = value; }
        }
    }
}
