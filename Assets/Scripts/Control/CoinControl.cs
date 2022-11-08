using System;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    // 金币队列
    public GameObject[] CoinPrefabs;

    private CoinGroup[] groups;


    private void Awake()
    {
        groups = new CoinGroup[4];
        groups[0] = new CoinGroup(60, CreateOne);
        groups[1] = new CoinGroup(80, CreateVGroup);
        groups[2] = new CoinGroup(90, CreateNGroup);
        groups[3] = new CoinGroup(100, CreateLineGroup);
    }

    public void Create(GameObject ground)
    {
        // 随机coin 类型
        int random = UnityEngine.Random.Range(0, 100);
        foreach (CoinGroup cg in groups)
        {
            if (cg.probability > random)
            {
                cg.action(ground);
                break;
            }
        }
    }

    private float getRandomX(GroundObject ground)
    {
        return UnityEngine.Random.Range(-ground.RenderWidth / 2 + 0.2f, ground.RenderWidth / 2 - 0.4f);
    }

    private void CreateOne(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(getRandomX(goo), UnityEngine.Random.Range(1f, 2f));
        CreateACoin(CoinPrefabs[UnityEngine.Random.Range(0, CoinPrefabs.Length)], ground, vector2);
    }

    private void CreateVGroup(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(getRandomX(goo), 1f);
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x - 0.6f, vector2.y + 0.8f));
        CreateACoin(CoinPrefabs[1], ground, new Vector2(vector2.x - 0.3f, vector2.y + 0.4f));
        CreateACoin(CoinPrefabs[2], ground, vector2);
        CreateACoin(CoinPrefabs[1], ground, new Vector2(vector2.x + 0.3f, vector2.y + 0.4f));
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + 0.6f, vector2.y + 0.8f));
    }

    private void CreateACoin(GameObject coinPrefab, GameObject ground, Vector2 position)
    {
        // 创建一个金币物体，父物体是 ground
        GameObject coin = Instantiate(coinPrefab, ground.transform);
        // 设置金币坐标
        Vector3 pos = coin.transform.localPosition;
        pos.y = position.y;
        pos.x = position.x;
        coin.transform.localPosition = pos;
        // 设置激活状态
        coin.SetActive(true);
        // 设置显示序列
        coin.GetComponent<SpriteRenderer>().sortingOrder = 5;
    }


    private void CreateNGroup(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(getRandomX(goo), 1.5f);
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x - 0.6f, vector2.y - 0.8f));
        CreateACoin(CoinPrefabs[1], ground, new Vector2(vector2.x - 0.3f, vector2.y - 0.4f));
        CreateACoin(CoinPrefabs[2], ground, vector2);
        CreateACoin(CoinPrefabs[1], ground, new Vector2(vector2.x + 0.3f, vector2.y - 0.4f));
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + 0.6f, vector2.y - 0.8f));
    }

    private void CreateLineGroup(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(getRandomX(goo), UnityEngine.Random.Range(0f, 1f));
        int size = UnityEngine.Random.Range(1, 5);
        for (var i = 0; i < size; i++)
        {
            CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + i * 0.3f, vector2.y));
        }
    }
}

public class CoinGroup
{

    public readonly int probability;

    public readonly Action<GameObject> action;

    public CoinGroup(int probability, Action<GameObject> action)
    {
        this.probability = probability;
        this.action = action;
    }
}

