using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameObject[] player;
    Rigidbody2D rigidBody;

    public float speed;
    public float maxHP;
    public float HP;
    public float noticeRange;
    public float damage;
    public float XPForPlayer;

    public bool isInvincible;
    SpriteRenderer spriteRenderer;
    public Material spriteDefault;
    public Material paintWhite;

    public GameObject diamondPrefab;
    GameObject diamondObject;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        HP = maxHP;

        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        int minIndex = -1;
        int min = 100000;
        for (int i = 0; i < player.Length; i++)
        {
            if(Vector3.Magnitude(player[i].transform.position - transform.position) < min)
            {
                minIndex = i;
            }
        }
        if (minIndex == -1)
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            if (Vector3.Magnitude(player[minIndex].transform.position - transform.position) < noticeRange)
            {
                Vector3 direction = Vector3.Normalize(player[minIndex].transform.position - transform.position);
                rigidBody.velocity = direction * speed;
                if(direction.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, 1);
                }
                else
                {
                    transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, 1);
                }
            }
            else
            {
                rigidBody.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public bool GetDamage(float damage)
    {
        HP -= damage;
        isInvincible = true;
        StartCoroutine(FlashWhite());
        StartCoroutine(InvincibilityCoolTime(1.0f));
        StartCoroutine(Knockback());

        if (HP <= 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                diamondObject = Instantiate(diamondPrefab);
                diamondObject.transform.position = transform.position;
            }
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    IEnumerator FlashWhite()
    {
        spriteRenderer.material = paintWhite;

        float time = 1.0f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        spriteRenderer.material = spriteDefault;
    }

    IEnumerator InvincibilityCoolTime(float cool)
    {
        float time = cool;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isInvincible = false;
    }

    IEnumerator Knockback()
    {
        float originalSpeed = speed;
        speed = -50;
        while (speed < originalSpeed)
        {
            speed += Time.deltaTime*1000;
            yield return new WaitForSeconds(0.001f);
        }
        
        speed = originalSpeed;
    }
}
