using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    private Animator anim;
    private bool Pressed;
    [SerializeField] private Animator fade;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        anim.Play(state);
        Pressed = true;

        switch (state)
        {
            case "Menu1P":
                AudioManager.Instance.PlayMenuSelection();
                StartCoroutine(Transition());
                break;
            case "Menu2P":
                AudioManager.Instance.PlayMenuSelection();
                break;
            case "Menu3P":
                AudioManager.Instance.PlayMenuSelection();
                break;
            case "Menu4P":
                AudioManager.Instance.PlayMenuSelection();
                break;
        }
    }

    IEnumerator Transition()
    {
        fade.Play("Close");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    public void Unpressed()
    {
        Pressed = true;
    }
}
