using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartScreen : MonoBehaviour
{

    // Update is called once per frame
    public void ChangeScene()
    {
        SceneManager.LoadScene("WeatherSelection");
    }
}
