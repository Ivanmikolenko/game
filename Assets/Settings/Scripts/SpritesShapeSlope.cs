using UnityEngine;
using UnityEngine.U2D; // Не забудьте эту директиву!

public class SpriteShapeSlope : MonoBehaviour {
    // Ссылка на контроллер Sprite Shape
    public SpriteShapeController shapeController;

    // Настройки генерации
    public int pointCount = 50;
    
    public float heightVariation = 5f;
    public float smoothness = 2f;

    void Start() {
        GenerateSlope();
    }

    void GenerateSlope() {

        Spline spline = shapeController.spline;

        spline.Clear();

        for (int i = 0; i < pointCount; i++) {

            float x = i * smoothness;
            float y = Mathf.PerlinNoise(i * 0.2f, 0) * heightVariation;
            
            spline.InsertPointAt(i, new Vector3(x, y, 0));
            
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
        }

        shapeController.RefreshSpriteShape();
    }
}