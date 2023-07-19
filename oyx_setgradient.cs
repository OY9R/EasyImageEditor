using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ouyangxu{
    public partial class oyx_setgradient : Form{
        public oyx_setgradient(){
            InitializeComponent();
        }

        oyx_main ff = new oyx_main();
        LinearGradientMode[] lgms = new LinearGradientMode[]{
            LinearGradientMode.Horizontal,
            LinearGradientMode.Vertical,
            LinearGradientMode.BackwardDiagonal,
            LinearGradientMode.ForwardDiagonal
        };

        private void oyx_setgradient_Load(object sender, EventArgs e){
            ff = (oyx_main)this.Owner;
            this.button1.BackColor = ff.startcolor;
            this.button2.BackColor = ff.endcolor;
            for (int i = 0; i < lgms.Length; i++){
                if (ff.linearGradientMode.ToString() == lgms[i].ToString()) comboBox1.SelectedIndex = i;
            }
        }

        private void preview(){
            Graphics g = this.CreateGraphics();
            Rectangle rec = new Rectangle(180, 100, 100, 80);
            LinearGradientBrush hb = new LinearGradientBrush(rec, button1.BackColor, button2.BackColor, lgms[comboBox1.SelectedIndex]);
            g.FillRectangle(hb, rec);
        }

        private void button3_Click(object sender, EventArgs e){
            ff.startcolor = this.button1.BackColor;
            ff.endcolor = this.button2.BackColor;
            ff.linearGradientMode = lgms[comboBox1.SelectedIndex];
            ff.colorType = 2;
            this.Close();
        }

        private void oyx_setgradient_Paint(object sender, PaintEventArgs e){
            preview();
        }

        private void button1_Click(object sender, EventArgs e){
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = this.button1.BackColor;
            if (cd.ShowDialog() == DialogResult.OK){
                this.button1.BackColor = cd.Color;
                preview();
            }
        }

        private void button2_Click(object sender, EventArgs e){
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = this.button1.BackColor;
            if (cd.ShowDialog() == DialogResult.OK){
                this.button2.BackColor = cd.Color;
                preview();
            }
        }

        private void button4_Click(object sender, EventArgs e){
            this.Close();
        }
    }
}
