using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BaseTowerClass: MonoBehaviour
{
    protected float degradeTimerDuration;
    protected float degradeCountdownTimer;
    public bool isDegraded;
    public int degradeRank;
    protected int maxDegradeRank;

    protected float overdriveTimerDuration;
    protected float overdriveCountdownTimer;
    public bool isOverdrive;

    //Tower Cost System (TBD)

    //Placement System working with UI (also TBD)

    #region Degrade Functions
    protected virtual void Degrade() // call public override void Degrade() to add custom degrade code for new towers
    {

    }

    public virtual void UndoDegrade() // call public override void UndoDegrade() to add custom degrade code for new towers
    {

    }

    protected virtual void OverDrive()
    {

    }

    protected virtual void GeneralTimer()
    {
        if (!isOverdrive)
        {
            DegradeTimer();
        }

        else
        {
            OverDriveTimer();
        }
    }

    protected virtual void DegradeTimer() // Literally just a timer (can also be overridden)
    {
        if (degradeCountdownTimer > 0)
        {
            degradeCountdownTimer -= Time.deltaTime;
        }

        if (degradeCountdownTimer < 0 && degradeRank < maxDegradeRank)
        {
            Degrade();
        }
    }

    protected virtual void OverDriveTimer()
    {
        if (overdriveCountdownTimer > 0)
        {
            overdriveCountdownTimer -= Time.deltaTime;
        }

        if (overdriveCountdownTimer < 0)
        {
            isOverdrive = false;
        }
    }

    protected virtual void ResetDegradeTimer() // resets timer, call usually after Degrade() (can also be overridden)
    {
        degradeCountdownTimer = degradeTimerDuration;
    }
    #endregion
}
