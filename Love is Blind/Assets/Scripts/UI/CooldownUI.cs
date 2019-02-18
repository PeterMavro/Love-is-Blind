using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Slider cooldownSlider;

    public void UpdateCooldown(float value)
    {
        cooldownSlider.value = value;
    }
}