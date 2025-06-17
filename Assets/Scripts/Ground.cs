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

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * smoothness;
            float progress = (float)i / (pointCount - 1);

            float noise = Mathf.PerlinNoise(i * noiseScale, 0);

            float heightDecrease = Mathf.Lerp(maxHeight, minHeight, progress * decreasePriority);

            float y = baseHeight + Mathf.Min(noise * maxHeight, heightDecrease);

            spline.InsertPointAt(i + 1, new Vector3(x, y, 0));
            spline.SetTangentMode(i + 1, ShapeTangentMode.Continuous);
        }

        spline.InsertPointAt(pointCount + 1, new Vector3(width, baseHeight, 0));

        shapeController.RefreshSpriteShape();
        UpdateCollider();

    }
    void UpdateCollider()
    {
        if (shapeController == null) return;

        PolygonCollider2D polyCollider = GetComponent<PolygonCollider2D>();
        if (polyCollider == null)
        {
            polyCollider = gameObject.AddComponent<PolygonCollider2D>();
        }

        Spline spline = shapeController.spline;
        int pointCount = spline.GetPointCount();

        Vector2 spritePivot = GetSpritePivotOffset();

        Vector2[] colliderPath = new Vector2[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            Vector3 worldPos = spline.GetPosition(i);

            Vector2 localPos = transform.InverseTransformPoint(worldPos);
            localPos -= spritePivot;

            colliderPath[i] = localPos;
        }

        polyCollider.pathCount = 1;
        polyCollider.SetPath(0, colliderPath);
        polyCollider.offset = Vector2.zero;
    }

    Vector2 GetSpritePivotOffset()
    {
        SpriteShapeRenderer renderer = shapeController.GetComponent<SpriteShapeRenderer>();
        if (renderer != null && renderer.sprite != null)
        {
            Sprite sprite = renderer.sprite;
            return sprite.pivot - new Vector2(sprite.rect.width, sprite.rect.height) * 0.5f;
        }
        return Vector2.zero;
    }
}