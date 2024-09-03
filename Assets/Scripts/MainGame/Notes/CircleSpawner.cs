using UnityEngine;
using System.Collections;

public class CircleSpawner : MonoBehaviour
{
    public GameObject imageNotesPrefab; // Image Notes の Prefab を設定
    public Vector3[] spawnPositions; // 15箇所の生成位置の座標を設定
    private GameObject currentNote;
    public float spawnDelay = 1f; // 次のノートを生成するまでの遅延時間

    void Start()
    {
        SpawnNote();
    }

    void SpawnNote()
    {
        // 既存のノートを削除
        if (currentNote != null)
        {
            Destroy(currentNote);
        }

        // ランダムな生成位置を選択
        if (spawnPositions != null && spawnPositions.Length == 15)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Vector3 spawnPosition = spawnPositions[randomIndex];

            // Image Notes を生成し、現在のノートとして保存
            currentNote = Instantiate(imageNotesPrefab, spawnPosition, Quaternion.identity);

            // Canvasを取得して、Image NotesをCanvasの子オブジェクトに設定
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                currentNote.transform.SetParent(canvas.transform, false);
            }
            else
            {
                Debug.LogError("Canvas not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Please ensure that 15 spawn positions are assigned in the inspector.");
        }

        // 4秒後に次のノートを生成するためにコルーチンを開始
        StartCoroutine(SpawnNextNote());
    }

    IEnumerator SpawnNextNote()
    {
        // 指定された遅延時間だけ待機
        yield return new WaitForSeconds(spawnDelay);

        // 再びノートを生成
        SpawnNote();
    }
}
