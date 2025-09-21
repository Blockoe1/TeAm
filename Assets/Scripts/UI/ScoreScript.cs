using System;
using UnityEngine;

public static class ScoreScript
{
    //public static int Score = 0;
    private static int score = 0;
    public static int Bitcoin = 0;
    public static int TimerSinceGettingBitcoin = 0;

    public static event Action<int> OnScoreChanged;

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }
}
