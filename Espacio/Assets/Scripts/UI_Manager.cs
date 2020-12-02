using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _liveImage;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOvertext;

    [SerializeField]
    private Text _restartGametext;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score:" + 0;
        _gameOvertext.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("Game_Manager is null");
        }
    }

    // Update is called once per frame
  public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    
    public void UpdateLives(int CurrenLives)
    {
        _liveImage.sprite = _liveSprites[CurrenLives];
        if(CurrenLives == 0 )
        {
            gameOverSequence();

        }
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOvertext.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOvertext.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }
    void gameOverSequence()
    {
        _gameManager.GameOver();
        _gameOvertext.gameObject.SetActive(true);
        _restartGametext.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

    }

    public void ResumePlay()
    {
        //GameManager gameManager = GameObject.Find("Game_Mnagaer").GetComponent<GameManager>();
        _gameManager.ResumeGame();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
