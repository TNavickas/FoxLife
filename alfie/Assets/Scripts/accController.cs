using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accController : foxController
{
    private Animator accAnimator;
    // Start is called before the first frame update
    void Start()
    {
        accAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /* Every update, check the settings of the fox and update the parameters in the animator*/
        accAnimator.SetBool("isFood", check_if_food());
        accAnimator.SetBool("isClothed", check_if_clothed());
        accAnimator.SetBool("isUmbrella", check_if_umbrella());
        accAnimator.SetBool("isWater", check_if_water());
    }

}
