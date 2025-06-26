using UnityEngine;
using UnityEngine.U2D;

public class Ground : MonoBehaviour
{
    public SpriteShapeController shapeController;
    public int pointCount = 100;
    public float maxHeight = 5f;
    public float minHeight = 1f;
    public float smoothness = 3f;
    public float noiseScale = 0.05f;
    public float decreasePriority = 0.7f;

    public bool drawPointsInRuntime = true;
    public Color pointColor = Color.green;
    public float pointRadius = 0.2f;
    public float debugDisplayTime = 1f;

    void Start()
    {
        GenerateDecreasingTopEdge();
    }

    void GenerateDecreasingTopEdge()
    {
        Spline spline = shapeController.spline;
        spline.Clear();

        float width = (pointCount - 1) * smoothness;
        float baseHeight = 0f;

        spline.InsertPointAt(0, new Vector3(0, baseHeight, 0));
        DrawDebugPoint(spline.GetPosition(0), 0);

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * smoothness;
            float progress = (float)i / (pointCount - 1);

            float noise = Mathf.PerlinNoise(i * noiseScale, 0);

            float heightDecrease = Mathf.Lerp(maxHeight, minHeight, progress * decreasePriority);

            float y = baseHeight + Mathf.Min(noise * maxHeight, heightDecrease);

            spline.InsertPointAt(i + 1, new Vector3(x, y, 0));
            spline.SetTangentMode(i + 1, ShapeTangentMode.Continuous);
            Debug.Log($"x = {x}, y = {y}");
            DrawDebugPoint(spline.GetPosition(i + 1), i + 1);
        }

        spline.InsertPointAt(pointCount + 1, new Vector3(width, baseHeight, 0));
        DrawDebugPoint(spline.GetPosition(pointCount + 1), pointCount + 1);

        shapeController.RefreshSpriteShape();
    }

    public Vector3 GetTopRightPosition()
    {
        if (shapeController == null) return transform.position;

        Bounds bounds = shapeController.GetComponent<SpriteShapeRenderer>().bounds;
        return bounds.max;
    }

    void LogTopRightCorner()
    {
        Vector3 topRight = GetTopRightPosition();
        Debug.Log($"Правый верхний угол объекта: {topRight}");
        Debug.DrawRay(topRight, Vector2.up * 0.5f, Color.red, 2f);
        Debug.DrawRay(topRight, Vector2.right * 0.5f, Color.red, 2f);
    }
    void DrawDebugPoint(Vector3 position, int pointIndex)
    {
        position = transform.TransformPoint(position);
        Debug.DrawRay(position, Vector3.up * pointRadius, pointColor, debugDisplayTime);
        Debug.DrawRay(position, Vector3.down * pointRadius, pointColor, debugDisplayTime);
        Debug.DrawRay(position, Vector3.left * pointRadius, pointColor, debugDisplayTime);
        Debug.DrawRay(position, Vector3.right * pointRadius, pointColor, debugDisplayTime);
    }
    void OnDrawGizmos()
    {
        if (!Application.isPlaying && shapeController != null)
        {
            Spline spline = shapeController.spline;
            for (int i = 0; i < spline.GetPointCount(); i++)
            {
                Gizmos.color = pointColor;
                Gizmos.DrawSphere(spline.GetPosition(i), pointRadius);
            }
        }
    }
    Vector2 GetSpritePivotOffset()
    {
        SpriteRenderer renderer = shapeController.GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.sprite != null)
        {
            Sprite sprite = renderer.sprite;
            return sprite.pivot - new Vector2(sprite.rect.width, sprite.rect.height);
        }
        return Vector2.zero;
    }
}