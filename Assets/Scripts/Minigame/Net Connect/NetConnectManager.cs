using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetConnectManager : BaseMicrogameClass
{
    public List<Color> ropeColors = new List<Color>();
    public List<Sprite> ropeSprites = new List<Sprite>();
    public List<Rope> leftSideRopes = new List<Rope>();
    public List<Rope> rightSideRopes = new List<Rope>();

    public bool isTaskCompleted;
    private Canvas canvas;

    public Rope currentlyHoveredRope;
    public Rope currentlyDraggedRope;

    public GameObject tickImg;
    public override void StartMicrogame()
    {
        InitializeWires();
    }

    private void Update()
    {
        HoverCheck();
    }

    private void InitializeWires()
    {
        //initializes wire variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        // sets the needed lists to prepare for random color selection
        List<Color> availableColors = ropeColors;
        List<Sprite> availableSprites = ropeSprites;
        List<int> availableLeftRopesIndex = new List<int>();
        List<int> availableRightRopesIndex = new List<int>();

        for (int i = 0; i < leftSideRopes.Count; i++)
        {
            leftSideRopes[i].canvas = canvas;
            leftSideRopes[i].netConnectManager = this;
            availableLeftRopesIndex.Add(i);
        }

        for (int i = 0; i < rightSideRopes.Count; i++)
        {
            rightSideRopes[i].canvas = canvas;
            rightSideRopes[i].netConnectManager = this;
            availableRightRopesIndex.Add(i);
        }

        // random color selection
        while (availableColors.Count > 0 &&
            availableSprites.Count > 0 &&
            availableLeftRopesIndex.Count > 0 &&
            availableRightRopesIndex.Count > 0)
        {
            Color pickedColor = availableColors[Random.Range(0, availableColors.Count)]; //picks random color
            Sprite pickedSprite = availableSprites[Random.Range(0, availableSprites.Count)]; //picks a random sprite

            //picks random wires
            int pickedLeftRope = Random.Range(0, availableLeftRopesIndex.Count);
            int pickedRightRope = Random.Range(0, availableRightRopesIndex.Count);

            leftSideRopes[availableLeftRopesIndex[pickedLeftRope]].SetRopeColorAndSprite(pickedColor, pickedSprite);
            rightSideRopes[availableRightRopesIndex[pickedRightRope]].SetRopeColorAndSprite(pickedColor, pickedSprite);

            // removes all selected
            availableColors.Remove(pickedColor);
            availableLeftRopesIndex.RemoveAt(pickedLeftRope);
            availableRightRopesIndex.RemoveAt(pickedRightRope);
        }

        StartCoroutine(CheckTaskCompletion());
    }

    private IEnumerator CheckTaskCompletion()
    {
        while (!isTaskCompleted)
        {
            int successfulWires = 0;

            for (int i = 0; i < rightSideRopes.Count; i++)
            {
                if (rightSideRopes[i].isSuccess) { successfulWires++; }
            }
            if (successfulWires >= rightSideRopes.Count)
            {
                Debug.Log("TASK COMPLETED");
                EndMicrogame(tickImg);
                break;
            }
            else
            {
                Debug.Log("TASK INCOMPLETED");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void HoverCheck()
    {
        int unHovered = 0;
        for (int i = 0; i < leftSideRopes.Count; i++)
        {
            if (!leftSideRopes[i].isHovered)
            {
                unHovered++;
            }
        }

        for (int i = 0; i < rightSideRopes.Count; i++)
        {
            if (!rightSideRopes[i].isHovered)
            {
                unHovered++;
            }
        }

        if(unHovered ==  leftSideRopes.Count + rightSideRopes.Count)
        {
            currentlyHoveredRope = null;
        }
    }
}
