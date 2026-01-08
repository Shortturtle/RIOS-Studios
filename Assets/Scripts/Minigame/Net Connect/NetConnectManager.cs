using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetConnectManager : BaseMicrogameClass
{
    public List<Color> ropeColors = new List<Color>();
    public List<Rope> leftSideRopes = new List<Rope>();
    public List<Rope> rightSideRopes = new List<Rope>();

    public bool isTaskCompleted;
    private Canvas canvas;

    public Rope currentlyHoveredRope;
    public Rope currentlyDraggedRope;
    public override void StartMicrogame()
    {
        InitializeWires();
    }

    private void Start()
    {
        StartMicrogame();
    }

    private void InitializeWires()
    {
        //initializes wire variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        // sets the needed lists to prepare for random color selection
        List<Color> availableColors = ropeColors;
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
            availableLeftRopesIndex.Count > 0 &&
            availableRightRopesIndex.Count > 0)
        {
            Color pickedColor = availableColors[Random.Range(0, availableColors.Count)];

            int pickedLeftRope = Random.Range(0, availableLeftRopesIndex.Count);
            int pickedRightRope = Random.Range(0, availableRightRopesIndex.Count);

            leftSideRopes[availableLeftRopesIndex[pickedLeftRope]].SetRopeColor(pickedColor);
            rightSideRopes[availableRightRopesIndex[pickedRightRope]].SetRopeColor(pickedColor);

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
                EndMicrogame();
                break;
            }
            else
            {
                Debug.Log("TASK INCOMPLETED");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
