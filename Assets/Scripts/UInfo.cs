using UnityEngine;
using UnityEngine.EventSystems;

public class UInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string title;
    [SerializeField] private string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.GetInfo(title, description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisableInfo();
    }
}
