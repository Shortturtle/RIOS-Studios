using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public GameObject ability_Freeze;
    public GameObject ability_Reverse;
    public GameObject ability_Holo;
    public GameObject ability_Trans;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ability_Freeze") == 1) { ability_Freeze.SetActive(true); }
        else { ability_Freeze.SetActive(false); }

        if (PlayerPrefs.GetInt("ability_Reverse") == 1) { ability_Reverse.SetActive(true); }
        else { ability_Reverse.SetActive(false); }

        if (PlayerPrefs.GetInt("ability_Holo") == 1) { ability_Holo.SetActive(true); }
        else { ability_Holo.SetActive(false); }

        if (PlayerPrefs.GetInt("ability_Trans") == 1) { ability_Trans.SetActive(true); }
        else { ability_Trans.SetActive(false); }
    }
}
