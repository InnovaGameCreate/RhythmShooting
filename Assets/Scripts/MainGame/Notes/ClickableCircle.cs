using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickableCircle : MonoBehaviour
{
    private GameManager gameManager;
    private Image imageComponent;
    public Sprite clicableSprite;
    private bool isClickable = false;
    private bool hasClicked = false;
    private float creationTime;
    [SerializeField]
    private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.0f);
    [SerializeField]
    private Vector3 initialScale = new Vector3(0.0f, 0.0f, 1.0f);
    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float successDelay = 0.5f;
    [SerializeField]
    private float missDelay = 0.5f;
    private bool hasColorChanged = false;

    void Start()
    {
        // GameManagerの取得
        gameManager = GameObject.FindObjectOfType<GameManager>();
        //if (gameManager != null)
        //{
        //    Debug.Log("GameManager found and assigned.");
        //}
        //else
        //{
        //    Debug.LogError("GameManager not found!");
        //}

        // Imageコンポーネントの取得
        imageComponent = GetComponent<Image>();
        //if (imageComponent == null)
        //{
        //    Debug.LogError("Image component not found on this GameObject!");
        //}

        creationTime = Time.time;
        transform.localScale = initialScale; // スケールの初期化
    }

    void FixedUpdate()
    {
        float elapsedTime = Time.time - creationTime;

        // スケール操作: duration秒でサイズを変化させる
        float t = Mathf.Clamp01(elapsedTime / duration);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        // 光らせ、クリック可能にする時間帯
        if (elapsedTime >= 1.4f && elapsedTime < 2.0f)
        {
            isClickable = true;
        }
        else if (elapsedTime >= 2.0f)
        {
            isClickable = false;
            if (!hasColorChanged)
            {
                StartCoroutine(ChangeColorOverTime());
                hasColorChanged = true; // 一度実行したらフラグを立てる
            }

        }
        if (hasClicked)
        {
            return; // クリックされていれば以降の処理は行わない
        }
        // 2.5秒後にクリックされていない場合、Missとともにオブジェクトを破棄
        if (elapsedTime > 2.2f && !hasClicked)
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
            imageComponent.sprite = clicableSprite;
            gameManager.Hit();
            StartCoroutine(ScaleAndFadeCoroutine());
            StartCoroutine(DelayedDestroyObject(successDelay));
        }
    }
    IEnumerator ScaleAndFadeCoroutine()
    {
        // 0.05秒間でサイズを小さくする
        Vector3 shrinkAmount = new Vector3(-0.1f, -0.1f, 0f);
        float shrinkDuration = 0.05f;
        float shrinkElapsed = 0f;

        while (shrinkElapsed < shrinkDuration)
        {
            shrinkElapsed += Time.deltaTime;
            transform.localScale += shrinkAmount * (Time.deltaTime / shrinkDuration);
            yield return null; // 1フレーム待機
        }

        // 0.25秒間でサイズを大きくしながら透明度を減少させる
        Vector3 growAmount = new Vector3(0.3f, 0.3f, 0.0f);
        float growDuration = 0.25f;
        float growElapsed = 0f;
        Color initialColor = imageComponent.color;

        while (growElapsed < growDuration)
        {
            growElapsed += Time.deltaTime;
            transform.localScale += growAmount * (Time.deltaTime / growDuration);

            // 透明度を1から0に変化させる
            Color newColor = imageComponent.color;
            newColor.a = Mathf.Lerp(1f, 0f, growElapsed / growDuration);
            imageComponent.color = newColor;

            yield return null;
        }

        // 最後に透明度を完全に0にする
        Color finalColor = imageComponent.color;
        finalColor.a = 0f;
        imageComponent.color = finalColor;

        // 最後のサイズの設定（必要であれば調整）
        transform.localScale = initialScale + growAmount;
    }
    IEnumerator ChangeColorOverTime()
    {
        float duration = 0.3f;  // 0.3秒間かけて色を変更
        float elapsed = 0f;

        Color startColor = Color.white;  // #FFFFFF
        Color targetColor = new Color(0.271f, 0.224f, 0.0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // 色を線形補間
            imageComponent.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            yield return null; // 1フレーム待機
        }

        // 最後に正確に目標の色をセット
        imageComponent.color = targetColor;
    }

    IEnumerator DelayedDestroyObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
