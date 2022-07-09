using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticsPanel : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI played;
    [SerializeField]
    private TextMeshProUGUI winPercentage;
    [SerializeField]
    private TextMeshProUGUI currentStreak;
    [SerializeField]
    private TextMeshProUGUI maxStreak;

    public void refreshStatistics() {
        PlayerStatistics stats = PlayerStatistics.Instance;

        played.SetText(""+stats.gamesPlayed);
        winPercentage.SetText("" + stats.winPercentage);
        currentStreak.SetText("" + stats.currentStreak);
        maxStreak.SetText("" + stats.maxStreak);
    }
}
