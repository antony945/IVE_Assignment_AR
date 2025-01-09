using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    TMP_Text timerText;
    private float remainingTime;

    [SerializeField]
    float maximumTime;

    // Start is called before the first frame update
    void OnEnable()
    {
        remainingTime = maximumTime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool TimeIsExpired()
    { return remainingTime <= 0; }

    public float GetRemainingTime()
    { return remainingTime; }

    public float GetMaximumTime()
    { return maximumTime; }
}
