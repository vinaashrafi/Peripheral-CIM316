using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region GameManager Event Bus
    private static GameManager _current;
    public static GameManager Current { get { return _current; } }

    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(this.gameObject);
        } else {
            _current = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    private float degreePerSecond;
    private Vector3 rotation;
    public GameObject sun;
    public float worldTimeSeconds;
    public float worldTimeMinutes;
    public float worldTimeHours = 7;
    public float worldTimeDays;
    private float _timer;
    private float WorldClockUpdater;

    public float secondsInADay;
    public float minutesInADay;
    public float hoursInADay;
    private float _totalSecondsInADay;
    public Vector3 _timePerSecond;

    private void Start()
    {
        _totalSecondsInADay = secondsInADay + minutesInADay*60 + hoursInADay*3600;
        CalculateDayCycleDegrees();
        CalculateTimePerSecond();
    }

    #region Day and Night Cycle Calculations

    private void CalculateDayCycleDegrees()
    {
        degreePerSecond = 360/_totalSecondsInADay;
    }

    private void CalculateTimePerSecond()
    {
        int tempRoundedMins = 0;
        int tempRoundedHours = 0;
        float tempSeconds = 0f;
        float tempMinutes = 0f;
        float tempHours = 0f;
        
         tempSeconds = 86400/_totalSecondsInADay;
         tempMinutes = tempSeconds/60;
        tempRoundedMins = Mathf.FloorToInt(tempMinutes);
        tempSeconds -= tempRoundedMins*60;
        if (tempRoundedMins >= 60)
        {
            tempHours = tempRoundedMins / 60;
            tempRoundedHours = Mathf.FloorToInt(tempHours);
            tempRoundedMins -= tempRoundedHours * 60;
        }
        
        _timePerSecond = new Vector3(tempSeconds, tempRoundedMins, tempRoundedHours);
    }

    #endregion

    private void Update()
    {
        rotation.x = degreePerSecond * Time.deltaTime;
        sun.transform.Rotate(rotation,Space.World);
        _timer += Time.deltaTime;
        WorldClockUpdater += Time.deltaTime;
        if (WorldClockUpdater >= 1)
        {
            worldTimeSeconds += _timePerSecond.x;
            if (worldTimeSeconds >= 60)
            {
                worldTimeSeconds -= 60;
                worldTimeMinutes++;
            }
            worldTimeMinutes += _timePerSecond.y;
            if (worldTimeMinutes >= 60)
            {
                worldTimeMinutes -= 60;
                worldTimeHours++;
            }
            worldTimeHours += _timePerSecond.z;
            if (worldTimeHours >= 24)
            {
                worldTimeHours -= 24;
                worldTimeDays++;
            }
            WorldClockUpdater -= 1;
        }
        if (_timer >= _totalSecondsInADay)
        {
            _timer = 0f;
        }
    }

    public void SetTime(Vector3 time)
    {
        //Dont Use this fuction, its dumb and only changes the time on the clock not the position of the sun
        worldTimeMinutes = Mathf.RoundToInt(time.x);
        worldTimeHours = Mathf.RoundToInt(time.y);
        worldTimeDays = Mathf.RoundToInt(time.z);
    }

    public Vector3 ReturnTime()
    {
        return new Vector3(worldTimeMinutes, worldTimeHours, worldTimeDays);
    }
}
