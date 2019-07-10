using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public int SceneNumberToLoad;

    public void OnTorontoClicked() {
        SelectedCityInfo.CityName = "Toronto";
        SelectedCityInfo.CityCode = "/ON/s0000458_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnOttawaClicked() {
        SelectedCityInfo.CityName = "Ottawa";
        SelectedCityInfo.CityCode = "/ON/s0000430_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnMontrealClicked() {
        SelectedCityInfo.CityName = "Montreal";
        SelectedCityInfo.CityCode = "/QC/s0000635_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnQuebecClicked() {
        SelectedCityInfo.CityName = "Quebec";
        SelectedCityInfo.CityCode = "/QC/s0000620_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnStJohnClicked() {
        SelectedCityInfo.CityName = "St. John's";
        SelectedCityInfo.CityCode = "/NL/s0000280_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnFrederictonClicked() {
        SelectedCityInfo.CityName = "Fredericton";
        SelectedCityInfo.CityCode = "/NB/s0000250_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnWinnipegClicked() {
        SelectedCityInfo.CityName = "Winnipeg";
        SelectedCityInfo.CityCode = "/MB/s0000193_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnReginaClicked() {
        SelectedCityInfo.CityName = "Regina";
        SelectedCityInfo.CityCode = "/SK/s0000788_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnEdmontonClicked() {
        SelectedCityInfo.CityName = "Edmonton";
        SelectedCityInfo.CityCode = "/AB/s0000045_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnVictoriaClicked() {
        SelectedCityInfo.CityName = "Victoria";
        SelectedCityInfo.CityCode = "/BC/s0000775_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnHalifaxClicked() {
        SelectedCityInfo.CityName = "Halifax";
        SelectedCityInfo.CityCode = "/NS/s0000318_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }

    public void OnCharlottetownClicked() {
        SelectedCityInfo.CityName = "Charlottetown";
        SelectedCityInfo.CityCode = "/PE/s0000583_e.xml";
        SceneManager.LoadScene(SceneNumberToLoad);
    }
}
