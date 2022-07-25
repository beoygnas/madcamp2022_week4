using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject[] player;

    public GameObject canvas;
    public GameObject HPBarPrefab;
    GameObject HPBarObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < player.Length; i++)
        {
            HPBarObject = Instantiate(HPBarPrefab);
            HPBarObject.transform.parent = canvas.transform;
            HPBarObject.GetComponent<HPBar>().parent = player[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
