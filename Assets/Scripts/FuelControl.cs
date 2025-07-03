using UnityEngine;
using UnityEngine.UI;

public class FuelControl : MonoBehaviour
{
    public static FuelControl Instance;

    [SerializeField] private Image fuelImage;
    [SerializeField] private float maxFuelAmount = 100f;
    [SerializeField, Range(0.1f, 5f)] private float fuelDrainSpeed = 1f;
    [SerializeField] private Gradient fuelGradient;

    private float curFuelAmount;

    void Start()
    {
        curFuelAmount = maxFuelAmount;
        UpdateUI();
    }

    void Update()
    {
        curFuelAmount -= Time.fixedDeltaTime * fuelDrainSpeed;
        UpdateUI();
    }

    private void UpdateUI()
    {
        fuelImage.fillAmount = (curFuelAmount / maxFuelAmount);
        fuelImage.color = fuelGradient.Evaluate(fuelImage.fillAmount);
    }
}
