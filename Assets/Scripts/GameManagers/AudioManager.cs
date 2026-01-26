using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AK.Wwise.Event stopAllAudio;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"AudioManager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }
    }

    public void PlayAudioEvent(AK.Wwise.Event audioEvent, GameObject gO)
    {
        audioEvent.Post(gO);
    }

    public void StopAudioEvent(AK.Wwise.Event audioEvent, GameObject gO)
    {
        audioEvent.Stop(gO);
    }

    public void StopAllAudioEvents(GameObject gO)
    {
        stopAllAudio.Post(gO);
    }
}
