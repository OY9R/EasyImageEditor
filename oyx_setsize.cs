using System;
using System.Windows.Forms;

namespace ouyangxu
{
    public partial class oyx_setsize : Form
    {
        public oyx_setsize()
        {
            InitializeComponent();
        }
        oyx_main ff;
        private void oyx_setsize_Load(object sender, EventArgs e)
        {
            ff = (oyx_main)this.Owner;
            text_height.Text = ff.ImageHeight.ToString();
            text_width.Text = ff.ImageWidth.ToString();
            text_newheight.Text = ff.ImageHeight.ToString();
            text_newwidth.Text = ff.ImageWidth.ToString();
        }
        int active_tbox = 0;
        private void text_newwidth_TextChanged(object sender, EventArgs e)
        {
            if (active_tbox != 1 || text_newwidth.Text.Length == 0) return;
            int newwidth;
            if (!int.TryParse(text_newwidth.Text.Trim(), out newwidth))
            {
                MessageBox.Show("请输入正确的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ck_constraint.Checked) return;
            double d = int.Parse(text_height.Text) * int.Parse(text_newwidth.Text) / int.Parse(text_width.Text);
            text_newheight.Text = (Math.Ceiling(d)).ToString();
        }

        private void text_newheight_TextChanged(object sender, EventArgs e)
        {
            if (active_tbox != 2 || text_newheight.Text.Length == 0) return;
            int newheight;
            if (!int.TryParse(text_newheight.Text.Trim(), out newheight))
            {
                MessageBox.Show("请输入正确的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ck_constraint.Checked) return;
            double d = int.Parse(text_width.Text) * int.Parse(text_newheight.Text) / int.Parse(text_height.Text);
            text_newwidth.Text = (Math.Ceiling(d)).ToString();
        }

        private void text_newwidth_Enter(object sender, EventArgs e)
        {
            active_tbox = 1;
        }

        private void text_newheight_Enter(object sender, EventArgs e)
        {
            active_tbox = 2;
        }

        private void text_newwidth_Leave(object sender, EventArgs e)
        {
            if (text_newwidth.Text.Length == 0)
            {
                MessageBox.Show("请输入新的尺寸！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                active_tbox = 0;
            }
        }

        private void text_newheight_Leave(object sender, EventArgs e)
        {
            if (text_newheight.Text.Length == 0)
            {
                MessageBox.Show("请输入新的尺寸！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                active_tbox = 0;
            }
        }

        private void ck_constraint_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_constraint.Checked)
            {
                text_newheight.Text = text_height.Text;
                text_newwidth.Text = text_width.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int newwidth, newheight;
            if (int.TryParse(text_newheight.Text, out newheight) && int.TryParse(text_newwidth.Text, out newwidth))
            {
                ff.ImageHeight = newheight;
                ff.ImageWidth = newwidth;
            }
            else
            {
                MessageBox.Show("请输入正确的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }
    }
}
