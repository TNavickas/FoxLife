using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed2 : MonoBehaviour
{
    public GameObject player;

    private void OnMouseDown()
    {
        var fox = player.GetComponent<FoxPlayer>();
        fox.citem();
        fox.incrementHearts();
        fox.incrementHunger();
        fox.incrementThrist();
        Debug.Log(fox.getTimeDifference());
    }
}
