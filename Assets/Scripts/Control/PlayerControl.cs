using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody2D rbody;

    private Animator animator;

    private int jumpStep = 2;

    private float speed;

    private bool isDie = false;

    private void Awake()
    {
        PostNotification.Register(this);
    }

    [Subscribe(Const.Notification.PlayerRevive)]
    public void IsRevive()
    {
        isDie = false;
        jumpStep = 2;
        animator.SetInteger("state", 0);
    }

    private void OnDestroy()
    {
        PostNotification.UnRegister(this);
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDie)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpStep > 0)
        {
            Jump();
        }

        bool isDir = false;

        if (Input.GetKeyDown(KeyCode.A))
        {
            speed = speed == 0 ? -2 : -1;
            isDir = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            speed = speed == 0 ? 1 : -1;
            isDir = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            speed += 2;
            isDir = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            speed -= 1;
            isDir = true;
        }

        if (isDir)
        {
            if (speed >= 0)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.Rotate(new Vector3(0, -180, 0));
            }
        }

        if (speed != 0)
        {
            Vector3 pos = transform.position;
            pos.x += Time.deltaTime * speed;
            transform.position = pos;
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
            animator.SetInteger("state", 2);
            AudioManager.Play("Boss死了");
            jumpStep = 0;
            isDie = true;
            PostNotification.Post(Const.Notification.PlayerDie, this);
        }
    }
}
