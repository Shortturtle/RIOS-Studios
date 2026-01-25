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
}
