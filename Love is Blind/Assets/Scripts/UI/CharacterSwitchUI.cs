using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitchUI : MonoBehaviour
{
    public Slider cooldownSlider;

    public void SetupCooldown(float minValue, float maxValue)
    {
        cooldownSlider.minValue = minValue;
        cooldownSlider.maxValue = maxValue;
    }

    public void UpdateCooldown(float value)
    {
        cooldownSlider.value = value;
    }
}