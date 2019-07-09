using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

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
    }

    public void OnOttawaSelected() {
        WeatherData weatherData = GetWeatherData("Ottawa");
        SetText(weatherData);
    }

    public void OnMontrealSelected() {
        WeatherData weatherData = GetWeatherData("Montreal");
        SetText(weatherData);
    }

    private void SetText(WeatherData weatherData) {
        StationText.text = weatherData.Station;
        TimeText.text = weatherData.Time;
        ConditionText.text = weatherData.Condition;
        TemperatureText.text = weatherData.Temperature.ToString();
        HumidexText.text = weatherData.Humidex.ToString();
        WindSpeedText.text = weatherData.WindSpeed.ToString();
    }

    private WeatherData GetWeatherData(string cityName) {

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
        XmlNode childNode = node.SelectSingleNode("station");
        return childNode.InnerText;
    }

    private string GetDateData(XmlNode node) {
        XmlNodeList childNodes = node.SelectNodes("dateTime");

        string textSummary = "";
        foreach (XmlNode dateTimeNode in childNodes) {
            string zone = dateTimeNode.Attributes["zone"]?.Value;
            if(zone == "EDT") {
                XmlNode summaryNode = dateTimeNode.SelectSingleNode("textSummary");
                textSummary = summaryNode.InnerText;
            }
        }
        return textSummary;
    }

    private string GetConditionData(XmlNode node) {
        XmlNode childNode = node.SelectSingleNode("condition");
        return childNode.InnerText;
    }

    private float GetTemperatureData(XmlNode node) {
        XmlNode childNode = node.SelectSingleNode("temperature");
        string temperatureStr = childNode.InnerText;
        return float.Parse(temperatureStr);
    }

    private int GetHumidexData(XmlNode node) {
        XmlNode childNode = node.SelectSingleNode("humidex");
        string humidexStr = childNode.InnerText;
        return int.Parse(humidexStr);
    }

    private float GetWindData(XmlNode node) {
        XmlNode windNode = node.SelectSingleNode("wind");
        XmlNode speedNode = windNode.SelectSingleNode("speed");
        string speedStr = speedNode.InnerText;
        return float.Parse(speedStr);
    }
}
