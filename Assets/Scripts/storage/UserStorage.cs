using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UserStorage
{
    public readonly UserGameData userGameData;

    // 单例对象
    public readonly static UserStorage Instance = new UserStorage();


    public UserStorage()
    {
        // 加载数据
        UserGameData data = JSONUtils.Load<UserGameData>(Const.FileName.UserData);
        if (data == null || data.level == 0)
        {
            data = new UserGameData();
            data.level = 1;
            data.offsetX = 0.5f;
            data.probabilityOfEnemy = 10;
            data.totalScore = 0;
            data.probabilityOfStool = 10;
            data.lifeNum = 3;
        }
        userGameData = data;
        JSONUtils.toJSON(data);
    }


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
        Instance.userGameData.level += 1;
        int level = Instance.userGameData.level;
        Instance.userGameData.offsetX = 0.5f * (level / 4);
        Instance.userGameData.probabilityOfEnemy = 10 + 10 * (level / 4);
        Instance.userGameData.probabilityOfStool = 10 + 10 * (level / 4);
        // 发送升级事件
        JSONUtils.Save<UserGameData>(Instance.userGameData, Const.FileName.UserData);
        PostNotification.Post(Const.Notification.PassedLevel, Instance);
    }

    /**
     * <summary>增加积分</summary>
     * <param name="score">添加的积分数</param>
     */
    public static void AddScore(int score)
    {
        Instance.userGameData.totalScore += score;
        AudioManager.Play("金币");

        // 检查是否升级
        int maxScore = (2 << Instance.userGameData.level) * 100;
        // 是否需要升级
        if (maxScore <= Instance.userGameData.totalScore)
        {
            // 升级
            UpgradeLevel();
        }
        else
        {
            JSONUtils.Save<UserGameData>(Instance.userGameData, Const.FileName.UserData);
            // 发送积分
            PostNotification.Post(Const.Notification.EatedCoin, Instance, score);
        }
    }

    /**
     * <summary>角色死亡</summary>
     */
    public static void Die()
    {
        AudioManager.Play("Boss死了");
        Instance.userGameData.lifeNum--;
        JSONUtils.Save<UserGameData>(Instance.userGameData, Const.FileName.UserData);

        PostNotification.Post(Const.Notification.PlayerDie, Instance);
    }
}

