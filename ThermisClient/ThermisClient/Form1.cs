namespace ThermisClient
{
    public partial class Form1 : Form
    {
        LogicAbstraction logicAbstraction;
        public Form1(LogicAbstraction logicAbstraction)
        {
            InitializeComponent();
            CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Thermis";

            this.logicAbstraction = logicAbstraction;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            logicAbstraction.onLogin(this.label1.Text, this.label2.Text);
        }
    }
}