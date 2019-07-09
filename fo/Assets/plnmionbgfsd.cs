using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plnmionbgfsd : MonoBehaviour
{
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public Camera conCam;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        conCam.backgroundColor = color1;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        conCam.backgroundColor = Color.Lerp(color1, color2, t);
    }
}
