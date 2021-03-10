namespace DefaultNamespace
{
    [System.Serializable]
    public class GameData
    {
        public int score;
        public string name;

        public GameData(int scoreInt, string nameStr)
        {
            score = scoreInt;
            name = nameStr;
        }
    }
}