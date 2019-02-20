using UnityEngine;

public class WayPointsPath : MonoBehaviour
{
    public Transform[] wayPoints;

    private void OnDrawGizmos()
    {
        if (wayPoints == null || wayPoints.Length <= 1) return;

        for (int i = 0; i < wayPoints.Length - 1; i++)
        {
            Debug.DrawLine(wayPoints[i].position, wayPoints[i + 1].position, Color.red);
        }
    }
}