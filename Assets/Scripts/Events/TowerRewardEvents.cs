using UnityEngine;
using System;

public class TowerRewardEvents
{
    public event Action<int> onTowerRewarded;

    public void TowerRewards(int towerRewardNumber)
    {
        if (onTowerRewarded != null)
        {
            onTowerRewarded(towerRewardNumber);
        }
    }

    //delete below of unneeded
    public event Action<int> onTowerRewardChange;

    public void TowerRewardChange(int towerRewardNumber)
    {
        if (onTowerRewardChange != null)
        {
            onTowerRewardChange(towerRewardNumber);
        }
    }
}
