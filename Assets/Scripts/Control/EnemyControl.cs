using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    // 敌人
    public GameObject EnemyPrefab;

    public void CreateEnemy(GameObject ground)
    {
        if (!RandomUtils.isWithinRatioOfPrecent(UserStorage.Get().probabilityOfEnemy))
        {
            return;
        }
        GameObject enemy = Instantiate(EnemyPrefab, ground.transform);
        GroundObject goo = ground.GetComponent<GroundObject>();
        SpriteRenderer render = enemy.GetComponent<SpriteRenderer>();
        float width = render.bounds.size.y + 0.1f;
        Vector3 pos = enemy.transform.localPosition;
        pos.x = UnityEngine.Random.Range(-goo.RenderWidth / 2, goo.RenderWidth / 2);
        pos.y = 1f;
        enemy.transform.localPosition = pos;
        render.sortingOrder = 5;
    }

}
