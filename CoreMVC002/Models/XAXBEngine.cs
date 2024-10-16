using System;
using System.Collections.Generic;

namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        public List<string> GuessHistory { get; set; } // 紀錄每次猜測的歷史
        public int GuessCount { get; private set; } // 累計猜測次數

        public XAXBEngine()
        {
            Secret = GenerateRandomSecret();
            GuessHistory = new List<string>();
            GuessCount = 0;
            Guess = null;
            Result = null;
        }

        public XAXBEngine(string secretNumber)
        {
            Secret = secretNumber;
            GuessHistory = new List<string>();
            GuessCount = 0;
            Guess = null;
            Result = null;
        }

        private string GenerateRandomSecret()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString(); // 隨機產生四位數的秘密數字
        }

        public int numOfA(string guessNumber)
        {
            int aCount = 0;
            for (int i = 0; i < Secret.Length; i++)
            {
                if (Secret[i] == guessNumber[i])
                {
                    aCount++;
                }
            }
            return aCount;
        }

        public int numOfB(string guessNumber)
        {
            int bCount = 0;
            bool[] matched = new bool[Secret.Length];

            for (int i = 0; i < Secret.Length; i++)
            {
                if (Secret[i] == guessNumber[i])
                {
                    matched[i] = true; 
                }
            }

            for (int i = 0; i < Secret.Length; i++)
            {
                for (int j = 0; j < Secret.Length; j++)
                {
                    if (i != j && !matched[j] && Secret[j] == guessNumber[i])
                    {
                        bCount++;
                        matched[j] = true; 
                        break;
                    }
                }
            }
            return bCount;
        }

        public bool IsGameOver(string guessNumber)
        {
            GuessCount++; // 累計猜測次數
            Guess = guessNumber;
            int aCount = numOfA(guessNumber);
            int bCount = numOfB(guessNumber);
            Result = $"{aCount}A{bCount}B";
            GuessHistory.Add($"猜測: {Guess}, 結果: {Result}"); // 紀錄猜測歷史

            return aCount == Secret.Length; // 如果猜對，返回 true
        }

        public void ResetGame()
        {
            Secret = GenerateRandomSecret();
            GuessHistory.Clear();
            GuessCount = 0;
            Guess = null;
            Result = null;
        }

        // 所有猜測歷史
        public string ShowGuessHistory()
        {
            return string.Join("\n", GuessHistory);
        }

        // 累計猜測次數
        public int ShowGuessCount()
        {
            return GuessCount;
        }
    }
}
