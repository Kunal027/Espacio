using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 4.5f;
    private float _speedMulti = 2;
    [SerializeField]
    private GameObject _laserPrephab;
    [SerializeField]
    private GameObject _tripleShotPrephab;
    [SerializeField]
    private float _fireRate = 0.18f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private Spawn_Manager _spawnManager;
    
    private bool _isTripleShotActiv = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _ShieldVisual;

    [SerializeField]
    private GameObject _LeftEngine, _RightEngine;

    [SerializeField]
    private int _score;

    private int _highScore;

    private UI_Manager _ui_Manager;

    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSoundclip;
    private AudioSource _audioSource;

   

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Game Maneger not found");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _ui_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();
        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }

        if ( _audioSource == null)
        {
            Debug.LogError("No audio on player is null");
        }
        else
        {
            _audioSource.clip = _laserSoundclip;
        }
        

        if (_spawnManager == null)
        {
            Debug.LogError("Spaws manager is null");
        }

        if(_ui_Manager == null)
        {
            Debug.Log("UI manager is null");
        }
    }


    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void calculateMovement()
    {
        float horiInput = Input.GetAxis("Horizontal");
        float vertiInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * horiInput* _speed *  Time.deltaTime);
        //transform.Translate(Vector3.up * vertiInput * _speed * Time.deltaTime);
        //or
        Vector3 direction = new Vector3(horiInput, vertiInput, 0);
        if(_isSpeedBoostActive == false )
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _speedMulti) * Time.deltaTime);
        }

        
 
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.98f)
        {
            transform.position = new Vector3(transform.position.x, -3.98f, 0);
        }

 
        if (transform.position.x > 8.34f)
        {
            transform.position = new Vector3(-8.34f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.34f)
        {
            transform.position = new Vector3(8.34f, transform.position.y, 0);
        }

    }
    void FireLaser()
    { 
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActiv == true)
        {
            Instantiate(_tripleShotPrephab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrephab, transform.position + new Vector3(0, 0.73f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }
    public void Damage()
    {   
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _ShieldVisual.SetActive(false);
            return;
        }
        //Called the methord in Enemy script
        _lives --;

        if(_lives == 2)
        {
            _LeftEngine.SetActive(true);
        }
         if(_lives == 1)
        {
            _RightEngine.SetActive(true);
        }
    


        _ui_Manager.UpdateLives(_lives);
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

    }

    public void tripleShotActive()
    {
        _isTripleShotActiv = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActiv = false;

    }
    public void speedBoostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _ShieldVisual.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());

    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isShieldActive = false;
        _ShieldVisual.SetActive(false);
    }

    public void AddScore(int points)
    {
        _score += points;
        _ui_Manager.UpdateScore(_score);
    }

    public void AddBestScore()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
        }
    }

}


