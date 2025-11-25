using UnityEngine;

public class TowerRewardManager : MonoBehaviour
{
    //idk how to properly use player prefs so imma store the towers as ints
    //so like tower A will be = 1, tower B will be = 2, etc
    private string towerReward;
    private int towerRewardNumber;

    private void OnEnable()
    {
        GameEventManager.instance.towerRewardEvents.onTowerRewarded += TowerNumberRewardedToPlayer;
    }
    private void OnDisable()
    {
        GameEventManager.instance.towerRewardEvents.onTowerRewarded -= TowerNumberRewardedToPlayer;
    }

    ////delete below if unneeded
    //private void Start()
    //{
    //    GameEventManager.instance.towerRewardEvents.TowerRewardChange(towerRewardNumber);
    //}

    //this for the tower reward
    private void TowerNumberRewardedToPlayer(int towerRewardNumber)
    {
        PlayerPrefs.SetInt("towerReward", towerRewardNumber);
        //GameEventManager.instance.towerRewardEvents.TowerRewardChange(towerRewardNumber);
    }

}
