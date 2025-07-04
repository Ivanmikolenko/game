using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Coin : MonoBehaviour
{
    public List<Sprite> coinConditions;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = coinConditions[0];
    }

    public void CollectCoin()
    {
        StartCoroutine(AnimateCoin());
    }

    private IEnumerator AnimateCoin()
    {
        for (int i = 1; i < coinConditions.Count; i++)
        {
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.sprite = coinConditions[i];
        }
    }
}
