using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : BaseNotificationBehaviour
{
    // 背景图片
    public Sprite[] Backgrounds;
    // 游戏主角
    public GameObject PlayerPrefab;

    // 背景移动速度
    public float SpeedOfBackground;

    void Update()
    {
        float moveX = Time.deltaTime * SpeedOfBackground;
        // 移动背景位置
        foreach (Transform tran in transform)
        {
            // 如果是游戏主角则忽略
            if (tran.gameObject.tag == Const.Tag.Player)
            {
                continue;
            }
            Vector3 position = tran.position;
            position.x -= moveX;
            if (tran.gameObject.tag == Const.Tag.Background && position.x < -Const.Config.MaxWidthOfScreen)
            {
                position.x += Const.Config.MaxWidthOfScreen * 2;
                // 获取背景组件，并设置精灵图片
                BackgroundControl bgControl = tran.gameObject.GetComponent<BackgroundControl>();
                bgControl.setSprite(Backgrounds[UnityEngine.Random.Range(0, Backgrounds.Length)]);
            }
            tran.position = position;
        }
    }


    [Subscribe(Const.Notification.PlayerResurrection)]
    public void StartAgain()
    {
        initPlayer();
    }

    void Start()
    {
        initPlayer();
    }

    private void initPlayer()
    {
        Instantiate(PlayerPrefab, transform);
    }
}
