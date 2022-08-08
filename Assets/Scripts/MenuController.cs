using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    private Animator anim;
    private bool Pressed;
    [SerializeField] private Animator fade;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject options;

    private void Start()
    {
        AudioManager.Instance.PlayMainTheme();
        anim = GetComponent<Animator>();
        Pressed = false;
    }

    public void GetAnimation(string state)
    {
        if (!Pressed)
        {
            anim.Play(state);
        }
    }

    public void PressButton(string state)
    {
        if (!Pressed)
        {
            Pressed = true;
            anim.Play(state);
            switch (state)
            {
                case "Menu1P"://Play
                    AudioManager.Instance.PlayMenuSelection();
                    StartCoroutine(Transition());
                    break;
                case "Menu2P"://Options
                    StartCoroutine(Options());
                    AudioManager.Instance.PlayMenuSelection();
                    break;
                case "Menu3P"://Credits
                    StartCoroutine(Credits());
                    AudioManager.Instance.PlayMenuSelection();
                    break;
                case "Menu4P"://Exit
                    AudioManager.Instance.PlayMenuSelection();
                    StartCoroutine(ExitGame());
                    break;
            }
        }
    }

    IEnumerator Options()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play("Fade");
        yield return new WaitForSeconds(0.5f);
        options.SetActive(true);
        Pressed = false;
        gameObject.SetActive(false);
    }

    IEnumerator Credits()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play("Fade");
        yield return new WaitForSeconds(0.5f);
        credits.SetActive(true);
        Pressed = false;
        gameObject.SetActive(false);
    }

    IEnumerator Transition()
    {
        fade.Play("Close");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    IEnumerator ExitGame()
    {
        fade.Play("Close");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
