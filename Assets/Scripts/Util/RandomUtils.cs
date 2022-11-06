using System;
public class RandomUtils
{
    /**
     * <summary>是否在一定比率内，比率是0-100</summary>
     * <param name="precent">比率范围</param>
     */
    public static bool isWithinRatioOfPrecent(int precent)
    {
        if (precent >= 100)
        {
            return true;
        }
        return UnityEngine.Random.Range(0, 100) <= precent;
    }
}

