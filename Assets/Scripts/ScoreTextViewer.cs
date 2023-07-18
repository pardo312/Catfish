using TMPro;
using UnityEngine;

public class ScoreTextViewer : MonoBehaviour
{
    public bool isMaxScore;
    public TMP_Text scoreValue;

    public void Update()
    {
        if (isMaxScore)
            scoreValue.text = PlayerPrefs.GetInt("Score").ToString();
        else
            scoreValue.text = ScoreManager.score.ToString();
    }
}
