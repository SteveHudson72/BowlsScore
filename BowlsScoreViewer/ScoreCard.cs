using System;
using System.Drawing;
using System.Windows.Forms;
using DGVPrinterHelper;


namespace BowlsScoreViewer
{

    public partial class fmScoreCard : Form
    {
        public static String lNameA = "Team A";
        public static String lNameB = "Team B";

        public fmScoreCard(string aGameString, string aNameA, string aNameB, string aRink, string aTitle, DateTime aDate)
        {
            InitializeComponent();
            bool isValid;
            int EndVal = 0;
            decimal lAEnd;
            decimal lBEnd;
            decimal lAScore = 0M;
            decimal lBScore = 0M;
            decimal lASet = 0M;
            decimal lBSet = 0M;

            int lenGameStr = aGameString.Length;

            isValid = lenGameStr >= 2;

            //isValid = isValid && ((aGameString.StartsWith("S") || (aGameString.StartsWith("H"))) || (aGameString.StartsWith("Q")));
            //isValid = isValid && aGameString.EndsWith("F");

            if (isValid)
            {
                Rink R = new Rink(aGameString);

                if (!String.IsNullOrEmpty(aNameA)) 
                {
                    R.SkipA = aNameA;
                    label1.Text = aNameA;
                }
                else
                {
                    R.SkipA = lNameA;
                }

                if (!String.IsNullOrEmpty(aNameB))
                {
                    R.SkipB = aNameB;
                    label2.Text = aNameB;
                }
                else
                {
                    R.SkipB = lNameB;
                }

                if (!String.IsNullOrEmpty(aTitle))
                {
                    label3.Text = aTitle;
                }
                else
                {
                    label3.Visible = false;
                }
            
            if (!String.IsNullOrEmpty(aRink))
                {
                    label4.Text = "Rink : " + aRink;
                }
                else
                {
                    label4.Visible = false;
                }

                label5.Text = "Date : " + aDate.ToString();

                R.QuarterA = false;
                R.QuarterB = false;

                foreach (Event e in R.Events)         
                {

                    switch (e.Code)
                    {
                        case 'A':
                            //Add a row, set value in next char in col1, add to col2, add 1 to col 3, set 0 in col4 and add 0 to col5
                            lAEnd = Convert.ToDecimal(e.Value);
                            if (R.QuarterA)
                                lAScore += ( lAEnd * 3/4);
                            else
                                lAScore += lAEnd;

                            EndVal++;
                            lBEnd = 0;
                            dataGridView1.RowCount++;
                            dataGridView1[0, dataGridView1.RowCount - 1].Value = lAEnd;
                            dataGridView1[1, dataGridView1.RowCount - 1].Value = lAScore.ToString("0.##");
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = EndVal;
                            dataGridView1[3, dataGridView1.RowCount - 1].Value = lBEnd;
                            dataGridView1[4, dataGridView1.RowCount - 1].Value = lBScore.ToString("0.##");
                            break;

                        case 'B':
                            //Add a row, set 0 in col1, add 0 to col2, add 1 to col 3, set value in next char in col4 and add to col5
                            lBEnd = Convert.ToDecimal(e.Value);
                            if (R.QuarterB)
                                lBScore += (lBEnd * 3/4);
                            else
                                lBScore += lBEnd;

                            EndVal++;
                            lAEnd = 0;
                            dataGridView1.RowCount++;
                            dataGridView1[0, dataGridView1.RowCount - 1].Value = lAEnd;
                            dataGridView1[1, dataGridView1.RowCount - 1].Value = lAScore.ToString("0.##");
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = EndVal;
                            dataGridView1[3, dataGridView1.RowCount - 1].Value = lBEnd;
                            dataGridView1[4, dataGridView1.RowCount - 1].Value = lBScore.ToString("0.##");
                            break;

                        case 'H':
                            //Set handicap - Show as end 0 in scorecard and set initial scores.
                            int commapos = e.Value.IndexOf(',', 0);
                            lAEnd = Convert.ToInt32(e.Value.Substring(0, commapos));
                            lBEnd = Convert.ToInt32(e.Value.Substring(commapos+1, e.Value.Length-(commapos+1)));
                            lAScore = lAEnd;
                            lBScore = lBEnd;
                            dataGridView1.RowCount++;
                            dataGridView1[0, dataGridView1.RowCount - 1].Value = lAEnd;
                            dataGridView1[1, dataGridView1.RowCount - 1].Value = lAScore;
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = "Handicap";
                            dataGridView1[3, dataGridView1.RowCount - 1].Value = lBEnd;
                            dataGridView1[4, dataGridView1.RowCount - 1].Value = lBScore;
                            break;

                        case 'D':
                            //Need to decide what to do to indicate who killed the end
                            dataGridView1.RowCount++;
                            if (e.Value == "A")
                                dataGridView1[0, dataGridView1.RowCount - 1].Value = "***";
                            else
                                dataGridView1[3, dataGridView1.RowCount - 1].Value = "***";
                            //dataGridView1[1, dataGridView1.RowCount - 1].Value = lAScore;
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = "Dead End";
                            //dataGridView1[4, dataGridView1.RowCount - 1].Value = lBScore;
                            break;

                        case 'T':
                            lAEnd = 0;
                            lBEnd = 0;
                            EndVal++;
                            dataGridView1.RowCount++;
                            dataGridView1[0, dataGridView1.RowCount - 1].Value = lAEnd;
                            dataGridView1[1, dataGridView1.RowCount - 1].Value = lAScore.ToString("0.##");
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = EndVal;
                            dataGridView1[3, dataGridView1.RowCount - 1].Value = lBEnd;
                            dataGridView1[4, dataGridView1.RowCount - 1].Value = lBScore.ToString("0.##");
                            break;

                        case 'E':
                            if (lAScore > lBScore)
                            {
                                lASet++;
                            }
                            else if (lAScore < lBScore)
                            {
                                lBSet++;
                            }
                            else 
                            {
                                lASet = lASet + 0.5M;
                                lBSet = lBSet + 0.5M;
                            }
                            lAEnd = 0;
                            lBEnd = 0;
                            lAScore = 0M;
                            lBScore = 0M;
                            EndVal = 0;
                            dataGridView1.RowCount++;
                            dataGridView1[0, dataGridView1.RowCount - 1].Value = "";
                            dataGridView1[1, dataGridView1.RowCount - 1].Value = lASet;
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = "End of Set";
                            dataGridView1[3, dataGridView1.RowCount - 1].Value = "";
                            dataGridView1[4, dataGridView1.RowCount - 1].Value = lBSet;
                            break;

                        case 'F':
                            break;

                        case 'S':
                            break;

                        case 'Q':
                            dataGridView1.RowCount++;
                            if (e.Value == "A")
                            {
                                if (R.QuarterA)
                                    dataGridView1[0, dataGridView1.RowCount - 1].Value = "Q-";
                                else
                                    dataGridView1[0, dataGridView1.RowCount - 1].Value = "Q+";
                                R.QuarterA = !R.QuarterA;
                            }
                            else if (e.Value == "B")
                            {
                                if (R.QuarterB)
                                    dataGridView1[3, dataGridView1.RowCount - 1].Value = "Q-";
                                else
                                    dataGridView1[3, dataGridView1.RowCount - 1].Value = "Q+";
                                R.QuarterB = !R.QuarterB;
                            }
                            dataGridView1[2, dataGridView1.RowCount - 1].Value = "Quarter";
                            break;

                        default:  //ignore invalid codes in the event list
                            break;

                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();

            printer.Title = label3.Text;

            printer.SubTitle = label1.Text + "          V          " + label2.Text;

            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |

                                          StringFormatFlags.NoClip;

            printer.PageNumbers = false;

            printer.PageNumberInHeader = false;

            printer.PorportionalColumns = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.Footer =  "© 2026 Steve Hudson - Bowls Score Viewer";

            printer.FooterSpacing = 15;



            printer.PrintDataGridView(dataGridView1);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

     

    }
}

