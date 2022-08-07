using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CustomUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Core")]
    public int buttonIndex;

    [Header("Hierarchy References")]
    [SerializeField] GameObject informationPanel;
    [SerializeField] Image img;
    [SerializeField] UIManager uiManager;

    [Header("Colors")]
    [SerializeField] Color hoverColor;
    [SerializeField] Color clickColor;
    [SerializeField] Color defaultColor;

    private bool isHovering;
    public void OnPointerDown(PointerEventData eventData)
    {
        img.color = clickColor;

        //process
        uiManager.onButtonClicked?.Invoke(buttonIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        informationPanel.SetActive(true);
        img.color = hoverColor;
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        informationPanel.SetActive(false);
        img.color = defaultColor;
        isHovering = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Color color = isHovering ? hoverColor : defaultColor;
        img.color = color;
    }
}
