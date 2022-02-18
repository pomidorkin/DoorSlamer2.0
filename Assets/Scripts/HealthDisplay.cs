using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Building mainBuilding;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = mainBuilding.GetHealth();
        slider.value = mainBuilding.GetHealth();
    }

    public void DecreaseValue(int amount)
    {
        if (slider.value > 0)
        {
            slider.value = amount;
        }
    }

    public void SetDisplay(int amount)
    {
        slider.value = amount;
    }
}
