using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

public class TowerButtonScript : MonoBehaviour
{
    public RectTransform portalBounds;
    public RectTransform buttonBounds;

    public GameObject tower;
    public float popOutDuration;
    public float popOutScaleMultiplier;
    public UIEffect uiEffect;
    private Image image;
    private bool selectGoal = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        uiEffect = GetComponent<UIEffect>();
        uiEffect.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonSelect();
    }

    public void SpawnTower()
    {
        if (!IsFullyInside(buttonBounds, portalBounds))
        {
            Debug.Log("OutsidePortalBounds");
            return;
        }

        if (tower.GetComponent<BaseTowerClass>() != null)
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
        uiEffect.enabled = selected;
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
    public bool IsFullyInside(RectTransform inner, RectTransform outer)
    {
        Vector3[] innerCorners = new Vector3[4];
        Vector3[] outerCorners = new Vector3[4];

        inner.GetWorldCorners(innerCorners);
        outer.GetWorldCorners(outerCorners);

        // outerCorners: [0] = bottom-left, [1] = top-left, [2] = top-right, [3] = bottom-right
        Rect outerRect = new Rect(
            outerCorners[0].x,
            outerCorners[0].y,
            outerCorners[2].x - outerCorners[0].x,
            outerCorners[2].y - outerCorners[0].y
        );

        foreach (Vector3 corner in innerCorners)
        {
            if (!outerRect.Contains(corner))
                return false;
        }

        return true;
    }
}
