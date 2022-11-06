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

    public void CreateGroup(GameObject ground)
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


    private void CreateOne(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(UnityEngine.Random.Range(0f, goo.RenderWidth), UnityEngine.Random.Range(0f, 0.8f));
        CreateACoin(CoinPrefabs[UnityEngine.Random.Range(0, CoinPrefabs.Length)], ground, vector2);
    }

    private void CreateVGroup(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(UnityEngine.Random.Range(0f, 1f), 0f);
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x - 0.6f, vector2.y + 0.8f));
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x - 0.3f, vector2.y + 0.4f));
        CreateACoin(CoinPrefabs[0], ground, vector2);
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + 0.3f, vector2.y + 0.4f));
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + 0.6f, vector2.y + 0.8f));
    }

    private void CreateACoin(GameObject coinPrefab, GameObject ground, Vector2 position)
    {
        GameObject coin = Instantiate(coinPrefab, ground.transform);
        coin.transform.localScale = Vector3.one;
        Vector3 pos = coin.transform.position;
        pos.y = position.y;
        pos.x = position.x;
        coin.transform.position = pos;
    }


    private void CreateNGroup(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(UnityEngine.Random.Range(0f, 1f), 0.8f);
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x - 0.6f, vector2.y - 0.8f));
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x - 0.3f, vector2.y - 0.4f));
        CreateACoin(CoinPrefabs[1], ground, vector2);
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + 0.3f, vector2.y - 0.4f));
        CreateACoin(CoinPrefabs[0], ground, new Vector2(vector2.x + 0.6f, vector2.y - 0.8f));
    }

    private void CreateLineGroup(GameObject ground)
    {
        GroundObject goo = ground.GetComponent<GroundObject>();
        Vector2 vector2 = new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
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

