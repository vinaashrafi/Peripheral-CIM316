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

    public float degreePerSecond;
    public Vector3 rotation;
    public GameObject sun;
    public int worldTimeMinutes;
    public int worldTimeHours = 7;
    public int worldTimeDays;
    private float _timer;
    
    
    private void Update()
    {
        rotation.x = degreePerSecond * Time.deltaTime;
        sun.transform.Rotate(rotation,Space.World);
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            
            worldTimeMinutes += 6;
            if (worldTimeMinutes >= 60)
            {
                worldTimeMinutes -= 60;
                worldTimeHours += 1;
                if (worldTimeHours >= 24)
                {
                    worldTimeHours -= 24;
                    worldTimeDays += 1;
                }
            }
            _timer -= 1;
        }
    }

    public void SetTime(Vector3 time)
    {
        worldTimeMinutes = Mathf.RoundToInt(time.x);
        worldTimeHours = Mathf.RoundToInt(time.y);
        worldTimeDays = Mathf.RoundToInt(time.z);
    }

    public Vector3 ReturnTime()
    {
        return new Vector3(worldTimeMinutes, worldTimeHours, worldTimeDays);
    }
}
