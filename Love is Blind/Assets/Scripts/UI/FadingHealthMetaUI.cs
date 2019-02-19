using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FadingHealthMetaUI : MonoBehaviour
{
    public FogOfWarSystem fogOfWarSystem;
    public Color fogNormalColor = Color.black;
    public Color fogHealthTint = Color.red;
    public PostProcessVolume ppVolume;
    [MinMax(0f, 1f)]
    public Vector2 minMaxPercentage;

    public void OnHealthChanged(float healthPercentage)
    {
        var invertPercentage = 1 - healthPercentage;
        if (invertPercentage > 0)
            invertPercentage = MathUtilities.GetValue(invertPercentage, minMaxPercentage.x, minMaxPercentage.y); 

        fogOfWarSystem.SetFogColor(Color.Lerp(fogNormalColor, fogHealthTint, invertPercentage));
        ppVolume.weight = invertPercentage;
    }
}
