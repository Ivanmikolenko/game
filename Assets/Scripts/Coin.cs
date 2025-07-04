using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Coin : MonoBehaviour
{
    private GameObject varManager;
    public List<Sprite> coinConditions;
    private VarManager varManagerScript;
    private SpriteRenderer spriteRenderer;
    private Collider2D collision;
    void Awake()
    {
        varManager = GameObject.FindGameObjectsWithTag("VarManager")[0];
        varManagerScript = varManager.GetComponent<VarManager>();
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
        varManagerScript.coinsCur++;
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
