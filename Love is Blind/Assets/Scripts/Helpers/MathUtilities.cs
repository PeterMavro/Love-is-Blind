
public static class MathUtilities
{
    /// <summary>
    /// Get the value that represents the percentage between a minimum and maximum 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public static float GetValue(float percentage, float min, float max)
    {
        return percentage * (max - min) + min;
    }

    /// <summary>
    /// Get the percentage that represents the value between a minimum and maximum 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float GetPercentage(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}
