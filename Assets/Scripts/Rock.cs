using UnityEngine;
using System.Collections.Generic;

public class RandomTextureApplier : MonoBehaviour
{
    public List<Sprite> rocks;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyRandomTexture();
    }

    public void ApplyRandomTexture()
    {
        int randomIndex = Random.Range(0, rocks.Count);
        spriteRenderer.sprite = rocks[randomIndex];
    }
}