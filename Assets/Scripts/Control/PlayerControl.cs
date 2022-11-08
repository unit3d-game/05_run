using UnityEngine;

public class PlayerControl : BaseNotificationBehaviour
{


    private const float InvincibleTimeDuration = 3f;

    private Rigidbody2D rbody;

    private Animator animator;

    private int jumpStep = 2;

    private float speed;


    private float invincibleTime = 0;

    public override void Awake()
    {
        base.Awake();
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void setHide(bool value)
    {
        if (value)
        {
            invincibleTime = InvincibleTimeDuration;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.material.color;
        color.a = value ? 0.6f : 1f;
        spriteRenderer.material.color = color;
    }

    private void FixedUpdate()
    {
        if (invincibleTime == 0)
        {
            return;
        }

        invincibleTime -= Time.deltaTime;
        if (invincibleTime <= 0)
        {
            setHide(false);
        }
    }


    [Subscribe(Const.Notification.PlayerDie)]
    public void StartAgain()
    {
        // 重置状态
        jumpStep = 2;
        animator.SetInteger("state", 0);
        // 设置 position
        Vector3 pos = transform.position;
        pos.x = 0;
        pos.y = 1.2f;
        transform.position = pos;
        setHide(true);
    }

    void Update()
    {
        // 水平方向
        float hor = Input.GetAxis(Const.Axis.Horizontal);
        // 是否jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpStep > 0)
        {
            Jump();
        }
        setSpeed(hor);
        if (speed == 0)
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * speed;
        transform.position = pos;
    }

    private void setSpeed(float hor)
    {
        if (hor == 0)
        {
            speed = 0;
            transform.rotation = Quaternion.identity;
        }
        else if (hor > 0)
        {
            speed = 1;
            transform.rotation = Quaternion.identity;
        }
        else if (speed != -2)
        {
            speed = -2;
            transform.Rotate(new Vector3(0, -180, 0));
        }

    }


    public void Jump()
    {
        // 向上一个力
        rbody.AddForce(Vector2.up * 200);
        animator.SetInteger("state", 1);
        AudioManager.Play("跳");
        jumpStep--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Const.Tag.Ground)
        {
            jumpStep = 2;
            animator.SetInteger("state", 0);
        }

        else if (collision.gameObject.tag == Const.Tag.Die)
        {
            // 如果还是隐身状态，忽略碰触死亡
            if (invincibleTime > 0)
            {
                return;
            }
            UserStorage.Die();
        }
    }
}
