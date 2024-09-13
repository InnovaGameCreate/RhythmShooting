
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
        // シーンがロードされたときに呼び出されるイベントに登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // シーンロードのイベントから解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーンがロードされるたびに呼ばれるメソッド
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
                // 自オブジェクトを破壊
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

    void FixedUpdate() // テキストをスコアに変換
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
