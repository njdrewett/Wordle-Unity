using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoSingleton<PlayerStatistics> {
    public int gamesPlayed { get; private set; } = 0;
    public int gamesWon { get; private set; } = 0;
    public int winPercentage { get; private set; } = 0;
    public int currentStreak { get; private set; } = 0;
    public int maxStreak { get; private set; } = 0;

    private Dictionary<int, int> guessDistribution = new Dictionary<int, int>();

    public void recordWin(int numberOfGuesses) {
        gamesPlayed++;
        gamesWon++;
        currentStreak++;
        decimal percentage = (decimal)gamesWon / gamesPlayed;

        winPercentage = (int)(percentage * 100);
        if (currentStreak > maxStreak) {
            maxStreak = currentStreak;
        }

        if (guessDistribution.ContainsKey(numberOfGuesses)) {
            int distribution = guessDistribution[numberOfGuesses];
            distribution++;
            guessDistribution[numberOfGuesses] = distribution;
        } else {
            guessDistribution[numberOfGuesses] = 1;
        }
    }

    public void recordLose() {
        gamesPlayed++;

        decimal percentage = (decimal)gamesWon / gamesPlayed;

        winPercentage = (int)(percentage * 100);
        if (currentStreak > maxStreak) {
            maxStreak = currentStreak;
        }
        currentStreak = 0;
    }

}
