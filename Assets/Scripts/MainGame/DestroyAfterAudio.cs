using UnityEngine;

public class DestroyAfterAudio : MonoBehaviour
{
    // AudioSource コンポーネントを保持
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // AudioClip が再生される
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // AudioClip の再生が終了したかどうかを確認
        if (!audioSource.isPlaying)
        {
            // オブジェクトを削除
            Destroy(gameObject);
        }
    }
}