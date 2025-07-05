using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    [SerializeField] private GameObject chunkSpawner;
    [SerializeField] private GameObject fuelControl;
    PlayerControls playerControls;
    private Player playerScript;

    public void Awake()
    {
        if (instance == null) instance = this;
        playerScript = player.GetComponent<Player>();
        player.SetActive(false);
        fuelControl.SetActive(false);
        chunkSpawner.SetActive(false);
    }

    public void GameOver()
    {
        cameraSwitcher.SwitchToMenuCamera();
        chunkSpawner.GetComponent<ChunkSpawner>().ClearMap();
        fuelControl.GetComponent<FuelControl>().curFuelAmount = fuelControl.GetComponent<FuelControl>().maxFuelAmount;

        playerControls = player.GetComponent<PlayerControls>();
        playerControls.Disable();
        player.SetActive(false);
        fuelControl.SetActive(false);
        chunkSpawner.SetActive(false);
    }

    public void Restart()
    {
        player.SetActive(true);
        player.transform.position = new Vector2(2+2f, 2+3f);
        playerScript.bikeObj.transform.position = new Vector2(2 + 0.83776f, 2 + 0.3505599f);
        playerScript.frontWheelObj.transform.position = new Vector2(2 + 1.58576f, 2 - 0.13644f);
        playerScript.rearWheelObj.transform.position = new Vector2(2 + 0.09575987f, 2 - 0.1214399f);
        playerScript.bikeObj.transform.rotation = Quaternion.identity;
        playerScript.frontWheelObj.transform.rotation = Quaternion.identity;
        playerScript.rearWheelObj.transform.rotation = Quaternion.identity;
        playerScript.frontWheelObj.GetComponent<Rigidbody2D>().angularVelocity = 0;
        playerScript.rearWheelObj.GetComponent<Rigidbody2D>().angularVelocity = 0;
        fuelControl.SetActive(true);
        chunkSpawner.SetActive(true);
    }
}
