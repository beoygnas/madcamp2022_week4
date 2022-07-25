using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject parent;
    Image HP;

    // Start is called before the first frame update
    void Start()
    {
        HP = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == null)
        {
            Destroy(gameObject);
        }
        else
        {
            HP.fillAmount = parent.GetComponent<Player>().HP / parent.GetComponent<Player>().maxHP;

            transform.position = Camera.main.WorldToScreenPoint(parent.transform.position + new Vector3(0, 1, 0));
        }
    }
}
