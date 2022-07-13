using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string state;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<MenuController>().GetAnimation(state);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<MenuController>().GetAnimation("Idle");
    }
}
