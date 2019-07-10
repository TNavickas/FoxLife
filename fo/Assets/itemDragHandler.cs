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
    bool ignore = false;
    float scaleFactor;
    Vector2 dragStartPos;
    GameObject draggedItem = null;
    Vector2 draggedItemPos;
    float dragThres = 15.0f;
    Color curCol;
    float newRot = 0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("P Down");
        // Store the starting touch position
        dragStartPos = eventData.position;
        pressStart = Time.time;
        Debug.Log(test.GetComponent<Canvas>().scaleFactor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("P Up");

    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
        ignore = false;
        draggedItem = null;
        // Store the starting touch position
        //dragStartPos = eventData.position;
        test2.GetComponent<ScrollRect>().OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            draggedItemPos = eventData.position;
            //newRot = eventData.delta.x * 5 / scaleFactor;
            newRot = Mathf.Clamp(Mathf.LerpAngle(newRot, eventData.delta.x * 5 / scaleFactor, 0.5f), -50f, 50f);
            //draggedItem.GetComponent<RectTransform>().position = eventData.position;
        }
        else if (!ignore && Time.time - pressStart > 0.25)
        {
            if (Vector2.Distance(dragStartPos, eventData.position) < (dragThres * scaleFactor))
            //if (eventData.delta.magnitude < 5)
            {
                Debug.Log("Start Hold");
                test2.GetComponent<ScrollRect>().OnEndDrag(eventData);

                // Spawn draggable version
                draggedItem = new GameObject();
                draggedItem.name = gameObject.name;
                Image itemImage = draggedItem.AddComponent<Image>();
                itemImage.preserveAspect = true;
                itemImage.raycastTarget = false;
                itemImage.sprite = gameObject.GetComponent<Image>().sprite;
                draggedItem.GetComponent<RectTransform>().SetParent(test);
                draggedItemPos = eventData.position;
                draggedItem.GetComponent<RectTransform>().localScale *= scaleFactor;
                draggedItem.GetComponent<RectTransform>().position = eventData.position;
                draggedItem.SetActive(true);

                // Change slot visual
                curCol = gameObject.GetComponent<Image>().color;
                gameObject.GetComponent<Image>().color = new Color(curCol.r, curCol.g, curCol.b, 0.25f);
            }
            else
            {
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
        if (draggedItem != null)
        {
            gameObject.GetComponent<Image>().color = curCol;

            // Check if it was placed in the play area and change the avatar's state, if so
            //  Note: Would typically want to use IDropHandler for this type of problem
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            GameObject dragEndGameObj = eventData.pointerCurrentRaycast.gameObject;
            if (dragEndGameObj != null && dragEndGameObj.name == "avatar")
            {
                dragEndGameObj.GetComponent<wrapper>().callFoxFunc(draggedItem, draggedItem.name);
            }

            Destroy(draggedItem);
            draggedItem = null;
        }
        else
        {
            test2.GetComponent<ScrollRect>().OnEndDrag(eventData);
        }
        Debug.Log("Drag End");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        scaleFactor = test.GetComponent<Canvas>().scaleFactor;
    }
    
    void FixedUpdate()
    {
        if (draggedItem != null)
        {
            draggedItem.GetComponent<RectTransform>().position = Vector2.Lerp(
                draggedItem.GetComponent<RectTransform>().position, draggedItemPos, 0.33f);
            float newZ = Mathf.LerpAngle(draggedItem.GetComponent<RectTransform>().eulerAngles.z, newRot, 0.3f);
            draggedItem.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, newZ);
            newRot = Mathf.LerpAngle(newRot, 0, 0.1f);
        }
    }
}
