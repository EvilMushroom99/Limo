using UnityEngine;
using UnityEngine.UI;

public class MusicButtons : MonoBehaviour
{
    [SerializeField] private Sprite onImage;
    [SerializeField] private Sprite offImage;
    [SerializeField] private bool isMusic;
    private bool isOn = true;
    

    public void ChangeState()
    {
        AudioManager.Instance.PlayMenuSelection();
        if (isOn)
        {
            isOn = false;
            GetComponent<Image>().sprite = offImage;
            if (isMusic)
            {
                AudioManager.Instance.MusicOff();
            }
            else
            {
                AudioManager.Instance.SoundsOff();
            }
        }
        else
        {
            isOn = true;
            GetComponent<Image>().sprite = onImage;

            if (isMusic)
            {
                AudioManager.Instance.MusicOff();
            }
            else
            {
                AudioManager.Instance.SoundsOff();
            }
        }
    }
}
