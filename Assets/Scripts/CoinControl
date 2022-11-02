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
        //int random = UnityEngine.Random.Range(0, 100);
        //foreach (CoinGroup cg in groups)
        //{
        //    if (cg.probability > random)
        //    {
        //        cg.action(ground);
        //        break;
        //    }
        //}
        CreateOne(ground);
    }


    private void CreateOne(GameObject ground)
    {
        GameObject coinPrefab = CoinPrefabs[UnityEngine.Random.Range(0, CoinPrefabs.Length)];
        GameObject coin = Instantiate(coinPrefab, ground.transform);
        Vector3 pos = coin.transform.position;
        GroundObject goo = ground.GetComponent<GroundObject>();
        pos.y = UnityEngine.Random.Range(0f, 2f);
        pos.x = UnityEngine.Random.Range(0f, goo.renderWidth);
        coin.transform.position = pos;
    }

    private void CreateVGroup(GameObject ground)
    {


    }

    private void CreateNGroup(GameObject ground) { }

    private void CreateLineGroup(GameObject ground) { }
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

