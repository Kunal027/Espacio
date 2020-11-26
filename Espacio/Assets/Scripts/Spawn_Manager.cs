using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrephab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject [] _powerUps;

    private bool _stopSpwning = false;


    void Start()
    {
        
    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpwning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.34f, 8.34f), 5f, 0);
            GameObject newEnemy = Instantiate(_EnemyPrephab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }

    
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpwning == false)
        {
            Vector3 postTOSpawn = new Vector3(Random.Range(-6.54f, 6.54f), 4f, 0);
            int randomPowerUp = Random.Range(0, 3);
           Instantiate(_powerUps[randomPowerUp], postTOSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8, 12)); 
}
    }
    public void OnPlayerDeath()
    {

        _stopSpwning = true;
    }
}
