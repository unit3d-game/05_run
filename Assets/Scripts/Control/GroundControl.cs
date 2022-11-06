using UnityEngine;

public class GroundControl : MonoBehaviour
{

    // 地面预制对象
    public GameObject GroundPrefab;

    // 地面移动速度
    public float Speed;

    private GroundObject groundOfLast;


    private bool isDied = false;

    private CoinControl coinControl;

    private EnemyControl enemyControl;


    // 创建一个随机地面，随机Y轴位置，随机宽度
    private void createGround(float ox)
    {
        GameObject ground = Instantiate(GroundPrefab, transform);

        float offsetX = groundOfLast == null ? 0 : (groundOfLast.getXOfBounds() + Random.Range(0.1f, Const.Config.MaxXOfGroundRandom));
        offsetX -= ox;
        groundOfLast = ground.GetComponent<GroundObject>();
        groundOfLast.Init(offsetX, Random.Range(-1.1f, -1.6f));
        coinControl = GetComponent<CoinControl>();
        enemyControl = GetComponent<EnemyControl>();
        coinControl.CreateGroup(ground);
        enemyControl.CreateEnemy(ground);
        if (next())
        {
            createGround(ox);
        }
    }

    private void Awake()
    {
        PostNotification.Register(this);
    }


    private void OnDestroy()
    {
        PostNotification.UnRegister(this);
    }

    [Subscribe(Const.Notification.PlayerDie)]
    private void PlayerDie()
    {
        isDied = true;
        Debug.Log("Receive a message is player died.");
    }

    private void Start()
    {
        createGround(0);
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
