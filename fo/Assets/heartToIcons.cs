using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartToIcons : MonoBehaviour
{
    public List<GameObject> happinessHearts;
    public List<GameObject> hungerHearts;
    public List<GameObject> thirstHearts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHearts(int health, int hunger, int thirst)
    {
        int remHH = Mathf.CeilToInt(happinessHearts.Count * (health / 10.0f));
        int remHuH = Mathf.CeilToInt(hungerHearts.Count * (hunger / 10.0f));
        int remTH = Mathf.CeilToInt(thirstHearts.Count * (thirst / 10.0f));
        foreach (GameObject heart in happinessHearts)
        {
            if (remHH > 0)
            {
                heart.SetActive(true);
                remHH -= 1;
            }
            else
            {
                heart.SetActive(false);
            }
        }
        foreach (GameObject heart in hungerHearts)
        {
            if (remHuH > 0)
            {
                heart.SetActive(true);
                remHuH -= 1;
            }
            else
            {
                heart.SetActive(false);
            }
        }
        foreach (GameObject heart in thirstHearts)
        {
            if (remTH > 0)
            {
                heart.SetActive(true);
                remTH -= 1;
            }
            else
            {
                heart.SetActive(false);
            }
        }
    }
}
