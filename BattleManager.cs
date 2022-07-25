using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public bool gameStart = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
                Destroy(this.gameObject);
        }

        CreatePlayer();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if 10
        // move player
        // create monsters
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && gameStart == false)
        {
            gameStart = true;
        }


    }


    void CreatePlayer()
    {
        Transform[] points = GameObject.Find("PlayerSpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);

        GameObject player = PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);

        Camera.main.GetComponent<MainCamera>().player = player;

    }
}
