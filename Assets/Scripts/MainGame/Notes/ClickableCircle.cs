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
        // GameManager�̎擾
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Debug.Log("GameManager found and assigned.");
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }

        // Image�R���|�[�l���g�̎擾
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on this GameObject!");
        }

        creationTime = Time.time;
        transform.localScale = initialScale; // �X�P�[���̏�����
    }

    void FixedUpdate()
    {
        float elapsedTime = Time.time - creationTime;

        // �X�P�[������: duration�b�ŃT�C�Y��ω�������
        float t = Mathf.Clamp01(elapsedTime / duration);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        // �ΐF�Ɍ��点�A�N���b�N�\�ɂ��鎞�ԑ�
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

        // 3.0�b��ɃN���b�N����Ă��Ȃ��ꍇ�AMiss�ƂƂ��ɃI�u�W�F�N�g��j��
        if (elapsedTime > 3.0f && !hasClicked)
        {
            hasClicked = true;
            gameManager.Miss();
            StartCoroutine(DelayedDestroyObject(missDelay));
        }
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    void OnMouseDown()
    {
        // �N���b�N�\�ŁA�܂��N���b�N����Ă��Ȃ��ꍇ
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
