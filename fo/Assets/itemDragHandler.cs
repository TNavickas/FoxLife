// From https://www.youtube.com/watch?v=Pc8K_DVPgVM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemDragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform test;
    public GameObject test2;
    float pressStart;
    bool held = false;
    bool ignore = false;
    Vector2 dragStartPos;
    GameObject draggedItem = null;
    Vector2 draggedItemPos;
    Color curCol;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("P Down");
        // Store the starting touch position
        dragStartPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("P Up");

    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
        pressStart = Time.time;
        ignore = false;
        held = false;
        draggedItem = null;
        // Store the starting touch position
        //dragStartPos = eventData.position;
        test2.GetComponent<ScrollRect>().OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (held == true)
        {
            Debug.Log("Held");
            draggedItemPos = eventData.position;
            //draggedItem.GetComponent<RectTransform>().position = eventData.position;
        }
        else if (!ignore && Time.time - pressStart > 0.25)
        {
            if (Vector2.Distance(dragStartPos, eventData.position) < 20)
            //if (eventData.delta.magnitude < 5)
            {
                Debug.Log("Start Hold");
                held = true;
                test2.GetComponent<ScrollRect>().OnEndDrag(eventData);
                // Spawn draggable version
                draggedItem = new GameObject();
                Image itemImage = draggedItem.AddComponent<Image>();
                itemImage.color = Color.green;
                draggedItem.GetComponent<RectTransform>().SetParent(test);
                draggedItemPos = eventData.position;
                draggedItem.GetComponent<RectTransform>().position = eventData.position;
                draggedItem.SetActive(true);
                // Change slot visual
                curCol = gameObject.GetComponent<Image>().color;
                gameObject.GetComponent<Image>().color = new Color(curCol.r, curCol.g, curCol.b, 0.25f);
            }
            else
            {
                Debug.Log("Not Held");
                ignore = true;
                test2.GetComponent<ScrollRect>().OnDrag(eventData);
            }
        }
        else
        {
            test2.GetComponent<ScrollRect>().OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (held == true)
        {
            gameObject.GetComponent<Image>().color = curCol;
            if (draggedItem != null)
            {
                Destroy(draggedItem);
                draggedItem = null;
            }
            // Check if it was placed in the play area and change the avatar's state, if so
        }
        else if (held == false)
        {
            test2.GetComponent<ScrollRect>().OnEndDrag(eventData);
        }
        held = false;
        Debug.Log("Drag End");
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }
    
    void FixedUpdate()
    {
        if (draggedItem != null)
        {
            draggedItem.GetComponent<RectTransform>().position = Vector2.Lerp(
                draggedItem.GetComponent<RectTransform>().position, draggedItemPos, 0.68f);
        }
    }
}
