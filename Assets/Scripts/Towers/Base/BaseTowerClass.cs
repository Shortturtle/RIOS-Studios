using UnityEngine;

public class BaseTowerClass: MonoBehaviour
{
    protected float degradeTimerDuration;
    protected float countdownTimer;

    //Tower Cost System (TBD)

    //Placement System working with UI (also TBD)

    #region Degrade Functions
    protected virtual void Degrade() // call public ovverride void Degrade() to add custom degrade code for new towers
    {

    }

    protected virtual void DegradeTimer() // Literally just a timer (can also be overridden)
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
        }

        else if (countdownTimer < 0)
        {
            Degrade();
        }
    }

    protected virtual void ResetDegradeTimer() // resets timer, call usually after Degrade() (can also be overridden)
    {
        countdownTimer = degradeTimerDuration;
    }
    #endregion

    #region Level Functions
    // to add if needed
    #endregion
}
