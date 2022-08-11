using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    [SerializeField] GameObject[] SlimeJump;
    [SerializeField] GameObject[] SlimeDie;
    [SerializeField] GameObject[] SlimeHit;
    [SerializeField] GameObject[] CommonSlimeAttack;
    [SerializeField] GameObject[] ArcherSlimeAttack;
    [SerializeField] GameObject[] BombSlimeAttack;

    [SerializeField] GameObject[] ArcherTowerAttack;
    [SerializeField] GameObject[] FireTowerAttack;
    [SerializeField] GameObject[] SpikeTowerActivate;
    [SerializeField] GameObject[] VoidTowerAttack;
    [SerializeField] GameObject[] FireBallExplotion;
    [SerializeField] GameObject[] BlackHoleExplotion;
    [SerializeField] GameObject[] BlackHoleCharge;

    [SerializeField] AudioSource MenuSelection;
    [SerializeField] AudioSource MenuSelectionBuilding;
    [SerializeField] AudioSource MenuSelectionBuildingDeny;

    [SerializeField] AudioSource BuildingPlaceDeny;
    [SerializeField] AudioSource BuildingPlaced;
    [SerializeField] GameObject[] BuildingDestroyed;
    [SerializeField] GameObject[] BuildingHit;

    [SerializeField] AudioSource WarTime;
    [SerializeField] AudioSource WaveCompleted;

    //LoopSounds
    [SerializeField] AudioSource MainMenu;
    [SerializeField] AudioSource WarMusic;
    [SerializeField] AudioSource PeacefulMusic;

    //SoundOn
    [SerializeField] private bool music = true;
    [SerializeField] private bool sounds = true;

    public bool IsMusicOn()
    {
        return music;
    }

    public bool IsSoundsOn()
    {
        return sounds;
    }

    public void MusicOff()
    {
        if (music)
        {
            music = false;
            MainMenu.Stop();
            PeacefulMusic.Stop();
            WarMusic.Stop();
        }
        else
        {
            music = true;
            if (SceneManager.GetActiveScene().name == "Gameplay")
            {
                if (GameManager.Instance.warTime)
                {
                    PlayWarMusic();
                }
                else
                {
                    PlayGameplayTheme();
                }
            }
            else
            {
                MainMenu.Play();
            }
        }
    }

    public void SoundsOff()
    {
        if (sounds)
        {
            sounds = false;
        }
        else
        {
            sounds = true;
        }
    }

    public void PlayMainTheme()
    {
        if (music)
        {
            PeacefulMusic.Stop();
            WarMusic.Stop();
            MainMenu.Play();
        }
    }

    public void PlayGameplayTheme()
    {
        if (music)
        {
            MainMenu.Stop();
            WarMusic.Stop();
            PeacefulMusic.Play();
        }
    }

    public void PlayWarMusic()
    {
        if (music)
        {
            PeacefulMusic.Stop();
            WarMusic.Play();
        }
    }

    //Pool
    public void PlaySlimeJump()
    {
        if (sounds)
        {
            for (int i = 0; i < SlimeJump.Length; i++)
            {
                if (!SlimeJump[i].activeInHierarchy)
                {
                    SlimeJump[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlaySlimeDie()
    {
        if (sounds)
        {
            for (int i = 0; i < SlimeDie.Length; i++)
            {
                if (!SlimeDie[i].activeInHierarchy)
                {
                    SlimeDie[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlaySlimeHit()
    {
        if (sounds)
        {
            for (int i = 0; i < SlimeHit.Length; i++)
            {
                if (!SlimeHit[i].activeInHierarchy)
                {
                    SlimeHit[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayCommonSlimeAttack()
    {
        if (sounds)
        {
            for (int i = 0; i < CommonSlimeAttack.Length; i++)
            {
                if (!CommonSlimeAttack[i].activeInHierarchy)
                {
                    CommonSlimeAttack[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayArcherSlimeAttack()
    {
        if (sounds)
        {
            for (int i = 0; i < ArcherSlimeAttack.Length; i++)
            {
                if (!ArcherSlimeAttack[i].activeInHierarchy)
                {
                    ArcherSlimeAttack[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayBombSlimeAttack()
    {
        if (sounds)
        {
            for (int i = 0; i < BombSlimeAttack.Length; i++)
            {
                if (!BombSlimeAttack[i].activeInHierarchy)
                {
                    BombSlimeAttack[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayArcherTowerAttack()
    {
        if (sounds)
        {
            for (int i = 0; i < ArcherTowerAttack.Length; i++)
            {
                if (!ArcherTowerAttack[i].activeInHierarchy)
                {
                    ArcherTowerAttack[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayFireTowerAttack()
    {
        if (sounds)
        {
            for (int i = 0; i < FireTowerAttack.Length; i++)
            {
                if (!FireTowerAttack[i].activeInHierarchy)
                {
                    FireTowerAttack[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayVoidTowerAttack()
    {
        if (sounds)
        {
            for (int i = 0; i < VoidTowerAttack.Length; i++)
            {
                if (!VoidTowerAttack[i].activeInHierarchy)
                {
                    VoidTowerAttack[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayFireBallExplotion()
    {
        if (sounds)
        {
            for (int i = 0; i < FireBallExplotion.Length; i++)
            {
                if (!FireBallExplotion[i].activeInHierarchy)
                {
                    FireBallExplotion[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayBlackHoleExplotion()
    {
        if (sounds)
        {
            for (int i = 0; i < BlackHoleExplotion.Length; i++)
            {
                if (!BlackHoleExplotion[i].activeInHierarchy)
                {
                    BlackHoleExplotion[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayBlackHoleCharge()
    {
        if (sounds)
        {
            for (int i = 0; i < BlackHoleCharge.Length; i++)
            {
                if (!BlackHoleCharge[i].activeInHierarchy)
                {
                    BlackHoleCharge[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlaySpikeTowerActivate()
    {
        if (sounds)
        {
            for (int i = 0; i < SpikeTowerActivate.Length; i++)
            {
                if (!SpikeTowerActivate[i].activeInHierarchy)
                {
                    SpikeTowerActivate[i].SetActive(true);
                    break;
                }
            }
        }
    }


    //Not Pool
    public void PlayMenuSelection()
    {
        if (sounds)
        {
            MenuSelection.Play();
        }
    }

    //Not Pool
    public void PlayMenuSelectionBuilding()
    {
        if (sounds)
        {
            MenuSelectionBuilding.Play();
        }
    }

    //Not Pool
    public void PlayMenuSelectionBuildingDeny()
    {
        if (sounds)
        {
            MenuSelectionBuildingDeny.Play();
        }
    }

    //Not Pool
    public void PlayBuildingPlaceDeny()
    {
        if (sounds)
        {
            BuildingPlaceDeny.Play();
        }
    }

    //Not Pool
    public void PlayBuildingPlaced()
    {
        if (sounds)
        {
            BuildingPlaced.Play();
        }
    }

    //Pool
    public void PlayBuildingDestroyed()
    {
        if (sounds)
        {
            for (int i = 0; i < BuildingDestroyed.Length; i++)
            {
                if (!BuildingDestroyed[i].activeInHierarchy)
                {
                    BuildingDestroyed[i].SetActive(true);
                    break;
                }
            }
        }
    }

    //Pool
    public void PlayBuildingHit()
    {
        if (sounds)
        {
            for (int i = 0; i < BuildingHit.Length; i++)
            {
                if (!BuildingHit[i].activeInHierarchy)
                {
                    BuildingHit[i].SetActive(true);
                    break;
                }
            }
        }
    }

    public void PlayWarDrums()
    {
        if (sounds)
        {
            WarTime.Play();
        }
    }

    public void PlayWaveCompleted()
    {
        if (sounds)
        {
            WaveCompleted.Play();
        }
    }
}
