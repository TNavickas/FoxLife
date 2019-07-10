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

    public GameObject weather;
    private WeatherData weatherData;
    private string weatherCond;
    private float weatherTemp;
    private float weatherWind;
    private string _item;

    // Start is called before the first frame update
    void Start()
    {
        updatePlayer();
        progressGame();
    }

    private void updatePlayer()
    {
        
        if (!PlayerPrefs.HasKey("_hearts"))
        {
            _hearts = Constants.MAXHEARTS;
            PlayerPrefs.SetInt("_hearts", _hearts);
        }
        else
        {
            _hearts = PlayerPrefs.GetInt("_hearts");
        }

        if (!PlayerPrefs.HasKey("_hunger"))
        {
            _hunger = Constants.MAXHUNGER;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }
        else
        {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if (!PlayerPrefs.HasKey("_thirst"))
        {
            _thirst = Constants.MAXTHIRST;
            PlayerPrefs.SetInt("_thirst", _thirst);
        }
        else
        {
            _thirst = PlayerPrefs.GetInt("_thirst");
        }

        if (!PlayerPrefs.HasKey("_exittime"))
        {
            PlayerPrefs.SetString("_exittime", currentTime());
        }

        InvokeRepeating("updateWeather", 0f, Constants.HOUR);
        InvokeRepeating("setExitTime", 0f, 30f);
        
    }

    public void checkConditions()
    {
        CancelInvoke("decrementHearts");
        CancelInvoke("decrementHunger");
        CancelInvoke("decrementThirst");
        
        Debug.Log(item);
        // Sunny and high temperatures
        if (weatherCond.Contains("Sunny") && weatherTemp >= 22.0)
        {
            Debug.Log("Burning Up");
            InvokeRepeating("decrementThirst", 5f, 5f);
            InvokeRepeating("decrementHunger", 10f, 10f);
            InvokeRepeating("decrementHearts", 10f, 10f);
        }
        // Raining
        else if (weatherCond.Contains("Rain") && item != "Umbrella") {
            Debug.Log("Rain Poisoning");
            InvokeRepeating("decrementHearts", 3f, 3f);
            InvokeRepeating("decrementHunger", 10f, 10f);
            InvokeRepeating("decrementThirst", 10f, 10f);
        }
        // Windy & Cold
        else if (weatherWind >= 25.0 && weatherTemp <= 10)
        {
            Debug.Log("cw");
            if (item != "CoatEar")
            {
                Debug.Log("dying");
                InvokeRepeating("decrementHearts", 3f, 3f);
                InvokeRepeating("decrementHunger", 3f, 3f);
                InvokeRepeating("decrementThirst", 10f, 10f);
            }
            else
            {
                InvokeRepeating("decrementHearts", 10f, 10f);
                InvokeRepeating("decrementHunger", 3f, 3f);
                InvokeRepeating("decrementThirst", 10f, 10f);
            }
        }

        // Snow

        else if (weatherCond.Contains("Snow") && item != "Hat")
        {
            Debug.Log("Hypothermia");
            InvokeRepeating("decrementHearts", 3f, 3f);
            InvokeRepeating("decrementHunger", 10f, 10f);
            InvokeRepeating("decrementThirst", 10f, 10f);
        }

        else
        {
            Debug.Log("no");
            InvokeRepeating("incrementHearts", 10f, 10f);
            InvokeRepeating("decrementThirst", 10f, 10f);
            InvokeRepeating("decrementHunger", 10f, 10f);
        }
        
    }

    public void change()
    {
        weatherCond = "ITS Snow with rain and flurries";
        weatherTemp = 15.0f;
        weatherWind = 25.0f;
        checkConditions();
    }

    public void citem()
    {
        item = "Hat";
        checkConditions();
    }

    private void decrementHearts()
    {
        _hearts -= 1;
        if (_hearts < 0) { _hearts = 0; }
    }

    private void decrementHunger()
    {
        _hunger -= 1;
        if (_hunger < 0) { _hunger = 0; }
    }

    private void decrementThirst()
    {
        _thirst -= 1;
        if (_thirst < 0) { _thirst = 0; }
    }

    public void incrementHearts()
    {
        if (hearts < Constants.MAXHEARTS) { hearts += 1; }
    }

    public void incrementHunger()
    {
        if (hunger < Constants.MAXHUNGER) { hunger += 1;}
    }

    public void incrementThrist()
    {
        if (thirst < Constants.MAXTHIRST) { thirst += 1;}
    }

    private void updateWeather()
    {
        weatherData = weather.GetComponent<WeatherDataController>().GetWeatherData(PlayerPrefs.GetString("_city"));
        weatherCond = weatherData.Condition;
        weatherTemp = float.Parse(weatherData.Temperature);
        weatherWind = float.Parse(weatherData.WindSpeed);
        Debug.Log(weatherData.Condition);
        checkConditions();
    }

    private void progressGame()
    {
        TimeSpan timeElapsed = getTimeDifference();
        _hearts -= (int)(timeElapsed.TotalHours);
        _hunger -= (int)(timeElapsed.TotalHours);
        _thirst -= (int)(timeElapsed.TotalHours);
        if (_hearts < 0){_hearts = 0;}
        if (_hunger < 0){_hunger = 0;}
        if (_thirst < 0){_thirst = 0;}
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

    public string item
    {
        get { return _item; }
        set { _item = value; }
    }

    public string currentTime()
    {
        DateTime current = DateTime.Now;
        return current.ToString();
    }

    void setExitTime()
    {
        PlayerPrefs.SetString("_exittime", currentTime());
    }

    public TimeSpan getTimeDifference()
    {
        return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("_exittime"));
    }

    public string exitTime()
    {
        return PlayerPrefs.GetString("_exittime");
    }
    public string city()
    {
        return weatherCond + weatherTemp + weatherWind;
    }

}
