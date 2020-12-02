using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*this script contains behavior of the laser and damage(player,destrucion of enemy and laser) parameters
  (player lives and damaging system) */

public class Enemy : MonoBehaviour
{
    //[SerializeField]
    private float _speed = 2.5f;

    private Player _player;

    private Animator _anim;

    private AudioSource _audEnemy;

    [SerializeField]
    private GameObject _laserPrephab;

    private float _fireRate = 3.0f;
    private float _canfire = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audEnemy = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The player is null(Enemy Script)");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The animator is null (Enemy Script )");
        }
       // Levelmanager();

    }
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canfire)
        {
            _fireRate = Random.Range(10f, 15f);
            _canfire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrephab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for(int i = 0; i<lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4f)
        {
            float randomXpos = Random.Range(-6.54f, 6.54f);
            transform.position = new Vector3(randomXpos, 4f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null) //Cheking the existance of the component 
            {
                player.Damage();
            }
            Destroy(this.gameObject, 2.8f);
            _speed = 0;
            _audEnemy.Play();
            this.GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath"); 
        }
        if (other.tag == "Laser")
        {
            _canfire = 0f;
            Destroy(other.gameObject, 2.8f);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 2.8f);
            _audEnemy.Play();
            _speed = 0;
            this.GetComponent<Collider2D>().enabled = false;
            Destroy(other.gameObject);
            
            
        }

    }
    /*
    public void Levelmanager()
    {
        if (_speed < 90)
        {
            _speed = 1.5f;
        }
        else if (_speed < 1000)
        {
            _speed = 2.5f;
        }
        else if (_speed < 2000)
        {
            _speed = 3.5f;
        }
        else if (_speed < 1000)
        {
            _speed = 4.5f;
        }
        else if (_speed < 1000)
        {
            _speed = 5.5f;
        }
        else
        {
            _speed = 6.5f;
        }
    }
    */
}