using System;
using System.Windows.Forms;

namespace BowlsScoreViewer
{
    public partial class BSVForm : Form
    {
        public BSVForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form myScoreCard = new fmScoreCard(textBox1.Text.ToUpper(), txtNameA.Text, txtNameB.Text, textBox4.Text, textBox5.Text, dateTimePicker1.Value);
            myScoreCard.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
