using UnityEngine;

public class MicrogameManager : MonoBehaviour
{
    public static MicrogameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Microgame Manager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }
    }

    public bool currentlyPlayingMinigame;
    public BaseTowerClass targettedTower;
    public GameObject chosenMicrogame;

    public void MicrogameStart( BaseTowerClass tower,  GameObject microgame)
    {
        if (microgame.GetComponent<BaseMicrogameClass>() != null)
        {
            targettedTower = tower;
            chosenMicrogame = microgame;

            var microgameInstance = Instantiate(microgame, GameObject.FindGameObjectWithTag("MicrogameCanvas").transform);
            microgameInstance.GetComponent<BaseMicrogameClass>().StartMicrogame();

            currentlyPlayingMinigame = true;
        }

        else
        {
            Debug.LogError($"Invalid Microgame{microgame}");
        }
    }

    public void MicrogameEnd()
    {
        targettedTower.RepairTower();
        ResourceManager.instance.AddAbilityPoint(1);
        currentlyPlayingMinigame = false;
        targettedTower = null;
        chosenMicrogame = null;
    }
}
