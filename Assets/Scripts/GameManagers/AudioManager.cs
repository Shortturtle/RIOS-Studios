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

    public void PlayAudioEvent(AK.Wwise.Event audioEvent)
    {
        audioEvent.Post(gameObject);
    }

    public void StopAudioEvent(AK.Wwise.Event audioEvent)
    {
        audioEvent.Stop(gameObject);
    }

    public void StopAllAudioEvents()
    {
        stopAllAudio.Post(gameObject);
    }
}
