using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float gameOverWait;

    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject player;
    
    //public Text restartText;
    

    private bool gameOver = false;
    private bool restart = false;
    private bool canRestart = false;
    private int score;
    private GameObject background;

    void Start()
    {
        newGame();
        background = GameObject.FindGameObjectWithTag("Background");
    }

    void newGame()
    {
        score = 0;
        UpdateScore();
    }

    void FixedUpdate()
    {
        UpdateScore();
        if (canRestart && restart)
            restartGame();
    }

    void Update()
    {
        if (gameOver && canRestart)
        {
            if (Application.platform == RuntimePlatform.Android && AndroidInputs.getAnyTouchBeginInput())
                restart = true;
            else if (Application.platform == RuntimePlatform.WindowsEditor && Input.anyKey)
                restart = true;
        }
        if (!gameOver)
        {
            if (background.GetComponent<Transform>().position.z < -4)
                background.GetComponent<Transform>().position = new Vector3(0, 0, 150);
            else if (background.GetComponent<Transform>().position.z < 10)
                background.GetComponent<Transform>().position = new Vector3(0, 0, background.GetComponent<Transform>().position.z - 0.0001f);
            else
                background.GetComponent<Transform>().position = new Vector3(0, 0, background.GetComponent<Transform>().position.z - 0.01f);
        }


    }

    void restartGame()
    {
        background.GetComponent<Transform>().position = new Vector3(0, 0, 10);
        restart = false;
        Instantiate(player);
        newGame();
        StartGame();
    }

    public void StartGame()
    {
        gameOver = false;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        canRestart = false;
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        yield return new WaitForSeconds(gameOverWait);
        canRestart = true;
    }

    IEnumerator GameOverSpawn()
    {
        GameObject gameOverObject = Instantiate(gameOverText, GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>());
        while (gameOver)
        {
            gameOverObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOverObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameOverObject);
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.GetComponent<TextMesh>().text = "Your score : " + score;
    }

    public void GameOver()
    {
        Debug.Log("Game over !");
        gameOver = true;
        StartCoroutine(GameOverSpawn());
    }
}