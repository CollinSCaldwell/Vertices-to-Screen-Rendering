using System;
using System.Windows.Forms;

namespace Graphics
{
    public partial class Form1 : Form
    {
        readonly private int height = 400;
        readonly private int width = 700;
        Main engine;

        public Form1()
        {
            engine = new Main(height, width);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            engine.CallToRun(ref displayPicture, this);
        }
    }
}
