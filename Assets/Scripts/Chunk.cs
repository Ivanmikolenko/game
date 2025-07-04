using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.PlayerSettings;

public class Chunk : MonoBehaviour
{
    public GameObject coin;
    public SpriteShapeController shapeController;
    private int pointCount = 50;
    private float maxHeight = 5f;
    private float minHeight = 0f;
    private float smoothness = 3f;
    private float noiseScale = 0.05f;
    private float decreasePriority = 0.9f;

    private Vector2 leftBotPoint, leftTopPoint, rightBotPoint, rightTopPoint;
    private PolygonCollider2D polygonCollider;
    private Color pointColor = Color.green;
    private float pointRadius = 0.2f;
    private float debugDisplayTime = 1f;
    private int minPointCount = 30;
    private int maxPointCount = 150;

    void Awake()
    {
        RandomizeParameters();
        GenerateDecreasingTopEdge();
    }
    public void RandomizeParameters()
    {
        pointCount = Random.Range(minPointCount, maxPointCount + 1);
        decreasePriority = Random.Range(0.7f, 0.9f);
        //maxHeight = Random.Range(4f, 10f);
    }
    void Update()
    {
        //Spline spline = shapeController.spline;
        //if (spline.GetPointCount() >= pointCount) UpdatePoints();
        //if (leftBotPoint != null) DrawDebugPoint(leftBotPoint);
        //if (rightBotPoint != null) DrawDebugPoint(rightBotPoint);
        //if (leftTopPoint != null) DrawDebugPoint(leftTopPoint);
        //if (rightTopPoint != null) DrawDebugPoint(rightTopPoint);
    }
    void GenerateDecreasingTopEdge()
    {
        Spline spline = shapeController.spline;
        spline.Clear();

        float width = (pointCount - 1) * smoothness;
        float baseHeight = 0f;

        spline.InsertPointAt(0, new Vector3(0, baseHeight, 0));
        //DrawDebugPoint(spline.GetPosition(0), 0);

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * smoothness;
            float progress = (float)i / (pointCount - 1);

            float noise = Mathf.PerlinNoise(i * noiseScale, 0);

            float heightDecrease = Mathf.Lerp(maxHeight, minHeight, progress * decreasePriority);

            float y = baseHeight + Mathf.Min(noise * maxHeight, heightDecrease);

            spline.InsertPointAt(i + 1, new Vector3(x, y, 0));
            spline.SetTangentMode(i + 1, ShapeTangentMode.Continuous);
            //Debug.Log($"x = {x}, y = {y}");
            //DrawDebugPoint(spline.GetPosition(i + 1), i + 1);
        }

        spline.InsertPointAt(pointCount + 1, new Vector3(width, baseHeight, 0));
        //DrawDebugPoint(spline.GetPosition(pointCount + 1), pointCount + 1);
        //leftBotPoint = transform.TransformPoint(spline.GetPosition(0));
        //leftTopPoint = transform.TransformPoint(spline.GetPosition(1));
        //rightTopPoint = transform.TransformPoint(spline.GetPosition(pointCount));
        //rightBotPoint = transform.TransformPoint(spline.GetPosition(pointCount - 1));
        UpdatePoints();

        shapeController.RefreshSpriteShape();
    }
    public void UpdatePoints()
    {
        Spline spline = shapeController.spline;
        leftBotPoint = transform.TransformPoint(spline.GetPosition(0));
        leftTopPoint = transform.TransformPoint(spline.GetPosition(1));
        rightTopPoint = transform.TransformPoint(spline.GetPosition(pointCount));
        rightBotPoint = transform.TransformPoint(spline.GetPosition(pointCount + 1));

        DrawDebugPoint(leftBotPoint);
        DrawDebugPoint(leftTopPoint);
        DrawDebugPoint(rightTopPoint);
        DrawDebugPoint(rightBotPoint);
    }
    public void AddCoins()
    {
        Spline spline = shapeController.spline;
        int randomPos = Random.Range(10, 20);
        int numOfCoins = Random.Range(3, 5);
        for (int i = 0; i < numOfCoins; i++)
        {
            GameObject newCoin = Instantiate(
                coin,
                new Vector2(
                    transform.TransformPoint(spline.GetPosition(i + randomPos)).x,
                    transform.TransformPoint(spline.GetPosition(i + randomPos)).y + 1.7f
                ),
                transform.rotation
            );
        }
    }
    public Vector2 GetLeftBotPoint()
    {
        return this.leftBotPoint;
    }
    public Vector2 GetRightTopPoint()
    {
        return this.rightTopPoint;
    }
    public Vector2 GetRightBotPoint()
    {
        Debug.Log(this.rightBotPoint);
        return this.rightBotPoint;
    }
    public Vector2 GetLeftTopPoint()
    {
        return this.leftTopPoint;
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
    void DrawDebugPoint(Vector2 position)
    {
        //position = transform.TransformPoint(position);
        Debug.DrawRay(position, Vector2.up * pointRadius, pointColor, debugDisplayTime);
        Debug.DrawRay(position, Vector2.down * pointRadius, pointColor, debugDisplayTime);
        Debug.DrawRay(position, Vector2.left * pointRadius, pointColor, debugDisplayTime);
        Debug.DrawRay(position, Vector2.right * pointRadius, pointColor, debugDisplayTime);
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
    //Vector2 GetSpritePivotOffset()
    //{
    //    SpriteRenderer renderer = shapeController.GetComponent<SpriteRenderer>();
    //    if (renderer != null && renderer.sprite != null)
    //    {
    //        Sprite sprite = renderer.sprite;
    //        return sprite.pivot - new Vector2(sprite.rect.width, sprite.rect.height);
    //    }
    //    return Vector2.zero;
    //}
}