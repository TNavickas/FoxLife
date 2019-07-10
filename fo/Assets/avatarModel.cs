using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class avatarModel : MonoBehaviour
{
    [SerializeField]
    private int _hearts;
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _thirst;

    public weatherToIcons weatherIconController;
    public heartToIcons heartIconController;
    string curWeatherState;

    public GameObject weather;
    private WeatherData weatherData;
    private string weatherCond;
    private float weatherTemp;
    private string _item;

    // Start is called before the first frame update
    void Start()
    {
        updatePlayer();
        progressGame();
        heartIconController.updateHearts(8, 6, 1);
        weatherIconController.displayIcons("Rain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useItem(GameObject item, string itemName)
    {
        Debug.Log(item);
        Debug.Log(itemName);
        _item = itemName;
        //checkConditions();
    }

    private void updatePlayer()
    {

        if (!PlayerPrefs.HasKey("_hearts"))
        {
            _hearts = 10;
            PlayerPrefs.SetInt("_hearts", _hearts);
        }
        else
        {
            _hearts = PlayerPrefs.GetInt("_hearts");
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

        Debug.Log("Mainly Sunny".Contains("Sunny"));
        Debug.Log(item);
        // Sunny and high temperatures
        if (weatherCond.Contains("Sunny") && weatherTemp >= 25.0)
        {
            curWeatherState = "Hot";
            Debug.Log("Burning Up");
            InvokeRepeating("decrementThirst", 5f, 5f);
            InvokeRepeating("decrementHunger", 10f, 10f);
            InvokeRepeating("decrementHearts", 10f, 10f);
        }

        else if (weatherCond.Contains("Rain") && item != "Umbrella")
        {
            curWeatherState = "Rain";
            Debug.Log("Rain Poisoning");
            InvokeRepeating("decrementHearts", 3f, 3f);
            InvokeRepeating("decrementHunger", 10f, 10f);
            InvokeRepeating("decrementThirst", 10f, 10f);
        }

        else
        {
            curWeatherState = "";
            Debug.Log("no");
            InvokeRepeating("decrementHearts", 10f, 10f);
            InvokeRepeating("decrementThirst", 10f, 10f);
            InvokeRepeating("decrementHunger", 10f, 10f);
        }

        weatherIconController.displayIcons(curWeatherState);

    }

    private void decrementHearts()
    {
        _hearts -= 1;
        if (_hearts < 0) { _hearts = 0; }
        heartIconController.updateHearts(_hearts, _hunger, _thirst);
    }

    private void decrementHunger()
    {
        _hunger -= 1;
        if (_hunger < 0) { _hunger = 0; }
        heartIconController.updateHearts(_hearts, _hunger, _thirst);
    }

    private void decrementThirst()
    {
        _thirst -= 1;
        if (_thirst < 0) { _thirst = 0; }
        heartIconController.updateHearts(_hearts, _hunger, _thirst);
    }

    private void updateWeather()
    {
        weatherData = weather.GetComponent<WeatherDataController>().GetWeatherData(SelectedCityInfo.CityCode);
        weatherCond = weatherData.Condition;
        weatherTemp = float.Parse(weatherData.Temperature);
        Debug.Log(weatherData.Condition);
        checkConditions();
    }

    private void progressGame()
    {
        TimeSpan timeElapsed = getTimeDifference();
        _hearts -= (int)(timeElapsed.TotalHours);
        _hunger -= (int)(timeElapsed.TotalHours);
        _thirst -= (int)(timeElapsed.TotalHours);
        if (_hearts < 0) { _hearts = 0; }
        if (_hunger < 0) { _hunger = 0; }
        if (_thirst < 0) { _thirst = 0; }
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
        return weatherCond + weatherTemp;
    }
}
