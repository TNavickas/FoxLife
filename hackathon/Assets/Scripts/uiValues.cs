using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiValues : MonoBehaviour
{

    public GameObject hearts;
    public GameObject hunger;
    public GameObject thirst;
    public GameObject fox;

    // Update is called once per frame
    void Update()
    {
        hearts.GetComponent<Text>().text = "" + fox.GetComponent<FoxPlayer>().hearts;
        hunger.GetComponent<Text>().text = "" + fox.GetComponent<FoxPlayer>().hunger;
        thirst.GetComponent<Text>().text = "" + fox.GetComponent<FoxPlayer>().thirst;
    }
}
