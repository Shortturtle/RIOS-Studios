using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonScript : MonoBehaviour
{
    public GameObject tower;
    public float popOutDuration;
    public float popOutScaleMultiplier;
    public Material material;
    private Image image;
    private bool selectGoal = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        material = new Material(image.material);
        material.mainTexture = image.sprite.texture;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonSelect();
    }

    public void SpawnTower()
    {
        if(tower.GetComponent<BaseTowerClass>() != null)
        {
            BuildingManager.instance.TowerPlacement(tower);
        }
    }

    private void ButtonSelect()
    {
        if (BuildingManager.instance.towerToPlace == tower && !selectGoal)
        {
            selectGoal = true;
            StartCoroutine(ButtonMovement(selectGoal));
        }

        else if (BuildingManager.instance.towerToPlace != tower && selectGoal)
        {
            selectGoal = false;
            StartCoroutine(ButtonMovement(selectGoal));
        }
    }

    private IEnumerator ButtonMovement(bool selected)
    {
        float timer = 0f;
        while (timer < popOutDuration / 2)
        {
            timer += Time.deltaTime;
            gameObject.transform.localScale = selected? Vector3.one * Mathf.Lerp(1, popOutScaleMultiplier, timer/popOutDuration) : Vector3.one * Mathf.Lerp(popOutScaleMultiplier, 1, timer / popOutDuration);
            yield return null;
        }

        image.maskable = !selected;
        Image[] imageComponents = this.GetComponentsInChildren<Image>();
        foreach (Image image in imageComponents)
        {
            image.maskable = !selected;
        }
        TextMeshProUGUI[] textComponents = this.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in textComponents)
        {
            text.maskable = !selected;
        }

        while (timer < popOutDuration)
        {
            timer += Time.deltaTime;
            gameObject.transform.localScale = selected ? Vector3.one * Mathf.Lerp(1, popOutScaleMultiplier, timer / popOutDuration) : Vector3.one * Mathf.Lerp(popOutScaleMultiplier, 1, timer / popOutDuration);
            yield return null;
        }

    }
}
