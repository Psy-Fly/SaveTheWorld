using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalText;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI finalScore;

    public void SetFinalText(string text)
    {
        finalText.text = text;
    }
    public void SetBestScore(int score)
    {
        bestScore.text = score.ToString();
    }
    public void SetFinalScore(int score)
    {
        finalScore.text = score.ToString();
    }
}
