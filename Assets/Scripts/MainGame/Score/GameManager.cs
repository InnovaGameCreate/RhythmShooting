using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int hit = 0;  // ������
    [SerializeField] public int miss = 0;  // ���s��
    [SerializeField] private Animator animator; // �A�j���[�V����
    [SerializeField] private GameObject notesSE; // �m�[�c�T�E���h�G�t�F�N�g
    [SerializeField] private GameObject plantPos; // PlantPos�I�u�W�F�N�g�i����Ώہj
    
    public void Hit() // ��������1���₷���\�b�h
    {
        hit += 1;
        animator.SetTrigger("Jump");  // �A�j���[�V�������g���K�[
        Instantiate(notesSE);  // �T�E���h�G�t�F�N�g���Đ�

        // PlantPos�̃R���C�_�[�ɐG��Ă���I�u�W�F�N�g��Plant�ł��邩�`�F�b�N
        Collider[] hitColliders = Physics.OverlapSphere(plantPos.transform.position, 0.5f); // PlantPos���ӂ̃R���C�_�[���擾
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Plant")) // �^�O��Plant�̃I�u�W�F�N�g��������
            {
                Transform sprout = hitCollider.transform.Find("Sprout"); // �q�I�u�W�F�N�g��Sprout��T��
                Transform flower = hitCollider.transform.Find("Flower"); // �q�I�u�W�F�N�g��Flower��T��

                if (sprout != null && flower != null)
                {
                    // Sprout���A�N�e�B�u�AFlower���A�N�e�B�u�ɂ���
                    sprout.gameObject.SetActive(false);
                    flower.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Miss() // ���s����1���₷���\�b�h
    {
        miss += 1;
    }

    public int GetHit() // ��������Ԃ����\�b�h
    {
        return hit;
    }

    public int GetMiss() // ���s����Ԃ����\�b�h
    {
        return miss;
    }
}