using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static Transform[] points;
    public static float totalDistance = 0;

    private void Awake()
    {
        Transform previousPoint = null;

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);

            if (previousPoint != null)
            {
                totalDistance += Vector3.Distance(previousPoint.position, points[i].position);
            }

            previousPoint = points[i];
        }

        Debug.Log(totalDistance);
    }
}
