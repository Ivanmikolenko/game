using UnityEngine;
using UnityEngine.UI;

public class FuelControl : MonoBehaviour
{
    [SerializeField] private Image fuelImage;
    [SerializeField] public float maxFuelAmount = 100f;
    [SerializeField, Range(0.1f, 5f)] private float fuelDrainSpeed = 1f;
    [SerializeField] private Gradient fuelGradient;

    public float curFuelAmount;

    void Awake()
    {
        curFuelAmount = maxFuelAmount;
        UpdateUI();
    }

    void Enable()
    {
        curFuelAmount = maxFuelAmount;
        UpdateUI();
    }

    void Update()
    {
        curFuelAmount -= Time.fixedDeltaTime * fuelDrainSpeed;
        UpdateUI();
        if (curFuelAmount <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    private void UpdateUI()
    {
        fuelImage.fillAmount = (curFuelAmount / maxFuelAmount);
        fuelImage.color = fuelGradient.Evaluate(fuelImage.fillAmount);
    }
}
