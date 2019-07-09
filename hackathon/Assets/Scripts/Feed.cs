using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{
    public GameObject player;

    private void OnMouseDown()
    {
        var fox = player.GetComponent<FoxPlayer>();
        fox.currentTime();
        Debug.Log(fox.getTimeDifference());
    }
}
