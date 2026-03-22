using UnityEngine;

public class SeasonsManager : MonoBehaviour
{
    //enable disable gameobjects and particles on camera, change material for main mesh and water, disable grass and flower in winter

    //seasons manager
    //spring = 1, summer = 2, autumn = 3, winter = 4
    public int seasonNumber;
    private int prevSeasonNum;

    //main gameobjects to enable and disable
    public GameObject seasonDefault;
    public GameObject springObject;
    public GameObject summerObject;
    public GameObject autumnObject;
    public GameObject winterObject;

    //misc to enable n disable
    public GameObject foliage;

    //material for rivers in lvl
    public Material waterRiver;
    public Material iceRiver;

    //gameobject for terrain and river, mostly to reference for renderer
    public GameObject terrainMesh;
    public GameObject riverMesh;

    //the thing to change
    private Renderer terrainRenderer;
    private Renderer riverRenderer;

    void Start()
    {
        //default season
        seasonNumber = 0;

        //reference renderer
        riverRenderer = riverMesh.GetComponent<Renderer>();
        terrainRenderer = terrainMesh.GetComponent<Renderer>();
    }

    //randomise the seasons
    public void SeasonsChanger()
    {
        int seasonChanger = Random.Range(1, 5);

        if (seasonChanger == prevSeasonNum)
        {
            seasonChanger = Random.Range(1, 5);
        }

        if (seasonChanger == 1) { ToSpring(); }
        if(seasonChanger == 2) { ToSummer(); }
        if(seasonChanger == 3) { ToAutumn(); }
        if(seasonChanger == 4) { ToWinter(); }

        prevSeasonNum = seasonChanger;
    }

    //func to changing to each season, mainly enabling and disabling GObjects
    private void ToSpring()
    {
        seasonDefault.SetActive(false);
        seasonNumber = 1;
        springObject.SetActive(true);
        summerObject.SetActive(false);
        autumnObject.SetActive(false);
        winterObject.SetActive(false);

        riverRenderer.material = waterRiver;
    }
    private void ToSummer()
    {
        seasonDefault.SetActive(false);
        seasonNumber = 2;
        springObject.SetActive(false);
        summerObject.SetActive(true);
        autumnObject.SetActive(false);
        winterObject.SetActive(false);

        riverRenderer.material = waterRiver;
    }
    private void ToAutumn()
    {
        seasonDefault.SetActive(false);
        seasonNumber = 3;
        springObject.SetActive(false);
        summerObject.SetActive(false);
        autumnObject.SetActive(true);
        winterObject.SetActive(false);

        riverRenderer.material = waterRiver;
    }
    private void ToWinter()
    {
        seasonDefault.SetActive(false);
        seasonNumber = 4;
        springObject.SetActive(false);
        summerObject.SetActive(false);
        autumnObject.SetActive(false);
        winterObject.SetActive(true);

        riverRenderer.material = iceRiver;
    }
}
