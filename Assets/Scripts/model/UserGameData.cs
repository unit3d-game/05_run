using System;

/**
 * 用户游戏数据，JSON 序列化
 */
public class UserGameData
{
    /**
     * <summary>当前游戏等级</summary>
     */
    public int level;

    /**
     * <summary>土地间最大随机间距</summary>
     */
    public float offsetX;

    /**
     * <summary>敌人出现的概率[0,100]</summary>
     */
    public int probabilityOfEnemy;

    /**
     * <summary>凳子出现的概率[0,100]</summary>
     */
    public int probabilityOfStool;

    /**
     * <summary>当前积分</summary>
     */
    public int totalScore;

    /**
     * <summary>剩余生命条数，初始化3条</summary>
     */
    public int lifeNum;

}

