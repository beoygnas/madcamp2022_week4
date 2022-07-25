using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class RandomSpawner : MonoBehaviour
{

    public bool[] isStarted;
    public bool[] isDelay;
    public GameObject[] enemyPrefabs; // 0~5 : level1, 6~11 : level2, 12~14 : level3, 15~17 : level4
    public Transform[] spawnPoints; // 0~3 : level1, 4~7 : level2, 8~11 : level3, 12~15 : level4

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(level2_StartTimer());
        StartCoroutine(level3_StartTimer());
        StartCoroutine(level4_StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Instance.gameStart)
        {
            if (isDelay[0] == false && isStarted[0] == true)
            {
                isDelay[0] = true;
                int randEmeny = Random.Range(0, 6);
                int randSpawnPoint = Random.Range(0, 4);

                PhotonNetwork.Instantiate(enemyPrefabs[randEmeny].name, spawnPoints[randSpawnPoint].position, transform.rotation);
                StartCoroutine(level1_SpawnTimer());
            }

            if (isDelay[1] == false && isStarted[1] == true)
            {
                isDelay[1] = true;
                int randEmeny = Random.Range(6, 12);
                int randSpawnPoint = Random.Range(4, 8);

                PhotonNetwork.Instantiate(enemyPrefabs[randEmeny].name, spawnPoints[randSpawnPoint].position, transform.rotation);
                StartCoroutine(level2_SpawnTimer());
            }

            if (isDelay[2] == false && isStarted[2] == true)
            {
                isDelay[2] = true;
                int randEmeny = Random.Range(12, 15);
                int randSpawnPoint = Random.Range(8, 12);

                PhotonNetwork.Instantiate(enemyPrefabs[randEmeny].name, spawnPoints[randSpawnPoint].position, transform.rotation);
                StartCoroutine(level3_SpawnTimer());
            }

            if (isDelay[3] == false && isStarted[3] == true)
            {
                isDelay[3] = true;
                int randEmeny = Random.Range(15, 18);
                int randSpawnPoint = Random.Range(12, 16);

                PhotonNetwork.Instantiate(enemyPrefabs[randEmeny].name, spawnPoints[randSpawnPoint].position, transform.rotation);
                StartCoroutine(level4_SpawnTimer());
            }
        }
    }

    IEnumerator level1_SpawnTimer()
    {
        yield return new WaitForSeconds(5f);
        isDelay[0] = false;
    }
    IEnumerator level2_SpawnTimer()
    {
        yield return new WaitForSeconds(10f);
        isDelay[1] = false;
    }
    IEnumerator level3_SpawnTimer()
    {
        yield return new WaitForSeconds(20f);
        isDelay[2] = false;
    }
    IEnumerator level4_SpawnTimer()
    {
        yield return new WaitForSeconds(30f);
        isDelay[3] = false;
    }


    IEnumerator level2_StartTimer()
    {
        //시간 말고, 일정레벨이 지나면 false가 되게 하면 될듯.
        yield return new WaitForSeconds(10f);
        isStarted[1] = true;
    }
    IEnumerator level3_StartTimer()
    {
        yield return new WaitForSeconds(20f);
        isStarted[2] = true;
    }
    IEnumerator level4_StartTimer()
    {
        yield return new WaitForSeconds(30f);
        isStarted[3] = true;
    }

}
