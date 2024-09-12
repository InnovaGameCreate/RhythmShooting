using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int hit = 0;  // 成功数
    [SerializeField] public int miss = 0;  // 失敗数
    [SerializeField] private Animator animator; // アニメーション
    [SerializeField] private GameObject notesSE; // ノーツサウンドエフェクト
    [SerializeField] private GameObject plantPos; // PlantPosオブジェクト（判定対象）
    void Start()
	{
		hit = 0;
		miss = 0;
	}
    public void Hit() // 成功数を1増やすメソッド
    {
        hit += 1;
        animator.SetTrigger("Jump");  // アニメーションをトリガー
        Instantiate(notesSE);  // サウンドエフェクトを再生

        // PlantPosのコライダーに触れているオブジェクトがPlantであるかチェック
        Collider[] hitColliders = Physics.OverlapSphere(plantPos.transform.position, 3f); // PlantPos周辺のコライダーを取得
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Plant")) // タグがPlantのオブジェクトを見つける
            {
                Transform sprout = hitCollider.transform.Find("Sprout"); // 子オブジェクトのSproutを探す
                Transform flower = hitCollider.transform.Find("Flower"); // 子オブジェクトのFlowerを探す

                if (sprout != null && flower != null)
                {
                    // Sproutを非アクティブ、Flowerをアクティブにする
                    sprout.gameObject.SetActive(false);
                    flower.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Miss() // 失敗数を1増やすメソッド
    {
        miss += 1;
    }

    public int GetHit() // 成功数を返すメソッド
    {
        return hit;
    }

    public int GetMiss() // 失敗数を返すメソッド
    {
        return miss;
    }
}