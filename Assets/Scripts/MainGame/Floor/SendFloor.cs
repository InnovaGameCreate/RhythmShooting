using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFloor : MonoBehaviour
{
    public GameObject DestinationObject; // �ړ���̃I�u�W�F�N�g

    void OnTriggerEnter(Collider other)
    {
        // ���̃I�u�W�F�N�g�ƏՓ˂����Ƃ��̏���
        if (other.gameObject)
        {
            // �^�[�Q�b�g�I�u�W�F�N�g�̍��W�Ɉړ�������
            other.gameObject.transform.position = DestinationObject.transform.position;
        }
    }
}
