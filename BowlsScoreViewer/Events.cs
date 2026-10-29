using System;

namespace BowlsScoreViewer
{
    public class Event
    {

        public char Code { get; set; }
        public string Value { get; set; }

        public Event()
        {
            Code = ' ';
            Value = String.Empty;
        }

        public Event(char code, string value)
        {
            Code = code;
            Value = value;
        }

        public void ScoreA(int aScore)
        {
            Code = 'A';
            Value = aScore.ToString();
        }

        public void ScoreB(int aScore)
        {
            Code = 'B';
            Value = aScore.ToString();
        }

        public void TiedEnd()
        {
            Code = 'T';
            //Value = String.Empty;
        }

        public void KilledEnd(char aSide)
        {
            Code = 'D';
            Value = aSide.ToString();
        }

        public void ToggleQuarter(char aSide)
        {
            Code = 'Q';
            Value = aSide.ToString();
        }

        public void EndSet()
        {
            Code = 'E';
            //Value = String.Empty;
        }

        public void Handicap(int aHandicapA, int aHandicapB)
        {
            Code = 'H';
            Value = aHandicapA + "," + aHandicapB;
        }

        public void StartGame() 
        {
            Code = 'S';
            //Value = String.Empty;
        }

        public void EndGame()
        {
            Code = 'F';
            //Value = String.Empty;
        }

    }
}
