using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] public int hit = 0;  // 成功数
    [SerializeField] public int miss = 0;  // 失敗数
    [SerializeField] private Animator animator; // アニメーション
    [SerializeField] private GameObject announceBordObj; // 演出用ボード
    [SerializeField] private GameObject notesSE; // ノーツサウンドエフェクト
    [SerializeField] private GameObject plantPos; // PlantPosオブジェクト（判定対象）
    private bool isFinishGame;
    void Start()
	{
        isFinishGame = false;
        // 再生開始位置を0.5秒に設定
        audioSource.time = 0.5f;

        // 音源の再生を開始
        audioSource.Play();
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

    private void FixedUpdate()
    {
        if (!audioSource.isPlaying　&& !isFinishGame)
        {
            isFinishGame = true;
            StartCoroutine("TransitionToResult");
        }
    }
    IEnumerator TransitionToResult()
    {
        // アニメーショントリガーをセット
        Animator AnnounceAnimator = announceBordObj.GetComponent<Animator>();
        AnnounceAnimator.SetTrigger("isFinish");
        yield return new WaitForSeconds(4.0f);
        // "Result"シーンに遷移
        SceneManager.LoadScene("Result");
    }
}