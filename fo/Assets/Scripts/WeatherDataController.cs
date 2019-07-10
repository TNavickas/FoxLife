using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeatherDataController : MonoBehaviour
{
    public List<string> CityNames;
    public List<string> CityCodes;

    public string URLStringPrefix = "https://dd.weather.gc.ca/citypage_weather/xml";

    public Text StationText;
    public Text TimeText;
    public Text ConditionText;
    public Text TemperatureText;
    public Text HumidexText;
    public Text WindSpeedText;

    public void OnTorontoSelected() {
        WeatherData weatherData = GetWeatherData("Toronto");
        SetText(weatherData);
        PlayerPrefs.SetString("_city", "Toronto");
        SceneManager.LoadScene("GameScene");
    }

    public void OnOttawaSelected() {
        WeatherData weatherData = GetWeatherData("Ottawa");
        SetText(weatherData);
        PlayerPrefs.SetString("_city", "Ottawa");
        SceneManager.LoadScene("GameScene");
    }

    public void OnMontrealSelected() {
        WeatherData weatherData = GetWeatherData("Montreal");
        SetText(weatherData);
        PlayerPrefs.SetString("_city", "Montreal");
        SceneManager.LoadScene("GameScene");
    }

    private void SetText(WeatherData weatherData) {
        StationText.text = weatherData.Station;
        TimeText.text = weatherData.Time;
        ConditionText.text = weatherData.Condition;
        TemperatureText.text = weatherData.Temperature.ToString();
        HumidexText.text = weatherData.Humidex.ToString();
        WindSpeedText.text = weatherData.WindSpeed.ToString();
    }

    public WeatherData GetWeatherData(string cityName) {

        int idx = CityNames.IndexOf(cityName);
        string cityCode = CityCodes[idx];

        string URLString = URLStringPrefix + cityCode;
        XmlDocument doc = new XmlDocument();
        doc.Load(URLString);

        XmlNode node = doc.DocumentElement.SelectSingleNode("currentConditions");

        WeatherData data = new WeatherData();
        data.Station = GetStationData(node);
        data.Time = GetDateData(node);
        data.Condition = GetConditionData(node);
        data.Temperature = GetTemperatureData(node);
        data.Humidex = GetHumidexData(node);
        data.WindSpeed = GetWindData(node);

        return data;
    }

    private string GetStationData(XmlNode node) {
        XmlNode childNode = node?.SelectSingleNode("station") ?? null;
        return childNode?.InnerText ?? "";
    }

    private string GetDateData(XmlNode node) {
        XmlNodeList childNodes = node?.SelectNodes("dateTime") ?? null;
        if (childNodes == null) return "";

        string textSummary = "";
        foreach (XmlNode dateTimeNode in childNodes) {
            string zone = dateTimeNode.Attributes["zone"]?.Value ?? "";
            if(zone == "EDT") {
                XmlNode summaryNode = dateTimeNode?.SelectSingleNode("textSummary") ?? null;
                textSummary = summaryNode?.InnerText ?? "";
            }
        }
        return textSummary;
    }

    private string GetConditionData(XmlNode node) {
        XmlNode childNode = node?.SelectSingleNode("condition") ?? null;
        return childNode?.InnerText ?? "";
    }

    private string GetTemperatureData(XmlNode node) {
        XmlNode childNode = node?.SelectSingleNode("temperature") ?? null;
        return childNode?.InnerText ?? "";
    }

    private string GetHumidexData(XmlNode node) {
        XmlNode childNode = node?.SelectSingleNode("humidex") ?? null;
        return childNode?.InnerText ?? "";
    }

    private string GetWindData(XmlNode node) {
        XmlNode windNode = node?.SelectSingleNode("wind") ?? null;
        XmlNode speedNode = windNode?.SelectSingleNode("speed") ?? null;
        return speedNode?.InnerText ?? "";
    }
}
