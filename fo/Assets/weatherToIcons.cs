using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherToIcons : MonoBehaviour
{
    public GameObject rain;
    public GameObject snow;
    public GameObject sun;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void displayIcons(string curWeather)
    {
        rain.SetActive(false);
        snow.SetActive(false);
        sun.SetActive(false);
        if (curWeather == "Rain")
        {
            rain.SetActive(true);
        }
        else if (curWeather == "Snow")
        {
            snow.SetActive(true);
        }
        else if (curWeather == "Hot")
        {
            sun.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
