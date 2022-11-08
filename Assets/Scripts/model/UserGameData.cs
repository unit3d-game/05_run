using System;

/**
 * 用户游戏数据，JSON 序列化
 */
public class UserGameData
{
    /**
     * <summary>当前游戏等级</summary>
     */
    public int Level;

    /**
     * <summary>土地间最大随机间距</summary>
     */
    public float OffsetX;

    /**
     * <summary>敌人出现的概率[0,100]</summary>
     */
    public int ProbabilityOfEnemy;

    /**
     * <summary>桌子出现的概率[0,100]</summary>
     */
    public int ProbabilityOfTable;

    /**
     * <summary>当前积分</summary>
     */
    public int TotalScore;

    /**
     * <summary>剩余生命条数，初始化3条</summary>
     */
    public int LifeNum;

}

