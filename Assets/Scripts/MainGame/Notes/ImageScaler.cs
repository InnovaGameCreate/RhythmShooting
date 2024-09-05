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

        // �X���[�Y�ɃX�P�[�����O
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        if (t >= 1.0f)
        {
            Destroy(this); // �X�P�[�����O������ɂ��̃R���|�[�l���g���폜����i�C�Ӂj
        }
    }
}
