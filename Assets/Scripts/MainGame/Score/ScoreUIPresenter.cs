
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUIPresenter : MonoBehaviour
{
    private GameObject scoreObject;
    private GameManager scoreScript;
    private GameObject scoreTextObj;
    private GameObject successScoreTextObj;

    private Text scoreText;
    private Text resultTextSuccess;

    private int successScore;
    private int falseScore;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        // �V�[�������[�h���ꂽ�Ƃ��ɌĂяo�����C�x���g�ɓo�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // �V�[�����[�h�̃C�x���g�������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �V�[�������[�h����邽�тɌĂ΂�郁�\�b�h
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainGame":
                scoreObject = GameObject.Find("GameManager");
                scoreTextObj = GameObject.Find("ScoreText");
                scoreScript = scoreObject.GetComponent<GameManager>();
                scoreText = scoreTextObj.GetComponent<Text>();
                break;

            case "Title":
                // ���I�u�W�F�N�g��j��
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
                Destroy(gameObject);
                break;

            case "Result":
                successScoreTextObj = GameObject.Find("SuccessScoreText");
                resultTextSuccess = successScoreTextObj.GetComponent<Text>();
                resultTextSuccess.text = successScore.ToString();
                break;

            default:
                Debug.LogWarning("Unknown scene: " + scene.name);
                break;
        }
    }

    void FixedUpdate() // �e�L�X�g���X�R�A�ɕϊ�
    {
        if (SceneManager.GetActiveScene().name == "MainGame" && scoreScript != null)
        {
            successScore = scoreScript.hit;
            falseScore = scoreScript.miss;

            scoreScript.GetHit();
            scoreScript.GetMiss();
            scoreText.text = successScore.ToString() + "\n" + falseScore.ToString();
        }
    }
}
