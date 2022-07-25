using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCanvas : MonoBehaviour
{
    public GameObject parent;
    public GameObject button_1;
    public GameObject button_2;
    public GameObject button_3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {

    }

    public void Button1Clicked()
    {
        print(1);
        parent.GetComponent<Player>().attackLV[0]++;
        gameObject.SetActive(false);
    }

    public void Button2Clicked()
    {
        gameObject.SetActive(false);
    }

    public void Button3Clicked()
    {
        gameObject.SetActive(false);
    }
}
