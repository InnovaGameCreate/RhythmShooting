using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFloor : MonoBehaviour
{
    public GameObject DestinationObject; // 移動先のオブジェクト

    void OnTriggerEnter(Collider other)
    {
        // 他のオブジェクトと衝突したときの処理
        if (other.gameObject)
        {
            // ターゲットオブジェクトの座標に移動させる
            other.gameObject.transform.position = DestinationObject.transform.position;
        }
    }
}
