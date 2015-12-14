using System;

namespace FeelTheBern
{
    public class ScoreSingleton
    {
        static ScoreSingleton instance;
        public static ScoreSingleton getInstance()
        {
            if (instance == null)
            {
                instance = new ScoreSingleton();
            }
            return instance;
        }
        private ScoreSingleton()
        {
        }
        public int playerScore{get; set;}
    }
}

