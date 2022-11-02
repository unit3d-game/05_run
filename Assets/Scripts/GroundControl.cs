using UnityEngine;

public class GroundControl : MonoBehaviour
{

    // 地面预制对象
    public GameObject GroundPrefab;

    // 地面移动速度
    public float Speed;

    private GroundObject groundOfLast;


    private bool isDied = false;

    //private CoinControl coinControl;


    // 创建一个随机地面，随机Y轴位置，随机宽度
    private void createGround(float ox)
    {
        GameObject ground = Instantiate(GroundPrefab, transform);
        float offsetX = groundOfLast == null ? -Const.Config.MaxWidthOfScreen / 2 : (groundOfLast.getXOfBounds() + Random.Range(0.1f, Const.Config.MaxXOfGroundRandom));
        offsetX -= ox;
        groundOfLast = ground.GetComponent<GroundObject>();
        groundOfLast.Init(offsetX, Random.Range(-1.1f, -1.6f));
        //coinControl.CreateGroup(ground);
        if (next())
        {
            createGround(ox);
        }
    }

    private void OnDestroy()
    {
        PostNotification.UnRegister(Const.Notification.PlayerDie, PlayerDie);
    }

    private void PlayerDie(MessagePayload payload)
    {
        isDied = true;
        Debug.Log("Receive a message is player died.");
    }

    private void Awake()
    {
        createGround(0);
        PostNotification.Register(Const.Notification.PlayerDie, PlayerDie);
        //coinControl = GetComponent<CoinControl>();
    }

    void Update()
    {
        if (isDied)
        {
            return;
        }
        // 本次移动的位置
        float speed = Time.deltaTime * Speed;
        // 移动背景位置
        foreach (Transform tran in transform)
        {
            Vector3 position = tran.position;
            position.x -= speed;
            tran.position = position;
        }
        if (next())
        {
            createGround(speed);
        }
    }

    private bool next()
    {
        if (groundOfLast == null)
        {
            return true;
        }
        return Const.Config.MaxWidthOfScreen - groundOfLast.getXOfBounds() > 0;
    }
}
