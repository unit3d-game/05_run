using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

/**
 * <summary>用户存储</summary>
 * 
 */
public class UserStorage
{
    public readonly UserGameData userGameData;

    // 单例对象
    public readonly static UserStorage Instance = new UserStorage();


    public UserStorage()
    {
        // 加载数据
        UserGameData data = JSONUtils.Load<UserGameData>();
        if (data == null || data.Level == 0)
        {
            data = new UserGameData();
            data.Level = 1;
            data.OffsetX = 0.5f;
            data.ProbabilityOfEnemy = 10;
            data.TotalScore = 0;
            data.ProbabilityOfTable = 10;
            data.LifeNum = 3;
        }
        userGameData = data;
        Debug.Log($"User data is {JSONUtils.toJSON(data)}");
    }

    /**
     * <summary>获取数据</summary>
     * <returns>返回 数据</returns>
     */
    public static UserGameData Get()
    {

        return Instance.userGameData;
    }

    /**
     * <summary>升级</summary>
     * 
     */
    private static void UpgradeLevel()
    {
        Instance.userGameData.Level += 1;
        int level = Instance.userGameData.Level;
        Instance.userGameData.OffsetX = 0.5f * (level / 4);
        Instance.userGameData.ProbabilityOfEnemy = 10 + 10 * (level / 4);
        Instance.userGameData.ProbabilityOfTable = 10 + 10 * (level / 4);
        // 发送升级事件
        JSONUtils.Save<UserGameData>(Instance.userGameData);
        PostNotification.Post(Const.Notification.PassedLevel, Instance);
    }

    /**
     * <summary>增加积分</summary>
     * <param name="score">添加的积分数</param>
     */
    public static void AddScore(int score)
    {
        Instance.userGameData.TotalScore += score;
        AudioManager.Play("金币");

        // 检查是否升级
        int maxScore = (2 << Instance.userGameData.Level) * 100;
        // 是否需要升级
        if (maxScore <= Instance.userGameData.TotalScore)
        {
            // 升级
            UpgradeLevel();
        }
        else
        {
            JSONUtils.Save<UserGameData>(Instance.userGameData);
            // 发送积分
            PostNotification.Post(Const.Notification.EatedCoin, Instance, score);
        }
    }

    /**
     * <summary>角色死亡</summary>
     */
    public static void Die()
    {
        if (Instance.userGameData.LifeNum < 1)
        {
            return;
        }
        AudioManager.Play("Boss死了");
        Instance.userGameData.LifeNum--;
        JSONUtils.Save<UserGameData>(Instance.userGameData);

        if (Instance.userGameData.LifeNum > 0)
        {
            PostNotification.Post(Const.Notification.PlayerDie, Instance);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            PostNotification.Post(Const.Notification.GameOver, Instance);
        }
    }

    /**
     * <summary>游戏是否结束</summary>
     * <returns>true 已经结束，false 未结束</returns>
     */
    public static bool IsGameOver()
    {

        return Instance.userGameData.LifeNum <= 0;
    }

    private void restart()
    {

        // 加载数据
        userGameData.Level = 1;
        userGameData.OffsetX = 0.5f;
        userGameData.ProbabilityOfEnemy = 10;
        userGameData.TotalScore = 0;
        userGameData.ProbabilityOfTable = 10;
        userGameData.LifeNum = 3;
        JSONUtils.Save<UserGameData>(Instance.userGameData);
    }

    /**
     * <summary>重新开始</summary>
     * 
     */
    public static void Restart()
    {

        Instance.restart();
    }
}

