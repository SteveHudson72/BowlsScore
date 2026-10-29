using System;
using System.Collections.Generic;

namespace BowlsScoreViewer
{
    public class Match
    {
        private String Team1 = "Team A";
        private String Team2 = "Team B";
        private String Competition;
        private String GameID;
        private int TargetScore = 0;
        private int MaxEnds;
        private char MatchType;  //'E'  Ends   'T'  Target Score     'S'    Sets   'H'   Timed
        private Char Venue;    //'M' Mixed (i.e. rinks at home and away)   'H' Home only  'A' Away only   'N' Neutral venue  ' ' Other (whistlestop, festival etc).
        public List<Rink> Rinks;
        private int PointsPerRink = 2;
        private int PointsForAggregate = 0;
        public bool AllowAggregateTie = true;


        public Match()
        {
            //Match Constructor
        }

        private void AddRink(Rink aRink)
        { }

        private void DeleteRink(string aRink)
        { }

        public decimal CurrentAggregateA()
        {
            decimal agg = 0M;
            foreach (Rink r in Rinks)
            {
                agg = agg + r.CurrentScoreA();
            }
            return agg;
        }

        public decimal CurrentAggregateB()
        {
            decimal agg = 0M;
            foreach (Rink r in Rinks)
            {
                agg = agg + r.CurrentScoreB();
            }
            return agg;
        }

        public decimal AggregateAtEndA(int aEnd)
        {
            decimal agg = 0M;
            foreach (Rink r in Rinks)
            {
                agg = agg + r.ScoreAtEndA(aEnd);
            }
            return agg;
        }

        public decimal AggregateAtEndB(int aEnd)
        {
            decimal agg = 0M;
            foreach (Rink r in Rinks)
            {
                agg = agg + r.ScoreAtEndB(aEnd);
            }
            return agg;
        }

        public decimal LastAvailAggregateA()
        {
            decimal agg = 0M;
            foreach (Rink r in Rinks)
            {
                agg = agg + r.ScoreAtEndA(LastAvailAggregateEnd());
            }
            return agg;
        }

        public decimal LastAvailAggregateB()
        {
            decimal agg = 0M;
            foreach (Rink r in Rinks)
            {
                agg = agg + r.ScoreAtEndB(LastAvailAggregateEnd());
            }
            return agg;
        }

        public int LastAvailAggregateEnd()
        {
            int lastComplete = Int32.MaxValue;
            foreach (Rink r in Rinks)
            {
                lastComplete = Math.Min(lastComplete, r.CurrentEnd());
            }
            return lastComplete;
        }

        public decimal HomeTotalA()
        {
            decimal agg = 0M;
            return agg;
        }

        public decimal AwayTotalA()
        {
            decimal agg = 0M;
            return agg;
        }

        public decimal HomeTotalB()
        {
            decimal agg = 0M;
            return agg;
        }

        public decimal AwayTotalB()
        {
            decimal agg = 0M;
            return agg;
        }

        public int AwayEnd()
        {
            return 0;
        }

        public int HomeEnd()
        {
            return 0;
        }

        private bool LoadGame()
        {
            // Load game
            // Load all rinks
            return true;
        }

        private bool SaveGame()
        {
            bool gameSaved = false;
            bool rinkSaved = true;
            // if Save Game to DB then gameSaved = true

            foreach (Rink r in Rinks) 
            {
                rinkSaved = rinkSaved && r.SaveRinkToDB();
            }
            return gameSaved && rinkSaved;
        }


    }
}
