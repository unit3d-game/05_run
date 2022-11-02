using System;
using System.Numerics;
using UnityEngine;

public partial class Const
{

    public static class Config
    {
        /**
         * <summary>地面随机宽度范围</summary> 
         */
        public readonly static UnityEngine.Vector2 WidthRangeOfGround = new UnityEngine.Vector2(4f, 6.72f);

        /**
         * <summary>地面X轴最大随机范围0-X</summary>
         */
        public const float MaxXOfGroundRandom = 0.5f;


        /**
         * <summary>地面Y轴最大随机范围0-X</summary>
         */
        public const float MaxYOfGroundRandom = 0.3f;

        /**
         * <summary>最大频幕宽度</summary>
         */
        public const float MaxWidthOfScreen = 7.2f;
    }

}

