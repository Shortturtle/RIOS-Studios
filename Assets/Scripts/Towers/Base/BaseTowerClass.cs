using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BaseTowerClass: MonoBehaviour
{
    protected float degradeTimerDuration;
    protected float degradeCountdownTimer;
    public bool isMaxDegraded = false;
    public int degradeRank = 0;
    public int maxDegradeRank;
    public GameObject degradeSign;

    protected float overdriveTimerDuration;
    protected float overdriveCountdownTimer;
    public bool isOverdrive = false;
    protected float bufferTimerDuration;
    protected float bufferCountdownTimer;
    public bool isBuffer = false;

    public bool isHovered;
    protected bool isStunned;

    public int cost;

    public GameObject microgame;

    public virtual void InitializeTower()
    {

    }

    #region Degrade Functions
    protected virtual void Degrade() // call public override void Degrade() to add custom degrade code for new towers
    {

    }

    public virtual void RepairTower() // call public override void UndoDegrade() to add custom degrade code for new towers
    {

    }

    protected virtual void OverDrive()
    {

    }

    protected virtual void GeneralDegradeTracker()
    {
        if (Time.timeScale == 0) { return; }

        if (isOverdrive)
        {
            OverDriveTimer();
        }

        else if (isBuffer)
        {
            BufferTimer();
        }

        else
        {
            DegradeTimer();
        }

        MaxDegradeTracker();
    }

    protected virtual void MaxDegradeTracker()
    {
        //If the degrade rank is at max && isn't already marked as max degraded, mark it as max degraded and show the degrade sign and VFX.
        if (degradeRank == maxDegradeRank && !isMaxDegraded)
        {
            isMaxDegraded = true;
            degradeSign.SetActive(true);
        }
        
        else if (!isMaxDegraded)
        {
            degradeSign.SetActive(false);
        }
    }

    protected virtual void DegradeTimer() // Literally just a timer (can also be overridden)
    {
        if (degradeCountdownTimer >= 0)
        {
            degradeCountdownTimer -= Time.deltaTime;
        }

        else if (degradeCountdownTimer < 0 && degradeRank < maxDegradeRank)
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

        else if (overdriveCountdownTimer < 0)
        {
            OverDriveEnd();
        }
    }

    protected virtual void BufferTimer()
    {
        if (bufferCountdownTimer > 0)
        {
            bufferCountdownTimer -= Time.deltaTime;
        }

        else if (bufferCountdownTimer < 0)
        {
            BufferEnd();
        }
    }

    protected virtual void OverDriveEnd()
    {
        isOverdrive = false;
        isBuffer = true;
        bufferCountdownTimer = bufferTimerDuration;
    }

    protected virtual void BufferEnd()
    {
        ResetDegradeTimer();
        isBuffer = false;
        Debug.Log("BufferEnd");
    }

    protected virtual void ResetDegradeTimer() // resets timer, call usually after Degrade() (can also be overridden)
    {
        degradeCountdownTimer = degradeTimerDuration;
    }
    #endregion

    #region UI Functions

    public virtual void HoverUIHandler()
    {
        
    }
    public virtual void InitializeHoverUI()
    {

    }

    public virtual void DeleteHoverUI()
    {

    }
    #endregion
}
