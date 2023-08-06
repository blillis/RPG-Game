using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400;
    const float phaseLength = 3600f; //1 hour in seconds

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    float time;

    [SerializeField] float timeScale = 60f;
    [SerializeField] float startAtTime = 28800f; //8:00am in seconds

    [SerializeField] Text text;
    [SerializeField] Light2D globalLight;
    private int days;

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
    }

    public void Subscribe(TimeAgent timeagent)
    {
        agents.Add(timeagent);
    }

    public void UnSubscribe(TimeAgent timeagent)
    {
        agents.Remove(timeagent);
    }

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        TimeValueCalculation();
        Daylight();
        if (time > secondsInDay)
        {
            NextDay();
        }
        TimeAgents();
    }

    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void Daylight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
    }

    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(time / phaseLength);
        Debug.Log(currentPhase);

        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
        
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
