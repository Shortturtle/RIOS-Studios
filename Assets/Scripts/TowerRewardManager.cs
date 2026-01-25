using UnityEngine;

public class TowerRewardManager : MonoBehaviour
{
    //strings to record the towers unlocked in playerprefs
    //to use this - if(PlayerPrefs.GetInt(tower_???) == 1) { CanUseTower or smth}
    private string tower_Portal;
    private string tower_Jinb;
    private string tower_Potion;
    private string tower_Net;
    private string tower_Diver;
    private string tower_Axe;
    private string tower_Railgun;
    private string tower_Drone;

    private void OnEnable()
    {
        GameEventManager.instance.towerRewardEvents.onTowerRewarded += TowerNumberRewardedToPlayer;
    }
    private void OnDisable()
    {
        GameEventManager.instance.towerRewardEvents.onTowerRewarded -= TowerNumberRewardedToPlayer;
    }

    //this for the tower reward, if tower reward int sent over from quest is certain number, set the tower attached to the no to be able to be used
    private void TowerNumberRewardedToPlayer(int towerRewardNumber)
    {
        if(towerRewardNumber == 101)
        {
            PlayerPrefs.SetInt(tower_Portal, 1);
        }
        if(towerRewardNumber == 1)
        {
            PlayerPrefs.SetInt(tower_Jinb, 1);
        }
        if(towerRewardNumber == 2)
        {
            PlayerPrefs.SetInt(tower_Potion, 1);
        }
        if(towerRewardNumber == 3)
        {
            PlayerPrefs.SetInt(tower_Net, 1);
        }
        if(towerRewardNumber == 4)
        {
            PlayerPrefs.SetInt(tower_Diver, 1);
        }
        if(towerRewardNumber == 5)
        {
            PlayerPrefs.SetInt(tower_Axe, 1);
        }
        if(towerRewardNumber == 6)
        {
            PlayerPrefs.SetInt(tower_Railgun, 1);
        }
        if(towerRewardNumber == 7)
        {
            PlayerPrefs.SetInt(tower_Drone, 1);
        }
    }
}
