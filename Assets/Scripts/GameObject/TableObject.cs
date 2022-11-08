using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == Const.GameObjectName.LeftDie)
        {

            Destroy(gameObject);
        }
    }
}
