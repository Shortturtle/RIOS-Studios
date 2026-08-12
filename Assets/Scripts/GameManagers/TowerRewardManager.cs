using System.Collections.Generic;
using UnityEngine;

public class TowerRewardManager : MonoBehaviour
{
    //strings to record the towers unlocked in playerprefs
    //to use this - if(PlayerPrefs.GetInt(tower_???) == 1) { CanUseTower or smth}
    private string tower_Portal = "tower_Portal";
    private string tower_Portal_Permanent = "tower_Portal_Permanent";
    private string tower_Jinb = "tower_Jinb";
    private string tower_Potion = "tower_Potion";
    private string tower_Net = "tower_Net";
    private string tower_Diver = "tower_Diver";
    private string tower_Axe = "tower_Axe";
    private string tower_Railgun = "tower_Railgun";

    public int towerRewardSet;

    public static TowerRewardManager instance;

    private void OnEnable()
    {
        GameEventManager.instance.towerRewardEvents.onTowerRewarded += TowerNumberRewardedToPlayer;
    }
    private void OnDisable()
    {
        GameEventManager.instance.towerRewardEvents.onTowerRewarded -= TowerNumberRewardedToPlayer;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {


        //to disable portal guy if not unlocked at the end yet
        if (towerCollectionList.Count > 8 && towerCollectionList[8] == 0)
        {
            if (towerCollectionList.Count > 7)
                towerCollectionList[7] = 0;
        }

        //for towers that are gained through going to new era
        if (!(towerRewardSet == 0))
        {
            GameEventManager.instance.towerRewardEvents.TowerRewards(towerRewardSet);
        }
    }

    //this for the tower reward, if tower reward int sent over from quest is certain number, set the tower attached to the no to be able to be used
    private void TowerNumberRewardedToPlayer(int towerRewardNumber)
    {
        if(towerRewardNumber == 101)
        {
            //PlayerPrefs.SetInt(tower_Portal, 1);
            towerRewardNumber = 7;
        }
        else if(towerRewardNumber == 202)
        {
            towerRewardNumber = 8;
            //PlayerPrefs.SetInt(tower_Portal, 1);
            //PlayerPrefs.SetInt(tower_Portal_Permanent, 1);
        }
        //else if(towerRewardNumber == 1)
        //{
        //    PlayerPrefs.SetInt(tower_Jinb, 1);
        //}
        //else if (towerRewardNumber == 2)
        //{
        //    PlayerPrefs.SetInt(tower_Potion, 1);
        //}
        //else if (towerRewardNumber == 3)
        //{
        //    PlayerPrefs.SetInt(tower_Net, 1);
        //}
        //else if (towerRewardNumber == 4)
        //{
        //    PlayerPrefs.SetInt(tower_Diver, 1);
        //}
        //else if (towerRewardNumber == 5)
        //{
        //    PlayerPrefs.SetInt(tower_Axe, 1);
        //}
        //else if (towerRewardNumber == 6)
        //{
        //    PlayerPrefs.SetInt(tower_Railgun, 1);
        //}

        towerCollectionList[towerRewardNumber] = 1;
    }

    private int towerSaveInteger;
    private int towerSavedState;
    public List<int> towerCollectionList = new List<int>();

    #region Save and Load
    public void Save(ref TowerSaveData data)
    {
        Debug.Log("Save");
        if (data.towerSaveDataList.Count == 0) { data.towerSaveDataList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 }; }  //if list created but no numbers, create list with all 9 ints
        data.towerSaveDataList = towerCollectionList;
    }

    public void Load(TowerSaveData data)
    {
        Debug.Log("Load");
        if (data.towerSaveDataList.Count == 0) { data.towerSaveDataList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 }; }  //if list created but no numbers, create list with all 9 ints
       
        while (towerCollectionList.Count < data.towerSaveDataList.Count)
        {
            towerCollectionList.Add(0); // default "not unlocked"
        }
        towerCollectionList = data.towerSaveDataList;
    }

    #endregion
}

//save system part
[System.Serializable]

public struct TowerSaveData
{
    public List<int> towerSaveDataList;
}
