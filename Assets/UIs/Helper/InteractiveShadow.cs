using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Mathf;

namespace LeTai.TrueShadow
{

public class InteractiveShadow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler,
                                 IDeselectHandler, IPointerDownHandler, IPointerUpHandler
{
    public float smoothTime = .05f;

    [Tooltip("Deselect on pointer up")]
    public bool autoDeselect;

    [Header("Size")]
    public float selectedSize = 28;
    public float hoverSize   = 28;
    public float clickedSize = 24;

    [Header("Distance")]
    public float selectedDistance = 12;
    public float hoverDistance   = 12;
    public float clickedDistance = 8;

    [Header("Color")]
    public Color selectedColor = new Color(0, 0, 0, .25f);
    public Color hoverColor   = new Color(0, 0, 0, .20f);
    public Color clickedColor = new Color(0, 0, 0, .25f);

    float normalSize;
    float normalDistance;
    Color normalColor;
    bool  normalStateAcquired;

    bool isSelected;
    bool isHovered;
    bool isClicked;

    Selectable selectable;

    float targetSize;
    float targetDistance;
    Color targetColor;

    static readonly Color FADED_COLOR = new Color(.5f, .5f, .5f, .5f);

#if UNITY_EDITOR
    void Reset()
    {

            normalStateAcquired = true;

            // Clicked UI remain selected, which is unwanted. Selected state is probably most useful on console
            // and keyboard nav, the later is rather hard to detect
            bool selectedIsNormal = Input.mousePresent || Input.touchSupported;
            autoDeselect = selectedIsNormal;

            hoverSize    = Round(Min(normalSize * 1.75f, normalSize + 20f));
            selectedSize = selectedIsNormal ? normalSize : hoverSize;
            clickedSize  = Round(Min(normalSize * 1.25f, normalSize + 15f));

            hoverDistance    = Round(Min(normalDistance * 1.5f, normalDistance + 20f));
            selectedDistance = selectedIsNormal ? normalDistance : hoverDistance;
            clickedDistance  = Round(Min(normalDistance * 1.25f, normalDistance + 15f));


            hoverColor    = Color.Lerp(normalColor, FADED_COLOR, .15f);
            selectedColor = selectedIsNormal ? normalColor : hoverColor;
            clickedColor  = Color.Lerp(normalColor, FADED_COLOR, .25f);

    }
#endif

    readonly List<RaycastResult> raycastResults = new List<RaycastResult>();

    void OnEnable()
    {
        selectable = GetComponent<Selectable>();

        isHovered = false;
        if (Input.mousePresent)
            isHovered = IsOverGameObject(Input.mousePosition);

        if (!isHovered)
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                isHovered = IsOverGameObject(Input.GetTouch(i).position);
                if (isHovered) break;
            }
        }

        isSelected = !autoDeselect && EventSystem.current.currentSelectedGameObject == gameObject;
        isClicked  = false;


    }


    void OnStateChange()
    {
        if (isClicked)
        {
            targetSize     = clickedSize;
            targetDistance = clickedDistance;
            targetColor    = clickedColor;
        }
        else if (isSelected)
        {
            targetSize     = selectedSize;
            targetDistance = selectedDistance;
            targetColor    = selectedColor;
        }
        else if (isHovered)
        {
            targetSize     = hoverSize;
            targetDistance = hoverDistance;
            targetColor    = hoverColor;
        }
        else
        {
            targetSize     = normalSize;
            targetDistance = normalDistance;
            targetColor    = normalColor;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        OnStateChange();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        OnStateChange();
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        OnStateChange();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        OnStateChange();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
        OnStateChange();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (autoDeselect && EventSystem.current.currentSelectedGameObject == gameObject)
            EventSystem.current.SetSelectedGameObject(null);

        isClicked = false;
        OnStateChange();
    }

    bool IsOverGameObject(Vector2 position)
    {
        var pointerData = new PointerEventData(EventSystem.current) { position = position };

        EventSystem.current.RaycastAll(pointerData, raycastResults);

        for (var i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject == gameObject)
                return true;
        }

        return false;
    }
}
}
