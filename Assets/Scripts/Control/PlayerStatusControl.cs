using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusControl : MonoBehaviour
{
    private TMP_Text level;

    private TMP_Text score;

    private Text life2;

    private GameObject gameOver;

    private readonly static string[] stars = { "", "❤️", "❤️ ❤️", "❤️ ❤️ ❤️" };

    private void Awake()
    {
        PostNotification.Register(this);
        UserGameData userGameData = UserStorage.Get();
        level = transform.Find("Level").GetComponent<TMP_Text>();
        score = transform.Find("Score").GetComponent<TMP_Text>();
        life2 = transform.Find("Life2").GetComponent<Text>();
        Debug.Log($"life num is {userGameData.lifeNum}");
        life2.text = stars[userGameData.lifeNum];
        score.text = $"{userGameData.totalScore}";
        level.text = $"{userGameData.level}";
        gameOver = transform.Find("GameOver").gameObject;
    }

    private void OnDestroy()
    {
        PostNotification.UnRegister(this);
        Debug.Log("player status destroy.");
    }

    [Subscribe(Const.Notification.PlayerDie)]
    private void PlayerDie()
    {
        gameOver.SetActive(true);

        life2.text = stars[UserStorage.Get().lifeNum];

    }

    [Subscribe(Const.Notification.PlayerRevive)]
    private void PlayerRevive()
    {
        gameOver.SetActive(false);
    }

    [Subscribe(Const.Notification.PassedLevel)]
    private void PassedLevel(MessagePayload<int> payload)
    {
        level.text = $"{UserStorage.Get().level}";
    }


    [Subscribe(Const.Notification.EatedCoin)]
    private void EatedCoin(MessagePayload<int> payload)
    {
        score.text = $"{UserStorage.Get().totalScore}";
    }
}

