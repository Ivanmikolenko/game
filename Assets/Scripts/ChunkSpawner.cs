using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunk;
    public GameObject rock;
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
            }
            else
            {
                lastChunk.transform.position = pos;
                lastChunkScript.UpdatePoints();
                pos.y = lastChunkScript.GetLeftTopPoint().y + 1;
                GameObject newrock = Instantiate(rock, pos, transform.rotation);
            }

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
    public void ClearAllChunks()
    {
        if (firstChunk != null) Destroy(firstChunk);
        if (middleChunk != null) Destroy(middleChunk);
        if (lastChunk != null) Destroy(lastChunk);

        firstChunk = middleChunk = lastChunk = null;
    }
}