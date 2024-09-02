using UnityEngine;
using System.Collections;

public class ImageScaler : MonoBehaviour
{
    public GameObject imagePrefab; // Image Notes の Prefab
    public Canvas canvas; // シーン内の Canvas をアサイン

    [Header("Image Notes の生成位置")]
    public Vector3[] positions = new Vector3[15]; // Inspector で指定する位置

    private GameObject currentImageInstance; // 現在存在する Image Notes のインスタンス

    private void Start()
    {
        StartCoroutine(ManageImageNotes());
    }

    private IEnumerator ManageImageNotes()
    {
        while (true)
        {
            // 既存の Image Notes を削除
            if (currentImageInstance != null)
            {
                Destroy(currentImageInstance);
            }

            // 15個の位置からランダムに1つを選択して生成
            int index = Random.Range(0, positions.Length);
            Vector3 selectedPosition = positions[index];

            currentImageInstance = Instantiate(imagePrefab);
            currentImageInstance.transform.SetParent(canvas.transform, false); // Canvas の子として配置
            currentImageInstance.transform.localPosition = selectedPosition; // 指定された位置に移動

            StartCoroutine(ScaleImage(currentImageInstance));

            // 4秒間待機
            yield return new WaitForSeconds(4.0f);
        }
    }

    private IEnumerator ScaleImage(GameObject imageNote)
    {
        Vector3 initialScale = imageNote.transform.localScale;
        Vector3 targetScale = new Vector3(1.1f, 1.1f, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (imageNote == null)
            {
                yield break; // もしオブジェクトが破棄されていたら処理を終了する
            }

            imageNote.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (imageNote != null)
        {
            imageNote.transform.localScale = targetScale; // 最終的にターゲットスケールに設定
        }
    }
}
