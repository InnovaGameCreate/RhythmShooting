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
    private Vector3 initialScale = new Vector3(0.1f, 0.1f, 1.0f);
    [SerializeField]
    private float duration = 3.3f;
    private float startTime;

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
        startTime = Time.time; // スケール用の開始時間も設定
        transform.localScale = initialScale; // スケールの初期化

        StartCoroutine(CheckClickTiming());
    }

    void Update()
    {
        float elapsedTime = Time.time - creationTime;

        // スケール操作: 3.3秒でサイズを変化させる
        float t = Mathf.Clamp01(elapsedTime / duration);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        // 3.0秒から3.1秒の間に緑に光らせる
        if (elapsedTime >= 3.0f && elapsedTime <= 3.1f)
        {
            imageComponent.color = Color.green;
            isClickable = true;
        }
        else if (elapsedTime > 3.1f)
        {
            imageComponent.color = Color.red;
            isClickable = false;
        }

        // 3.3秒後にオブジェクトを破棄する
        if (elapsedTime > 3.3f && !hasClicked)
        {
            gameManager.Miss();
            Destroy(gameObject);
        }
    }

    IEnumerator CheckClickTiming()
    {
        yield return new WaitForSeconds(3.0f);

        while (Time.time - creationTime <= 3.1f)
        {
            yield return null;
            if (isClickable && Input.GetMouseButtonDown(0))
            {
                if (!hasClicked)
                {
                    hasClicked = true;
                    gameManager.Hit();
                    Destroy(gameObject);
                }
            }
        }

        // クリックされなかった場合、色を元に戻す
        if (!hasClicked)
        {
            imageComponent.color = Color.white;
        }
    }
}
