using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;

public class RandomTextureApplier : MonoBehaviour
{
    public Sprite rock;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = rock;
    }
}