using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID;

    [SerializeField]
    private AudioClip _audClip;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.84f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audClip, transform.position);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.tripleShotActive();
                        break;
                    case 1:
                        player.speedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("No powerup collected");
                        break;
                }
                Destroy(this.gameObject);


            }
        }

    }
}
