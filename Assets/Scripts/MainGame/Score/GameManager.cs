using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int hit = 0;�@//������
    [SerializeField] public int miss = 0;�@//���s��

    public void Hit() //���������P���₷���\�b�h
    {
        hit += 1;
    }
    public void Miss() //���s�����P���₷���\�b�h
    {
        miss += 1;
    }
    public int GetHit() //��������Ԃ����\�b�h
    {
        return hit;
    }
    public int GetMiss() //���s����Ԃ����\�b�h
    {
        return miss;
    }

}
