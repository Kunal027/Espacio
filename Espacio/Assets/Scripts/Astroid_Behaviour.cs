﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid_Behaviour : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;

    [SerializeField]
    private GameObject _explosionPrephab;
    private Spawn_Manager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrephab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.startSpawning();
            this.GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}