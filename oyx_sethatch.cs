using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ouyangxu
{
    public partial class oyx_sethatch : Form
    {
        public oyx_sethatch()
        {
            InitializeComponent();
        }

        oyx_main ff = new oyx_main();
        HatchStyle[] hss = new HatchStyle[]
        {
            HatchStyle.BackwardDiagonal,
            HatchStyle.Cross,
            HatchStyle.DarkDownwardDiagonal,
            HatchStyle.DarkHorizontal,
            HatchStyle.DarkUpwardDiagonal,
            HatchStyle.DarkVertical,
            HatchStyle.DashedDownwardDiagonal,
            HatchStyle.DashedHorizontal,
            HatchStyle.DashedUpwardDiagonal,
            HatchStyle.DashedVertical,
            HatchStyle.DiagonalBrick,
            HatchStyle.DiagonalCross,
            HatchStyle.Divot,
            HatchStyle.DottedDiamond,
            HatchStyle.DottedGrid,
            HatchStyle.ForwardDiagonal,
            HatchStyle.Horizontal,
            HatchStyle.HorizontalBrick,
            HatchStyle.LargeCheckerBoard,
            HatchStyle.Vertical,
            HatchStyle.Wave,
            HatchStyle.ZigZag
        };

        private void oyx_sethatch_Load(object sender, EventArgs e)
        {
            ff = (oyx_main)this.Owner;
            this.button1.BackColor = ff.startcolor;
            this.button2.BackColor = ff.endcolor;
            for (int i = 0; i < hss.Length; i++)
            {
                this.comboBox1.Items.Add(hss[i].ToString());
                if (ff.hatchStyle.ToString() == hss[i].ToString()) comboBox1.SelectedIndex = i;
            }
        }

        private void preview()
        {
            Graphics g = this.CreateGraphics();
            Rectangle rec = new Rectangle(202, 115, 90, 60);
            HatchBrush hb = new HatchBrush(hss[comboBox1.SelectedIndex], button1.BackColor, button2.BackColor);
            g.FillEllipse(hb, rec);
        }

        private void oyx_sethatch_Paint(object sender, PaintEventArgs e)
        {
            preview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = this.button1.BackColor;
            if(cd.ShowDialog() == DialogResult.OK)
            {
                this.button1.BackColor = cd.Color;
                preview();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = this.button2.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.button2.BackColor = cd.Color;
                preview();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ff.startcolor = this.button1.BackColor;
            ff.endcolor = this.button2.BackColor;
            ff.hatchStyle = hss[comboBox1.SelectedIndex];
            ff.colorType = 3;
            this.Close();
        }
    }
}
