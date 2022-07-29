using UnityEngine;

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
            Debug.Log("Instance already exists, destroying object!");
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
    [SerializeField] GameObject[] FireBallExplotion;

    [SerializeField] AudioSource MenuSelection;
    [SerializeField] AudioSource MenuSelectionBuilding;
    [SerializeField] AudioSource MenuSelectionBuildingDeny;

    [SerializeField] AudioSource BuildingPlaceDeny;
    [SerializeField] AudioSource BuildingPlaced;
    [SerializeField] GameObject[] BuildingDestroyed;
    [SerializeField] GameObject[] BuildingHit;

    [SerializeField] AudioSource WarTime;
    [SerializeField] AudioSource WaveCompleted;


    //Pool
    public void PlaySlimeJump()
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

    //Pool
    public void PlaySlimeDie()
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

    //Pool
    public void PlaySlimeHit()
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

    //Pool
    public void PlayCommonSlimeAttack()
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

    //Pool
    public void PlayArcherSlimeAttack()
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

    //Pool
    public void PlayBombSlimeAttack()
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

    //Pool
    public void PlayArcherTowerAttack()
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

    //Pool
    public void PlayFireTowerAttack()
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

    //Pool
    public void PlayFireBallExplotion()
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

    //Pool
    public void PlaySpikeTowerActivate()
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


    //Not Pool
    public void PlayMenuSelection()
    {
        MenuSelection.Play();
    }

    //Not Pool
    public void PlayMenuSelectionBuilding()
    {
        MenuSelectionBuilding.Play();
    }

    //Not Pool
    public void PlayMenuSelectionBuildingDeny()
    {
        MenuSelectionBuildingDeny.Play();
    }

    //Not Pool
    public void PlayBuildingPlaceDeny()
    {
        BuildingPlaceDeny.Play();
    }

    //Not Pool
    public void PlayBuildingPlaced()
    {
        BuildingPlaced.Play();
    }

    //Pool
    public void PlayBuildingDestroyed()
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

    //Pool
    public void PlayBuildingHit()
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

    public void PlayWarDrums()
    {
        WarTime.Play();
    }

    public void PlayWaveCompleted()
    {
        WaveCompleted.Play();
    }
}
