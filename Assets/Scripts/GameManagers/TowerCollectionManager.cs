using UnityEngine;

public class TowerCollectionManager : MonoBehaviour
{
    //what towers to enable n disable if they have it or not
    public GameObject tower_Portal;
    public GameObject tower_Jinb;
    public GameObject tower_Potion;
    public GameObject tower_Net;
    public GameObject tower_Diver;
    public GameObject tower_Axe;
    public GameObject tower_Railgun;
    public GameObject tower_Drone;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("tower_Jinb"));
        Debug.Log("test");

        if(PlayerPrefs.GetInt("tower_Portal") == 1) { tower_Portal.SetActive(true); }
        else { tower_Portal.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Jinb") == 1) { tower_Jinb.SetActive(true); }
        else { tower_Jinb.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Potion") == 1) { tower_Potion.SetActive(true); }
        else { tower_Potion.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Net") == 1) { tower_Net.SetActive(true); }
        else { tower_Net.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Diver") == 1) { tower_Diver.SetActive(true); }
        else { tower_Diver.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Axe") == 1) { tower_Axe.SetActive(true); }
        else { tower_Axe.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Railgun") == 1) { tower_Railgun.SetActive(true); }
        else { tower_Railgun.SetActive(false); }

        if(PlayerPrefs.GetInt("tower_Drone") == 1) { tower_Drone.SetActive(true); }
        else { tower_Drone.SetActive(false); }
    }

}
