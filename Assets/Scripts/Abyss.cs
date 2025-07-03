using UnityEngine;
public class Abyss : MonoBehaviour
{
    public Vector2[] worldPoints = new Vector2[4];

    private PolygonCollider2D polygonCollider;

    private void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public void UpdateCollider(Vector2[] worldPoints)
    {
        polygonCollider.pathCount = 0;

        polygonCollider.pathCount = 1;

        Vector2[] localPoints = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            localPoints[i] = transform.InverseTransformPoint(worldPoints[i]);
        }

        polygonCollider.SetPath(0, localPoints);
    }
}