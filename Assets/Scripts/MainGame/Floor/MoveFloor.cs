using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public float moveSpeed = 50.0f;

    void Update()
    {
        // オブジェクトを右から左に移動させる
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}