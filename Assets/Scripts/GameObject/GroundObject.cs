using UnityEngine;

public class GroundObject : MonoBehaviour
{
    // 宽度
    public float RenderWidth { private set; get; }
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider2D;


    /**
     * <summary>初始化组件</summary>
     * <param name="x">x 位置</param>
     * <param name="y">y 位置</param>
     */
    public void Init(float x, float y)
    {

        Transform groundObject = transform.Find("GroundObject");

        spriteRenderer = groundObject.GetComponent<SpriteRenderer>();
        boxCollider2D = groundObject.GetComponent<BoxCollider2D>();
        // 随机一个 宽度
        RenderWidth = Random.Range(Const.Config.WidthRangeOfGround.x, Const.Config.WidthRangeOfGround.y);
        // 横向拉伸
        float scaleX = RenderWidth / spriteRenderer.sprite.bounds.size.x;
        //Debug.Log($"p[{x},{y}],x{scaleX}, bs {spriteRenderer.sprite.bounds.size.x}");
        // 宽度缩放
        groundObject.localScale = new Vector3(scaleX, 1, 1);
        // 重新计算中心点,
        Vector3 position = transform.position;
        // 位置
        position.x = x;
        position.y = y;
        transform.position = position;
    }

    public float getXOfBounds()
    {
        return transform.position.x + RenderWidth;
    }


    private void Update()
    {
        if (transform.position.x <= -Const.Config.MaxWidthOfScreen)
        {
            Destroy(gameObject);
        }
    }
}

