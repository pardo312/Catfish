using System;
public static class ScoreManager
{
    public static int score = 0;
    public static Action onChangeScore;

    public static void ChangeScore(int newScore)
    {
        score = newScore;
        onChangeScore?.Invoke();
    }

    public static void ResetPlayScore()
    {
        score = 0;
    }
}
