using TMPro;
using UnityEngine;

public class VarManager : MonoBehaviour
{
    public TMP_Text speedText, rSpeedText, jumpForceText, coinsTotalText, coinsCurText;
    public GameObject player;
    public int coinsTotal, coinsCur;
    private int 
        speedLevel = 0, 
        speedLevelMax = 5, 
        rSpeedLevel = 0, 
        rSpeedLevelMax = 5, 
        jumpForceLevel = 0, 
        jumpForceLevelMax = 5;
    private float speedBase, rSpeedBase, jumpForceBase;
    private Player playerScript;
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        speedBase = playerScript.speed;
        rSpeedBase = playerScript.rotationSpeed;
        jumpForceBase = playerScript.jumpForce;

        UpdateCoinsTotalText();
        UpdateSpeedLevelText();
        UpdateRSpeedLevelText();
        UpdateJumpForceLevelText();
    }
    
    public void BoostSpeed()
    {
        if (speedLevel < speedLevelMax && coinsTotal > 0)
        {
            coinsTotal--;
            speedLevel++;
            playerScript.speed = speedBase + speedBase * 0.025f * speedLevel;
        }
        UpdateCoinsTotalText();
        UpdateSpeedLevelText();
    }
    public void BoostRSpeed()
    {
        if (rSpeedLevel < rSpeedLevelMax && coinsTotal > 0)
        {
            coinsTotal--;
            rSpeedLevel++;
            playerScript.rotationSpeed = rSpeedBase + rSpeedBase * 0.025f * rSpeedLevel;
        }
        UpdateCoinsTotalText();
        UpdateRSpeedLevelText();
    }
    public void BoostJumpForce()
    {
        if (jumpForceLevel < jumpForceLevelMax && coinsTotal > 0)
        {
            coinsTotal--;
            jumpForceLevel++;
            playerScript.rotationSpeed = jumpForceBase + jumpForceBase * 0.025f * jumpForceLevel;
        }
        UpdateCoinsTotalText();
        UpdateJumpForceLevelText();
    }
    public void UpdateCoinsTotal()
    {
        coinsTotal += coinsCur;
        coinsCur = 0;
        UpdateCoinsTotalText();
    }
    public void UpdateSpeedLevelText()
    {
        speedText.text = $"{speedLevel}/{speedLevelMax}";
    }
    public void UpdateRSpeedLevelText()
    {
        rSpeedText.text = $"{rSpeedLevel}/{rSpeedLevelMax}";
    }
    public void UpdateJumpForceLevelText()
    {
        jumpForceText.text = $"{jumpForceLevel}/{jumpForceLevelMax}";
    }

    public void UpdateCoinsCurText()
    {
        coinsCurText.text = $"{coinsCur}";
    }
    public void UpdateCoinsTotalText()
    {
        coinsTotalText.text = $"{coinsTotal}";
    }
}
