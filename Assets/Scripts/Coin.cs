using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Coin : MonoBehaviour
{
    public List<Sprite> coinConditions;
    private SpriteRenderer spriteRenderer;
    private Collider2D collision;
    void Awake()
    {
        collision = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = coinConditions[0];
    }

    private void Update()
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectCoin();
            Destroy(gameObject);
        }
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
