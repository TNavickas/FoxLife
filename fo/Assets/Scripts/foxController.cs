using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class foxController : MonoBehaviour
{
    private Animator foxAnimator;
    private avatarModel avatarInfo;
    // Start is called before the first frame update
    void Start()
    {
        foxAnimator = GetComponent<Animator>();
        Debug.Log("agassdadasdasdasd");
        avatarInfo = GetComponent<avatarModel>();
        Debug.Log(avatarInfo);
    }

    // Update is called once per frame
    void Update()
    {
        /* Every update, check the settings of the fox and update the parameters in the animator*/
        foxAnimator.SetBool("isFood", check_if_food());
        foxAnimator.SetBool("isHungry", check_if_hungry());
        foxAnimator.SetBool("isClothed", check_if_clothed());
        foxAnimator.SetBool("isContent", check_if_content());
        foxAnimator.SetBool("isCold", check_if_cold());
        foxAnimator.SetBool("isUmbrella", check_if_umbrella());
        foxAnimator.SetBool("isWater", check_if_water());
        foxAnimator.SetBool("isThirsty", check_if_thirsty());
        foxAnimator.SetInteger("Happiness", get_happiness());

    }
    //checks to see if food is on the screen, returns true if found
    public bool check_if_food()
    {
        Debug.Log(avatarInfo);
        string item = avatarInfo.item;
        return (item == "food" ? true : false);
    }
    //if just ate food return true
    public bool check_if_hungry()
    {
        return true;
    }

    //checks to see if thirsty: if just drank water return true
    public bool check_if_thirsty()
    {
        return false;
    }
    //checks to see if there is clothing on the screen, returns true if found
    public bool check_if_clothed()
    {
        return true;
    }

    //checks to see if the fox is content, returns true if so
    public bool check_if_content()
    {
        return true;
    }
    //checks to see if the fox is cold, returns true if so
    public bool check_if_cold()
    {
        return true;
    }

    public bool check_if_umbrella()
    {
        return false;
    }

    public bool check_if_water()
    {
        return false;
    }

    //returns the happiness of the fox
    public int get_happiness()
    {
        return 100;
    }


}
