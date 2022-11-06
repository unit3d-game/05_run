using UnityEngine;
using System.Collections;

public class EnemyObject : MonoBehaviour
{




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == Const.GameObjectName.LeftDie)
        {

            Destroy(gameObject);
        }
    }

}

