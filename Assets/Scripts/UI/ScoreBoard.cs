using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int amountScore)
    {
        scoreText.text = amountScore.ToString();
    }

}
