using UnityEngine;

public class TowerSelectMenu : MonoBehaviour
{
    private bool menuOpen = false;


    public void TowerSelectMenuPopup()
    {
        if (menuOpen == false) { OpenTowerSelectMenu(); }
        else { CloseTowerSelectMenu(); }
    }

    private void OpenTowerSelectMenu()
    {
        transform.LeanMoveLocal(new Vector2(290, 0), 0.5f).setEaseOutCirc();
        menuOpen = true;
    }
    private void CloseTowerSelectMenu()
    {
        transform.LeanMoveLocal(new Vector2(1925, 0), 0.5f).setEaseOutCirc();
        menuOpen = false;
    }
}
