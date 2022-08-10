using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPreviewController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler, IDragHandler
{
    [SerializeField] private ShopSystem shopSystem;

    [SerializeField] private Transform itemPreviewContainer;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float progressionSpeed;
    private bool isHoveringOver;

    private void Awake()
    {
        shopSystem = GetComponentInParent<ShopSystem>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringOver = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isHoveringOver)
            return;

        if (eventData.dragging)
        {
            shopSystem.CurrentSelectedItemGameobject.transform.Rotate(new Vector3(0, -eventData.delta.x, 0));
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (!isHoveringOver)
            return;

        if (eventData.IsScrolling())
        {
            if(eventData.scrollDelta.y > 0)
            {
                if(itemPreviewContainer.localPosition.z < maxDistance)
                {
                    itemPreviewContainer.localPosition += (transform.forward * progressionSpeed);
                    Debug.Log("Moving forward");
                }
                else
                {
                    itemPreviewContainer.localPosition = new Vector3(0, 0, maxDistance);
                }
            }
            else if(eventData.scrollDelta.y < 0)
            {
                if(itemPreviewContainer.localPosition.z > minDistance)
                {
                    itemPreviewContainer.localPosition -= (transform.forward * progressionSpeed);
                    Debug.Log("Moving backward");
                }
                else
                {
                    itemPreviewContainer.localPosition = new Vector3(0, 0, minDistance);
                }
            }
        }
    }
}
