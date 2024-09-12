using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickableCircle : MonoBehaviour
{
    private GameManager gameManager;
    private Image imageComponent;
    private bool isClickable = false;
    private bool hasClicked = false;
    private float creationTime;
    [SerializeField]
    private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.0f);
    [SerializeField]
    private Vector3 initialScale = new Vector3(0.0f, 0.0f, 1.0f);
    [SerializeField]
    private float duration = 3.0f;
    [SerializeField]
    private float successDelay = 0.5f;
    [SerializeField]
    private float missDelay = 0.5f;

    void Start()
    {
        // GameManagerの取得
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Debug.Log("GameManager found and assigned.");
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }

        // Imageコンポーネントの取得
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on this GameObject!");
        }

        creationTime = Time.time;
        transform.localScale = initialScale; // スケールの初期化
    }

    void FixedUpdate()
    {
        float elapsedTime = Time.time - creationTime;

        // スケール操作: duration秒でサイズを変化させる
        float t = Mathf.Clamp01(elapsedTime / duration);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        // 緑色に光らせ、クリック可能にする時間帯
        if (elapsedTime >= 1.8f && elapsedTime <= 2.4f)
        {
            imageComponent.color = Color.green;
            isClickable = true;
        }
        else if (elapsedTime > 2.5f)
        {
            imageComponent.color = Color.red;
            isClickable = false;
        }

        // 3.0秒後にクリックされていない場合、Missとともにオブジェクトを破棄
        if (elapsedTime > 3.0f && !hasClicked)
        {
            hasClicked = true;
            gameManager.Miss();
            StartCoroutine(DelayedDestroyObject(missDelay));
        }
    }

    // クリックされたときの処理
    void OnMouseDown()
    {
        // クリック可能で、まだクリックされていない場合
        if (isClickable && !hasClicked)
        {
            hasClicked = true;
            gameManager.Hit();
            StartCoroutine(DelayedDestroyObject(successDelay));
        }
    }

    IEnumerator DelayedDestroyObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
