using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button ContinueButton;

    void Start()
    {
        string cityName = PlayerPrefs.GetString("_cityName");
        string cityCode = PlayerPrefs.GetString("_cityCode");

        if (string.IsNullOrEmpty(cityName) || string.IsNullOrEmpty(cityCode)) {
            SelectedCityInfo.CityName = "";
            SelectedCityInfo.CityCode = "";
        }
        else {
            SelectedCityInfo.CityName = cityName;
            SelectedCityInfo.CityCode = cityCode;

            ContinueButton.enabled = true;
        }
    }

    public void OnPlayClicked() {
        SceneManager.LoadScene(1);
    }

    public void OnContinueClicked() {
        SceneManager.LoadScene(2);
    }
}
