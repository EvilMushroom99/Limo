using UnityEngine;
using UnityEngine.UI;

public class SawmillController : MonoBehaviour
{
    private int wood;
    private int totalwood;

    [SerializeField] private Text woodText;
    [SerializeField] private GameObject canvas;

    void Start()
    {
        wood = 0;
        totalwood = 1;
        woodText.text = "+" + wood;
    }

    public void GetResources()
    {
        GameManager.Instance.IncreaseWood(totalwood + wood);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            wood += 1;
            woodText.text = "+" + wood;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            wood -= 1;
            woodText.text = "+" + wood;
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.Instance.warTime)
        {
            canvas.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.Instance.warTime)
        {
            canvas.SetActive(false);
        }
    }
}
