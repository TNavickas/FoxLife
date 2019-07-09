using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FoxPlayer : MonoBehaviour
{
    [SerializeField]
    private int _hearts;
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _thirst;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("exittime", "07/09/2019 12:12:00");
        InitPlayer();
    }

    private void InitPlayer()
    {
        if (!PlayerPrefs.HasKey("_hearts"))
        {
            _hearts = 10;
            PlayerPrefs.SetInt("_hearts", _hearts);
        }
        else
        {
            _hearts = PlayerPrefs.GetInt("_health");
        }

        if (!PlayerPrefs.HasKey("_hunger"))
        {
            _hunger = 10;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }
        else
        {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if (!PlayerPrefs.HasKey("_thirst"))
        {
            _thirst = 10;
            PlayerPrefs.SetInt("_thirst", _thirst);
        }
        else
        {
            _thirst = PlayerPrefs.GetInt("_thirst");
        }

        if (!PlayerPrefs.HasKey("exittime"))
        {
            PlayerPrefs.SetString("exittime", currentTime());
        }

        TimeSpan timeElapsed = getTimeDifference();
        Debug.Log(timeElapsed.TotalHours);
        _hearts -= (int) (timeElapsed.TotalHours);
        _hunger -= (int) (timeElapsed.TotalHours);
        _thirst -= (int) (timeElapsed.TotalHours);
        if (_hearts < 0) {
            _hearts = 0;
        }
        if (_hunger < 0)
        {
            _hunger = 0;
        }
        if (_thirst < 0)
        {
            _thirst = 0;
        }

        InvokeRepeating("setExitTime", 0f, 30f);

    }

    public int hearts
    {
        get { return _hearts; }
        set { _hearts = value; }
    }

    public int hunger
    {
        get { return _hunger; }
        set { _hunger = value; }

    }

    public int thirst
    {
        get { return _thirst; }
        set { _thirst = value; }
    }

    public string currentTime()
    {
        DateTime current = DateTime.Now;
        return current.ToString();
    }

    void setExitTime()
    {
        PlayerPrefs.SetString("exittime", currentTime());
    }

    public TimeSpan getTimeDifference()
    {
        Debug.Log("exit:" + Convert.ToDateTime(PlayerPrefs.GetString("exittime")));
        return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("exittime"));
    }

}
