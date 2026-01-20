using UnityEngine;

public class JackInBoxTower : OffenseTowerBase
{
    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
