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
                this.Text = fullname.Substring(fullname.LastIndexOf(@"\") + 1);
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
