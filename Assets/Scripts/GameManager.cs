using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    [SerializeField] private GameObject chunkSpawner;
    [SerializeField] private GameObject fuelControl;

    public void Awake()
    {
        if (instance == null) instance = this;

        player.SetActive(false);
        fuelControl.SetActive(false);
        chunkSpawner.SetActive(false);
    }

    public void GameOver()
    {
        cameraSwitcher.SwitchToMenuCamera();
        chunkSpawner.GetComponent<ChunkSpawner>().ClearMap();
        player.transform.position = new Vector2(2f, 3f);

        player.SetActive(false);
        fuelControl.SetActive(false);
        chunkSpawner.SetActive(false);
    }

    public void Restart()
    {
        player.SetActive(true);
        fuelControl.SetActive(true);
        chunkSpawner.SetActive(true);
    }
}
