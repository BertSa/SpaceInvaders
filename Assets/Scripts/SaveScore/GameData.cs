using System;

namespace SaveScore
{
    [Serializable]
    public class GameData
    {
        public int Score { get; }
        public string Name { get; }

        public GameData(int scoreInt, string nameStr)
        {
            Score = scoreInt;
            Name = nameStr;
        }
    }
}