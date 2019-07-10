using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrapper : MonoBehaviour
{
    public avatarModel avatar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callFoxFunc(GameObject item, string itemName)
    {
        avatar.useItem(item, itemName);
    }
}
