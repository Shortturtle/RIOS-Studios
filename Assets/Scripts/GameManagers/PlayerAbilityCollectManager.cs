using UnityEngine;

public class PlayerAbilityCollectManager : MonoBehaviour
{
    private string ability_Freeze;
    private string ability_Reverse;
    private string ability_Holo;
    private string ability_Trans;

    public int abilityGet;

    private void Start()
    {
        if (!(abilityGet == 0))
        {
            PlayerAbilityGet(abilityGet);
        }
    }

    //this for the tower reward, if tower reward int sent over from quest is certain number, set the tower attached to the no to be able to be used
    private void PlayerAbilityGet(int abilityNumber)
    {
        if (abilityNumber == 1)
        {
            PlayerPrefs.SetInt(ability_Freeze, 1);
        }
        if (abilityNumber == 2)
        {
            PlayerPrefs.SetInt(ability_Reverse, 1);
        }
        if (abilityNumber == 3)
        {
            PlayerPrefs.SetInt(ability_Holo, 1);
        }
        if (abilityNumber == 4)
        {
            PlayerPrefs.SetInt(ability_Trans, 1);
        }
    }
}
