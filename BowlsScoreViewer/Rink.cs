using System;
using System.Collections.Generic;

namespace BowlsScoreViewer
{
    public class Rink
    {
        public String SkipA = "Player A";
        public String SkipB = "Player B";
        private String LeadA = "";
        private String LeadB = "";
        private String SecondA = "";
        private String SecondB = "";
        private String ThirdA = "";
        private String ThirdB = "";
        private String RinkID;
        private String GameID;
        public bool QuarterA = false;
        public bool QuarterB = false;
        public Char Venue { get; private set; }    //'H' Home  'A' Away   'N' Neutral venue  ' ' Other (whistlestop, festival etc).
        public List<Event> Events;
        public bool ExtraEndIfRinkTied = false;
        public bool ExtraEndIfAggregateTied = false;
        public bool UsingSets = false;
        public DateTime LastRead;

        public Rink(string aGameString)
        {
            Events = new List<Event>();
            EventsFromString(aGameString);
        }

        public Rink()
        { 
        }

        private void ProcessEventsToTarget(int aTargetEnd, ref decimal aScore, ref decimal bScore, ref int aEnd, ref decimal aSet, ref decimal bSet)
        {
            decimal lAEnd = 0;
            decimal lBEnd = 0;
            bool lQuarterA = false;
            bool lQuarterB = false;

            foreach (Event e in Events)
            {
                if (aEnd < aTargetEnd)
                {
                    switch (e.Code)
                    {
                        case 'A':
                            //Add a row, set value in next char in col1, add to col2, add 1 to col 3, set 0 in col4 and add 0 to col5
                            lAEnd = Convert.ToDecimal(e.Value);
                            if (lQuarterA)
                                aScore += (lAEnd * 3 / 4);
                            else
                                aScore += lAEnd;

                            aEnd++;
                            lBEnd = 0;
                            break;

                        case 'B':
                            //Add a row, set 0 in col1, add 0 to col2, add 1 to col 3, set value in next char in col4 and add to col5
                            lBEnd = Convert.ToDecimal(e.Value);
                            if (lQuarterB)
                                bScore += (lBEnd * 3 / 4);
                            else
                                bScore += lBEnd;

                            aEnd++;
                            lAEnd = 0;
                            break;

                        case 'H':
                            //Set handicap - Show as end 0 in scorecard and set initial scores.
                            int commapos = e.Value.IndexOf(',', 0);
                            lAEnd = Convert.ToInt32(e.Value.Substring(0, commapos));
                            lBEnd = Convert.ToInt32(e.Value.Substring(commapos + 1, e.Value.Length - (commapos + 1)));
                            aScore = lAEnd;
                            bScore = lBEnd;
                            break;

                        case 'D':
                            //Need to decide what to do to indicate who killed the end
                            break;

                        case 'T':
                            lAEnd = 0;
                            lBEnd = 0;
                            aEnd++;
                            break;

                        case 'E':
                            if (aScore > bScore)
                            {
                                aSet++;
                            }
                            else if (aScore < bScore)
                            {
                                bSet++;
                            }
                            else
                            {
                                aSet = aSet + 0.5M;
                                bSet = bSet + 0.5M;
                            }
                            lAEnd = 0M;
                            lBEnd = 0M;
                            aScore = 0M;
                            bScore = 0M;
                            aEnd = 0;
                            break;

                        case 'F':
                            break;

                        case 'S':
                            break;

                        case 'Q':
                            if (e.Value == "A")
                            {
                                lQuarterA = !lQuarterA;
                            }
                            else if (e.Value == "B")
                            {
                                lQuarterB = !lQuarterB;
                            }
                            break;

                    }
                }
            }
        }

        public decimal CurrentScoreA()
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(System.Int32.MaxValue, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return aScore;
        }

        public decimal CurrentScoreB()
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(System.Int32.MaxValue, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return bScore;
        }

        public decimal CurrentSetScoreA()
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(System.Int32.MaxValue, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return aSet;
        }

        public decimal CurrentSetScoreB()
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(System.Int32.MaxValue, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return bSet;
        }
        public decimal CurrentSet()
        {
            if (Events[Events.Count - 1].Code == 'F')
            {
                //Game Finished
                return CurrentSetScoreB() + CurrentSetScoreA();
            }
            else
            {
                //Game In Progress
                return CurrentSetScoreB() + CurrentSetScoreA() + 1;
            }
        }

        public int CurrentEnd()
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(System.Int32.MaxValue, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return aEnd;
        }

        public decimal ScoreAtEndA(int aEndNum)
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(aEndNum, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return aScore;
        }

        public decimal ScoreAtEndB(int aEndNum)
        {
            decimal aScore = 0M;
            decimal bScore = 0M;
            int aEnd = 0;
            decimal aSet = 0M;
            decimal bSet = 0M;
            ProcessEventsToTarget(aEndNum, ref aScore, ref bScore, ref aEnd, ref aSet, ref bSet);
            return bScore;
        }

        private bool LoadRinkFromDB()
        {
            // Clear object, and load rink and events from DB
            return false;
        }

        public bool SaveRinkToDB()
        {
            //Save rink - could use separate calls to just update the events list or the timestamp
            return false;
        }

        private void AddEvent(Event aEvent)
        {
            Events.Add(aEvent);
//            SaveEventToDB();
            LastRead = DateTime.Now;
            SaveRinkToDB();
        }

        private void AddEvent(char code, string value)
        {
            Event e = new Event(code, value);
            Events.Add(e);
//            SaveEventToDB();
            LastRead = DateTime.Now;
            SaveRinkToDB();
        }

        public void DeleteLastEvent()
        {
            if (Events.Count > 0)
            {
                Events.RemoveAt(Events.Count - 1);
                //remove from database;
                LastRead = DateTime.Now;
                SaveRinkToDB();
            }
        }

        public string EventsToString()
        {
            string str = string.Empty;
            foreach (Event e in Events)
            {
                str = str + e.Code + ": " + e.Value+"\n";
            }
            return str;
        }

        public void EventsFromString(string aGameString)
        {
            Events.Clear();
            int lCurrChar = 0;
            while (lCurrChar < aGameString.Length)
            {

                switch (aGameString[lCurrChar])
                {
                    case 'A':
                        //Add a row, set value in next char in col1, add to col2, add 1 to col 3, set 0 in col4 and add 0 to col5
                        lCurrChar++;
                        AddEvent('A', (aGameString[lCurrChar] - 48).ToString());
                        lCurrChar++;
                        break;

                    case 'B':
                        //Add a row, set 0 in col1, add 0 to col2, add 1 to col 3, set value in next char in col4 and add to col5
                        lCurrChar++;
                        AddEvent('B', (aGameString[lCurrChar] - 48).ToString());
                        lCurrChar++;
                        break;

                    case 'H':
                        //Set handicap - Show as end 0 in scorecard and set initial scores.
                        lCurrChar++;
                        int lAEnd;
                        int lBEnd;
                        if (aGameString[lCurrChar] == '-')
                        {
                            lCurrChar++;
                            int lAVal = (aGameString[lCurrChar] - 48);
                            lAEnd = 0 - lAVal;
                        }
                        else
                        {
                            int lAVal = (aGameString[lCurrChar] - 48);
                            lAEnd = lAVal;
                        }
                        lCurrChar++;
                        if (aGameString[lCurrChar] == '-')
                        {
                            lCurrChar++;
                            int lBVal = (aGameString[lCurrChar] - 48);
                            lBEnd = 0 - lBVal;
                        }
                        else
                        {
                            int lBVal = (aGameString[lCurrChar] - 48);
                            lBEnd = lBVal;
                        }
                        lCurrChar++;
                        AddEvent('H', lAEnd + "," + lBEnd);
                        break;

                    case 'D':
                        //Need to decide what to do to indicate who killed the end
                        lCurrChar++;
                        if (aGameString[lCurrChar] == 'A')
                        {
                            AddEvent('D', "A");
                        }
                        else
                        {
                            AddEvent('D', "B");
                        }
                        lCurrChar++;
                        break;

                    case 'T':
                        lCurrChar++;
                        AddEvent('T', "");
                        break;

                    case 'E':
                        lCurrChar++;
                        AddEvent('E', "");
                        break;

                    case 'F':
                        lCurrChar++;
                        AddEvent('F', "");
                        break;

                    case 'S':
                        lCurrChar++;
                        AddEvent('S', "");
                        break;
                    case 'Q':
                        lCurrChar++;
                        if (aGameString[lCurrChar] == 'A')
                        {
                            AddEvent('Q', "A");
                        }
                        else if (aGameString[lCurrChar] == 'B')
                        {
                            AddEvent('Q', "B");
                        }
                        lCurrChar++;
                        break;

                    default: //Ignore invalid characters in the string
                        lCurrChar++;
                        break;
                }
            }

        }
    }
}
