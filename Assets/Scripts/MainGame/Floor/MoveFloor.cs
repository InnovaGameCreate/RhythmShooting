using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public float moveSpeed = 50.0f;

    void Update()
    {
        // �I�u�W�F�N�g���E���獶�Ɉړ�������
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}