using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int hit = 0;　//成功数
    [SerializeField] public int miss = 0;　//失敗数

    public void Hit() //成功数を１増やすメソッド
    {
        hit += 1;
    }
    public void Miss() //失敗数を１増やすメソッド
    {
        miss += 1;
    }
    public int GetHit() //成功数を返すメソッド
    {
        return hit;
    }
    public int GetMiss() //失敗数を返すメソッド
    {
        return miss;
    }

}
