using UnityEngine;
using System.Collections;

public class ClickableCircle : MonoBehaviour
{
    private float gracePeriod = 0.1f; // ±0.1秒の猶予
    private GameManager gameManager;

    void Start()
    {
        // GameManager の参照を取得
        GameObject scoreObject = GameObject.Find("GameManager");
        if (scoreObject != null)
        {
            gameManager = scoreObject.GetComponent<GameManager>();
            Debug.Log("GameManager found and assigned.");
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D fired."); // トリガーが発火したことを確認
        if (other.CompareTag("ImageNote"))
        {
            Debug.Log("Trigger with ImageNote detected.");
            StartCoroutine(HandleOverlap(other.gameObject));
        }
        else
        {
            Debug.Log("Trigger with non-ImageNote detected: " + other.gameObject.name);
        }
    }

    private IEnumerator HandleOverlap(GameObject imageNote)
    {
        float startTime = Time.time;
        float overlapStartTime = startTime + 0.1f; // 猶予開始時間（±0.1秒）
        float overlapEndTime = overlapStartTime + 0.1f; // 猶予終了時間
        bool isSuccessful = false;

        while (Time.time < overlapEndTime)
        {
            if (Time.time >= overlapStartTime && Time.time <= overlapEndTime)
            {
                // 猶予期間内であればクリックを検出
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collider = GetComponent<Collider2D>();

                    if (collider.OverlapPoint(mousePosition))
                    {
                        Debug.Log("Success detected.");
                        gameManager.Hit(); // 成功数を増やす
                        isSuccessful = true;
                        break; // 成功したのでループを終了
                    }
                }
            }
            yield return null;
        }

        if (!isSuccessful)
        {
            Debug.Log("Failure detected.");
            gameManager.Miss(); // 失敗数を増やす
        }

        // 判定が終わった後に Image Note を削除
        Destroy(imageNote);
    }

}
