using UnityEngine;
using UnityEngine.UI;

public class HouseController : MonoBehaviour
{
    private int totalpop;

    [SerializeField] private Text popText;
    [SerializeField] private GameObject canvas;

    void Start()
    {
        totalpop = 2;
        popText.text = "+" + totalpop;
    }

    public void GetResources()
    {
        GameManager.Instance.IncreasePopulation(totalpop);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            totalpop += 1;
            popText.text = "+" + totalpop;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            totalpop -= 1;
            popText.text = "+" + totalpop;
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
