using UnityEngine;
using System.Collections;

public class TableControl : MonoBehaviour
{
    /**
     * <summary>桌子预制件</summary>
     */
    public GameObject[] TablePrefabs;

    /**
     * <summary>创建一个桌子</summary>
     * 
     */
    public void Create(GameObject ground)
    {
        // 检测是否是需要生成 桌子
        if (!RandomUtils.isWithinRatioOfPrecent(UserStorage.Get().ProbabilityOfTable))
        {
            // 不需要则不生成
            return;
        }
        int randomIndex = RandomUtils.GetIndex<GameObject>(TablePrefabs);
        GameObject tablePrefab = TablePrefabs[randomIndex];
        GameObject table = Instantiate(tablePrefab, ground.transform);
        Vector3 pos = table.transform.localPosition;
        pos.x = 0;
        pos.y = randomIndex == 0 ? 0.6f : 0.9f;
        table.transform.localPosition = pos;
    }
}

