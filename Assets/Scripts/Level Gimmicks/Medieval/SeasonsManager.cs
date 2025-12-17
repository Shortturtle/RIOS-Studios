using UnityEngine;

public class SeasonsManager : MonoBehaviour
{
    //seasons manager
    //spring = 1, summer = 2, autumn = 3, winter = 4
    public int seasonNumber;


    void Start()
    {
        //default
        seasonNumber = 1;
    }

    
    //change to seasons
    public void ToSpring()
    {
        seasonNumber = 1;
    }
    public void ToSummer()
    {
        seasonNumber = 2;
    }
    public void ToAutumn()
    {
        seasonNumber = 3;
    }
    public void ToWinter()
    {
        seasonNumber = 4;
    }
}
