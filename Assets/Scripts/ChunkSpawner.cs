using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;
using System.Linq;
public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunk;
    public List<GameObject> rocks;
    public GameObject abyss;
    private GameObject lastChunk, middleChunk, firstChunk;
    public GameObject playerObject;
    Player playerScript;
    Chunk firstChunkScript, middleChunkScript, lastChunkScript;
    private Vector2 playerPosition;
    void SpawnChunk()
    {
        Vector2 pos = new Vector2(0f, -5f);
        if (lastChunk != null)
        {
            pos = lastChunkScript.GetRightBotPoint();
        }
        GameObject newChunk = Instantiate(chunk, pos, transform.rotation);
        Chunk newChunkScript = newChunk.GetComponent<Chunk>();

        if (firstChunk != null)
        {
            Destroy(firstChunk);
        }

        firstChunk = middleChunk;
        firstChunkScript = middleChunkScript;

        middleChunk = lastChunk;
        middleChunkScript = lastChunkScript;

        lastChunk = newChunk;
        lastChunkScript = newChunkScript;

        if (middleChunk != null)
        {
            pos.y = pos.y - (lastChunkScript.GetLeftTopPoint().y - middleChunkScript.GetRightTopPoint().y);
            int randomK = Random.Range(1, 10);
            if (randomK >= 7)
            {
                pos.x += Random.Range(3f, 5f);
                lastChunk.transform.position = pos;
                lastChunkScript.UpdatePoints();

                GameObject newAbyss = Instantiate(abyss, pos, transform.rotation);
                Abyss newAbyssScript = newAbyss.GetComponent<Abyss>();
                Vector2[] points = new Vector2[]
                {
                    middleChunkScript.GetRightBotPoint(),
                    middleChunkScript.GetRightTopPoint(),
                    new Vector2(lastChunkScript.GetLeftTopPoint().x, lastChunkScript.GetLeftTopPoint().y),
                    lastChunkScript.GetLeftBotPoint()
                };

                newAbyssScript.UpdateCollider(points);
            }
            else
            {
                lastChunk.transform.position = pos;
                lastChunkScript.UpdatePoints();
                pos.y = lastChunkScript.GetLeftTopPoint().y + 1;
                GameObject newrock = Instantiate(rocks[Random.Range(0, rocks.Count - 1)], pos, transform.rotation);
            }
            lastChunkScript.AddCoins();

        }
        if (firstChunk != null) firstChunkScript.UpdatePoints();
        if (middleChunk != null) middleChunkScript.UpdatePoints();
        if (lastChunk != null) lastChunkScript.UpdatePoints();
    }

    void Start()
    {
        playerScript = playerObject.GetComponent<Player>();

        SpawnChunk();
        SpawnChunk();
        SpawnChunk();
    }

    void Update()
    {
        playerPosition = playerScript.curPos;
        if (playerPosition.x > lastChunkScript.GetLeftTopPoint().x)
        {
            SpawnChunk();
        }
    }
    public void ClearMap()
    {
        if (firstChunk != null) Destroy(firstChunk);
        if (middleChunk != null) Destroy(middleChunk);
        if (lastChunk != null) Destroy(lastChunk);

        firstChunk = middleChunk = lastChunk = null;

        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
        for (int i = 0; i < rocks.Length; i++)
        {
            Destroy(rocks[i]);
        }
        GameObject[] abysses = GameObject.FindGameObjectsWithTag("Abyss");
        for (int i = 0; i < abysses.Length; i++)
        {
            Destroy(abysses[i]);
        }
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        for (int i = 0; i < coins.Length; i++)
        {
            Destroy(coins[i]);
        }
    }
}