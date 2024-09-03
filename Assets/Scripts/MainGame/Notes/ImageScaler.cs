using UnityEngine;

public class ImageScaler : MonoBehaviour
{
    private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.0f);
    private Vector3 initialScale = new Vector3(0.1f, 0.1f, 1.0f);
    private float duration = 3.3f;
    private float startTime;

    void Start()
    {
        transform.localScale = initialScale;
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // スムーズにスケーリング
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        if (t >= 1.0f)
        {
            Destroy(this); // スケーリング完了後にこのコンポーネントを削除する（任意）
        }
    }
}
