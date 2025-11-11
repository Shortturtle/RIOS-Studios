using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

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
    protected float bufferTimerDuration;
    protected float bufferCountdownTimer;
    public bool isBuffer = false;

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

    #region Microgame Functions
    public void StartMicrogame()
    {
        GameObject microgameInstance = Instantiate(microgame, microgameCanvas.transform);
        microgameInstance.transform.GetChild(0).GetComponent<SPMGManager>().InitalizeTower(this);
    }
    #endregion
}
