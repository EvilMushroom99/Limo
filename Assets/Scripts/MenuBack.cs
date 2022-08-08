using UnityEngine;
using System.Collections;

public class MenuBack : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Animator currentMenu;
    private bool pressed;

    public void Back()
    {
        if (!pressed)
        {
            pressed = true;
            StartCoroutine(BackTransition());
        }
    }

    IEnumerator BackTransition()
    {
        AudioManager.Instance.PlayMenuSelection();
        currentMenu.Play("Close");
        yield return new WaitForSeconds(0.5f);
        menu.SetActive(true);
        menu.GetComponent<Animator>().Play("Start");
        pressed = false;
        gameObject.SetActive(false);
    }
}
