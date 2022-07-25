using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage;
    public GameObject parent;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = parent.GetComponent<Rigidbody2D>().velocity;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<Enemy>().isInvincible)
            {
                if (other.GetComponent<Enemy>().GetDamage(damage))
                {
                    parent.GetComponent <Player>().addXP(other.GetComponent<Enemy>().XPForPlayer);
                }
            }
        }
    }
}
