using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NaughtyAttributes;
public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected enum HoverCases
    {
        TITLE, NONE
    }
    [SerializeField] private HoverCases hoverCase;

    [SerializeField, ShowIf("hoverCase", HoverCases.TITLE), TextArea] private string additionalInformation; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverCase == HoverCases.TITLE)
        {
            FindFirstObjectByType<TitleBehavior>().ShowExtraInformation(additionalInformation);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCase == HoverCases.TITLE)
        {
            FindFirstObjectByType<TitleBehavior>().HideExtraInformation();
        }
    }
}
