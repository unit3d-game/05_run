using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusControl : BaseNotificationBehaviour
{
    private TMP_Text level;

    private TMP_Text score;

    private Text life2;

    private GameObject gameOver;

    private readonly static string[] stars = { "", "❤️", "❤️ ❤️", "❤️ ❤️ ❤️" };

    public override void Awake()
    {
        // 这里调用下父类原始方法
        base.Awake();
        PostNotification.Register(this);
        UserGameData userGameData = UserStorage.Get();
        level = transform.Find("Level").GetComponent<TMP_Text>();
        score = transform.Find("Score").GetComponent<TMP_Text>();
        life2 = transform.Find("Life2").GetComponent<Text>();
        Debug.Log($"life num is {userGameData.LifeNum}");
        life2.text = stars[userGameData.LifeNum];
        score.text = $"{userGameData.TotalScore}";
        level.text = $"{userGameData.Level}";
        gameOver = transform.Find("GameOver").gameObject;
    }

    [Subscribe(Const.Notification.GameOver)]
    private void PlayerDie()
    {
        gameOver.SetActive(true);

        life2.text = stars[UserStorage.Get().LifeNum];
    }

    [Subscribe(Const.Notification.GameRestart)]
    private void GameRestart()
    {
        gameOver.SetActive(false);
    }

    [Subscribe(Const.Notification.PassedLevel)]
    private void PassedLevel()
    {
        level.text = $"{UserStorage.Get().Level}";
    }


    [Subscribe(Const.Notification.EatedCoin)]
    private void EatedCoin()
    {
        score.text = $"{UserStorage.Get().TotalScore}";
    }
}

