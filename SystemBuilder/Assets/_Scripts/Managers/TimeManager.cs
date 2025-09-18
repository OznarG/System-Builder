using System;
using TMPro;
using UnityEngine;

#region Enums
public enum DayEnum
{
    Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
}

public enum StationEnum
{
    Spring, Summer, Fall, Winter
}
#endregion

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int minutes;
    public int Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }
    private int hours;
    public int Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }
    private int days;
    public int Days { get { return days; } set { days = value; OnDaysChange(value); } }

    private float tempSeconds;

    [SerializeField] private TMP_Text minute;
    [SerializeField] private TMP_Text hour;

    public DayEnum day;
    public StationEnum yearStation;

    public float roamerCalculator;
    public void Update()
    {
        tempSeconds += Time.deltaTime;
        if (tempSeconds >= 1)
        {
            Minutes += 1;
            tempSeconds = 0;
        }
    }

    private void OnMinutesChange(int value)
    {
        //Debug.Log("Minutes Update");
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
        minute.text = minutes.ToString();
    }
    private void OnHoursChange(int value)
    {
        Debug.Log("Hour change called");
        hour.text = hours.ToString();
    }
    private void OnDaysChange(int value)
    {
        throw new NotImplementedException();
    }
}
