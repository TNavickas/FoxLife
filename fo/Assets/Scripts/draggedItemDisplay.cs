using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class draggedItemDisplay : MonoBehaviour
{

    public Vector2 itemPos;
    public float itemRot = 0;
    public bool shouldDelete = false;
    public bool fadeOut = false;
    public float lerpSpeed = 0.33f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetComponent<RectTransform>().position = Vector2.Lerp(
            GetComponent<RectTransform>().position, itemPos, lerpSpeed);
        float newZ = Mathf.LerpAngle(GetComponent<RectTransform>().eulerAngles.z, itemRot, 0.3f);
        GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, newZ);
        itemRot = Mathf.LerpAngle(itemRot, 0, 0.1f);
        if (fadeOut)
        {
            Color curCol = GetComponent<Image>().color;
            float colAlpha = Mathf.Lerp(curCol.a, 0, 0.4f);
            GetComponent<Image>().color = new Color(curCol.r, curCol.g, curCol.b, colAlpha);
            Debug.Log(colAlpha);
            if (colAlpha < 0.05f)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        if (shouldDelete && Vector2.Distance((Vector2)(GetComponent<RectTransform>().position), itemPos) < 25)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        }
    }
}
