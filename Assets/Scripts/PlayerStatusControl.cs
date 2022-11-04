using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusControl : MonoBehaviour
{
    private TMP_Text level;

    private TMP_Text score;

    private Text life2;

    private GameObject gameOver;

    private int totalScore = 0;

    private int currentLevel = 1;

    private int lifeNum = 3;

    private readonly static string[] stars = { "", "❤️", "❤️ ❤️", "❤️ ❤️ ❤️" };

    private void Awake()
    {
        PostNotification.Register(this);
        level = transform.Find("Level").GetComponent<TMP_Text>();
        score = transform.Find("Score").GetComponent<TMP_Text>();
        life2 = transform.Find("Life2").GetComponent<Text>();
        life2.text = stars[lifeNum];
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

        if (lifeNum > 0)
        {
            lifeNum--;
            life2.text = stars[lifeNum];
        }

    }

    [Subscribe(Const.Notification.PlayerRevive)]
    private void PlayerRevive()
    {
        gameOver.SetActive(false);
    }

    [Subscribe(Const.Notification.PassedLevel)]
    private void PassedLevel(MessagePayload<int> payload)
    {
        currentLevel = payload.data;
        level.text = $"{currentLevel}";
    }


    [Subscribe(Const.Notification.EatedCoin)]
    private void EatedCoin(MessagePayload<int> payload)
    {
        totalScore += payload.data;
        score.text = $"{totalScore}";
    }


}

