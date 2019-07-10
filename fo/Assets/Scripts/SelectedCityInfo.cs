using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectedCityInfo {
    private static string cityName = "";
    private static string cityCode = "";

    public static string CityName {
        get {
            return cityName;
        }
        set {
            cityName = value;
            PlayerPrefs.SetString("_cityName", value);
            PlayerPrefs.Save();
        }
    }
    public static string CityCode {
        get {
            return cityCode;
        }
        set {
            cityCode = value;
            PlayerPrefs.SetString("_cityCode", value);
            PlayerPrefs.Save();
        }
    }
}
