using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BaseTowerClass: MonoBehaviour
{
    protected float degradeTimerDuration;
    protected float degradeCountdownTimer;
    public bool isDegraded = false;
    public int degradeRank = 0;
    public int maxDegradeRank;

    protected float overdriveTimerDuration;
    protected float overdriveCountdownTimer;
    public bool isOverdrive = false;

    public GameObject microgameCanvas;
    public GameObject microgame;

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
        if (degradeCountdownTimer >= 0)
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

    #region Microgame Functions
    public void StartMicrogame()
    {
        GameObject microgameInstance = Instantiate(microgame, microgameCanvas.transform);
        microgameInstance.transform.GetChild(0).GetComponent<FNMGManager>().InitalizeTower(this);
    }
    #endregion
}
