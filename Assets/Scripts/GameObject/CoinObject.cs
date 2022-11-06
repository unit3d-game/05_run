using UnityEngine;

public class CoinObject : MonoBehaviour
{
    /**
     * <summary>分数</summary>
     */
    public int Score;


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == Const.Tag.Player)
        {
            UserStorage.AddScore(Score);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == Const.Tag.Die)
        {
            Destroy(gameObject);
        }
    }
}

