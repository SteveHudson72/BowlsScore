using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BowlsScoreViewer.Rink;


namespace BowlsScoreViewer
{
    class Program
    {
        static void Main(string[] args)
        {

            //            string aGameString = "SA2B1A1B2DAB3B2A4A1B1A1B1B1A1A3B1DBA2B2F";  /* Simple game */
                        string aGameString = "SA2B1A1B2DAB3B2A4A1B1A1B1B1A1A3B1DBA2";  /* Incomplete Simple game */
            //            string aGameString = "H-10SA1B2DAA2A3A2B2B1A1TDBB2A2A1B3B2B1A1B2B1DAB1B1A3B1B1F";   /* Handicap */
            //            string aGameString = "SQBA1B2DAA2A3A2B2B1A1TDBB2A2A1B3B2B1B2B1DAB1B1A1B1B1F";    /* Quartering for one team */
            //            string aGameString = "SQBA1B2DAA2A3A2QAB2B1A1TDBB2A2A1B3B2B1B2B1DAB1A2B1B1B1F";    /* Quartering for one team and partial quartering for other */
            //            string aGameString = "SA1B2DAA2A3A2B2B1A1TDBB2EA2A1B3B2B1B2B1DAB1B1EB1B1EF";   /* Sets game */
            //            string aGameString = "SA1B2DAA2A3A2B2B1A1TDBB2EA2A1B3B2B1B2B1DAB1B1EB1";    /* Incomplete Sets game */
            
            string aNameA = "Steve";
            string aNameB = "Andrew";

            Rink R = new Rink(aGameString);

            if (!String.IsNullOrEmpty(aNameA))
            {
                R.SkipA = aNameA;
            }
            if (!String.IsNullOrEmpty(aNameB))
            {
                R.SkipB = aNameB;
            }

            R.UsingSets = aGameString.Contains('E');
            Console.WriteLine(aGameString);
            Console.WriteLine($"{R.Events.Count} events");
            Console.WriteLine($"{R.CurrentScoreA()} - {R.CurrentScoreB()} after {R.CurrentEnd()} ends.");
            if (R.UsingSets)
            {
                Console.WriteLine($"Sets {R.CurrentSetScoreA()} - {R.CurrentSetScoreB()} in set {R.CurrentSet()}.");
            }
            Console.WriteLine($"{R.SkipA} after 7 ends had {R.ScoreAtEndA(7)} shots.");
            Console.WriteLine($"{R.SkipB} after 6 ends had {R.ScoreAtEndB(6)} shots.");
            Console.WriteLine("Events:\n"+R.EventsToString());
            R.DeleteLastEvent();
            Console.WriteLine("Final end removed.");
            Console.WriteLine($"{R.Events.Count} events");
            Console.WriteLine($"{R.CurrentScoreA()} - {R.CurrentScoreB()} after {R.CurrentEnd()} ends.");
            if (R.UsingSets)
            {
                Console.WriteLine($"Sets {R.CurrentSetScoreA()} - {R.CurrentSetScoreB()} in set {R.CurrentSet()}.");
            }
            Console.WriteLine($"{R.SkipA} after 7 ends had {R.ScoreAtEndA(7)} shots.");
            Console.WriteLine($"{R.SkipB} after 6 ends had {R.ScoreAtEndB(6)} shots.");
            Console.WriteLine("Events:\n" + R.EventsToString());

            Console.ReadLine();
        }
    }
}
