using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusControl : BaseNotificationBehaviour
{


    private TMP_Text level;

    private TMP_Text score;

    private Text life2;

    private GameObject nextLevel;

    private TMP_Text nextLevelTime;

    private GameObject resurrectionCountdown;

    private TMP_Text resurrectionCountdownTimeText;

    private float levelTime;

    private float resurrectionCountdownTime;

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
        life2.text = stars[userGameData.LifeNum];
        score.text = $"{userGameData.TotalScore}";
        level.text = $"{userGameData.Level}";
        nextLevel = transform.Find("NextLevel").gameObject;
        nextLevelTime = UnityUtils.RequiredGetComponent<TMP_Text>(nextLevel.transform, "Time");
        resurrectionCountdown = UnityUtils.FindByName(transform, "ResurrectionCountdown");
        resurrectionCountdownTimeText = UnityUtils.RequiredGetComponent<TMP_Text>(resurrectionCountdown.transform, "Time");
    }

    [Subscribe(Const.Notification.GameRestart)]
    private void GameRestart()
    {
        nextLevel.SetActive(false);
    }


    [Subscribe(Const.Notification.PlayerDie)]
    private void PlayerDie()
    {
        life2.text = stars[UserStorage.Get().LifeNum];
        UserStorage.SetStop();
        resurrectionCountdownTime = 3f;
        resurrectionCountdown.SetActive(true);
    }

    [Subscribe(Const.Notification.PassedLevel)]
    private void PassedLevel()
    {
        level.text = $"{UserStorage.Get().Level}";
        nextLevel.SetActive(true);
        TMP_Text text = UnityUtils.RequiredGetComponent<TMP_Text>(nextLevel.transform, "Level");
        text.SetText($"LEVEL {UserStorage.Get().Level}");
        levelTime = 5;
        EatedCoin();
    }

    private void FixedUpdate()
    {
        showResurrectionTime();
        showLevelTime();
    }

    private void showResurrectionTime()
    {

        if (!resurrectionCountdown.activeSelf)
        {
            return;
        }

        if (resurrectionCountdownTime > 0)
        {
            resurrectionCountdownTime -= Time.deltaTime;
            resurrectionCountdownTime = resurrectionCountdownTime >= 0 ? resurrectionCountdownTime : 0;
            resurrectionCountdownTimeText.text = $"{1 + (int)resurrectionCountdownTime}";
            if (resurrectionCountdownTime == 0)
            {
                UserStorage.SetContinue();
                resurrectionCountdown.SetActive(false);
                PostNotification.Post(Const.Notification.PlayerResurrection, this);
            }
        }
    }

    private void showLevelTime()
    {
        if (!nextLevel.activeSelf)
        {
            return;
        }
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
            levelTime = levelTime >= 0 ? levelTime : 0;
            nextLevelTime.text = $"{1 + (int)levelTime}";
            if (levelTime == 0)
            {
                UserStorage.SetContinue();
                GameRestart();
            }
        }

    }


    [Subscribe(Const.Notification.EatedCoin)]
    private void EatedCoin()
    {
        score.text = $"{UserStorage.Get().TotalScore}";
    }
}

