using UnityEngine;

public class CreateNewSaveFile : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.CreateSaveFile();
    }
}
