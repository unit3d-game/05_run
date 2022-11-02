using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusControl : MonoBehaviour
{
    private Text level;

    private Text score;

    private Text life2;

    private GameObject gameOver;

    private int totalScore = 0;

    private int currentLevel = 1;

    private int lifeNum = 3;

    private readonly static string[] stars = { "", "❤️", "❤️ ❤️", "❤️ ❤️ ❤️" };

    void Start()
    {
        level = transform.Find("Level").GetComponent<Text>();
        score = transform.Find("Score").GetComponent<Text>();
        life2 = transform.Find("Life2").GetComponent<Text>();
        gameOver = transform.Find("GameOver").gameObject;
    }

    private void Awake()
    {
        PostNotification.Register(Const.Notification.EatedCoin, EatedCoin);
        PostNotification.Register(Const.Notification.PassedLevel, PassedLevel);
        PostNotification.Register(Const.Notification.PlayerDie, PlayerDie);
        PostNotification.Register(Const.Notification.PlayerRevive, PlayerRevive);
    }

    private void PlayerDie(MessagePayload payload)
    {
        gameOver.SetActive(true);

        if (lifeNum > 0)
        {
            lifeNum--;
            life2.text = stars[lifeNum];
        }

    }

    private void PlayerRevive(MessagePayload payload)
    {
        gameOver.SetActive(false);
    }

    private void PassedLevel(MessagePayload payload)
    {
        currentLevel = (int)payload.data;
        level.text = $"{currentLevel}";
    }

    private void EatedCoin(MessagePayload payload)
    {
        totalScore += (int)payload.data;
        score.text = $"{totalScore}";
    }

    private void OnDestroy()
    {

        PostNotification.UnRegister(this);
    }
}

