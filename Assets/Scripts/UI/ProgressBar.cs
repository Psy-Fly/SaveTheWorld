using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float timeToDifficultUp = 10;
    public int gameStageId = 0;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = timeToDifficultUp;
        
    }

    private void Update()
    {
        slider.value += Time.deltaTime;
        if (slider.value >= timeToDifficultUp)
        {
            gameStageId++;
            GameManager.instance.DifficultUp();
        }
    }
    
    public void ResetProgressBar(float nextTimeToDifficultUp)
    {
        slider.value = 0;
        timeToDifficultUp = nextTimeToDifficultUp;
        slider.maxValue = timeToDifficultUp;
    }
}
