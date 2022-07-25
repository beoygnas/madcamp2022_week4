using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Player : MonoBehaviour
{
    public FloatingJoystick joystick;
    float x, y;
    Rigidbody2D rigidBody;
    Animator animator;

    public float speed;
    public float maxHP;
    public float HP;

    public float XP;
    public float LV;
    

    public float XPForLevelUp;
    public GameObject levelUpCanvas;

    public float skillXP;
    public float skillLV;
    public float skillXPForLevelUp;
    public GameObject skillLevelUpCanvas;

    // 수정 전
    public GameObject swordPrefab;
    GameObject swordObject1;
    GameObject swordObject2;
    GameObject swordObject3;
    GameObject swordObject4;

    // 기본공격 추가
    public float[] attackLV; // 0 : sword, 1 : gun, 2 : electricity
    


    bool isRight;

    public bool isInvincible;
    SpriteRenderer spriteRenderer;
    public Material spriteDefault;
    public Material paintWhite;

    // Photon
    private PhotonView pv;
    private TextMeshProUGUI playerCount;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject temp = GameObject.Find("Floating Joystick");
        joystick = temp.GetComponent<FloatingJoystick>();
        playerCount = GameObject.Find("PlayerCount").GetComponent<TextMeshProUGUI>();


        StartCoroutine(AutoAttack_Sword());

        HP = maxHP;
        XP = 0;

        isInvincible = false;

        levelUpCanvas.SetActive(false);
        
        pv = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            Move();
            playerCount.SetText(PhotonNetwork.CurrentRoom.PlayerCount.ToString()+"/10");
        }

    }
    void Move()
    {
        //이동
        x = joystick.Horizontal;
        y = joystick.Vertical;

        rigidBody.velocity = new Vector2(x, y) * speed;
        if(x == 0 && y == 0)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isWalk", true);

            if(x > 0)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    animator.SetBool("isSide", true);
                    animator.SetBool("isUp", false);
                    animator.SetBool("isDown", false);
                    transform.localScale = new Vector2(-0.75f, 0.75f);
                    isRight = true;
                }
                else
                {
                    if(y > 0)
                    {
                        animator.SetBool("isSide", false);
                        animator.SetBool("isUp", true);
                        animator.SetBool("isDown", false);
                    }
                    else
                    {
                        animator.SetBool("isSide", false);
                        animator.SetBool("isUp", false);
                        animator.SetBool("isDown", true);
                    }
                }
            }
            else
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    animator.SetBool("isSide", true);
                    animator.SetBool("isUp", false);
                    animator.SetBool("isDown", false);
                    transform.localScale = new Vector2(0.75f, 0.75f);
                    isRight = false;
                }
                else
                {
                    if (y > 0)
                    {
                        animator.SetBool("isSide", false);
                        animator.SetBool("isUp", true);
                        animator.SetBool("isDown", false);
                    }
                    else
                    {
                        animator.SetBool("isSide", false);
                        animator.SetBool("isUp", false);
                        animator.SetBool("isDown", true);
                    }
                }
            }


            if (Vector2.SqrMagnitude(new Vector2(x, y)) > 0.7)
            {
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }
        }        
    }

    IEnumerator AutoAttack_Sword()
    {
        while (true)
        {
            swordObject1 = Instantiate(swordPrefab);
            swordObject1.GetComponent<Sword>().parent = gameObject;
            swordObject2 = Instantiate(swordPrefab);
            swordObject2.GetComponent<Sword>().parent = gameObject;
            swordObject3 = Instantiate(swordPrefab);
            swordObject3.GetComponent<Sword>().parent = gameObject;
            swordObject4 = Instantiate(swordPrefab);
            swordObject4.GetComponent<Sword>().parent = gameObject;

            bool isSide = animator.GetBool("isSide");
            bool isUp = animator.GetBool("isUp");
            bool isDown = animator.GetBool("isDown");
            
            float offset = 1.5f;
            float cooltime = 1.0f;
            
            if(attackLV[0] >= 4) {
                cooltime = 0.5f;    
                offset = 2.0f;
                swordObject1.transform.position = transform.position + new Vector3(offset, 0, 0);
                swordObject2.transform.position = transform.position + new Vector3(-offset, 0, 0);
                swordObject3.transform.position = transform.position + new Vector3(0, offset, 0);
                swordObject4.transform.position = transform.position + new Vector3(0, -offset, 0);
                swordObject1.transform.Rotate(new Vector3(0, 0, 180));
                swordObject3.transform.Rotate(new Vector3(0, 0, -90));
                swordObject4.transform.Rotate(new Vector3(0, 0, 90));

                swordObject1.transform.localScale = new Vector2(swordObject1.transform.localScale.x * 1.25f, swordObject1.transform.localScale.y * 1.25f);
                swordObject2.transform.localScale = new Vector2(swordObject2.transform.localScale.x * 1.25f, swordObject2.transform.localScale.y * 1.25f);
                swordObject3.transform.localScale = new Vector2(swordObject3.transform.localScale.x * 1.25f, swordObject3.transform.localScale.y * 1.25f);
                swordObject4.transform.localScale = new Vector2(swordObject4.transform.localScale.x * 1.25f, swordObject4.transform.localScale.y * 1.25f);
                
                yield return new WaitForSeconds(cooltime);
            }
            else{
                if(attackLV[0] >= 3) offset = 2.0f;
            
                if (isSide)
                {
                    if (isRight)
                    {
                        swordObject1.transform.position = transform.position + new Vector3(offset, 0, 0);
                        swordObject1.transform.Rotate(new Vector3(0, 0, 180));
                    }
                    else
                    {
                        swordObject1.transform.position = transform.position + new Vector3(-offset, 0, 0);
                        swordObject2.transform.Rotate(new Vector3(0, 0, 180));
                    }
                }
                else if (isUp)
                {
                    swordObject1.transform.position = transform.position + new Vector3(0, offset, 0);
                    swordObject1.transform.Rotate(new Vector3(0, 0 ,-90));
                    swordObject2.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (isDown)
                {
                    swordObject1.transform.position = transform.position + new Vector3(0, -offset, 0);
                    swordObject1.transform.Rotate(new Vector3(0, 0, 90));
                    swordObject2.transform.Rotate(new Vector3(0, 0 ,-90));
                }


                if(attackLV[0] >= 2){
                    swordObject2.transform.position = new Vector3(2*transform.position.x - swordObject1.transform.position.x, 2*transform.position.y - swordObject1.transform.position.y, 0); 
                }

                if(attackLV[0] >= 3){
                    swordObject1.transform.localScale = new Vector2(swordObject1.transform.localScale.x * 1.25f, swordObject1.transform.localScale.y * 1.25f);
                    swordObject2.transform.localScale = new Vector2(swordObject2.transform.localScale.x * 1.25f, swordObject2.transform.localScale.y * 1.25f);
                }

                yield return new WaitForSeconds(cooltime);
            }
        }
    }

    public void GetDamage(float damage)
    {
        pv.RPC("GetDamageRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    void GetDamageRPC(float damage)
    {

        HP -= damage;
        isInvincible = true;
        StartCoroutine(FlashWhite());
        StartCoroutine(InvincibilityCoolTime(1.0f));

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void addXP(float XPforPlayer)
    {
        XP += XPforPlayer;
        print("xp"+XP);
        if (XP >= XPForLevelUp)
        {
            LV += 1;
            XP -= XPForLevelUp;
            levelUpCanvas.SetActive(true);
            levelUpCanvas.GetComponent<LevelUpCanvas>().LevelUp();
        }
    }
    //
    public void addAttackLV(int idx)
    {
        attackLV[idx]++;
    }

    public void addSkillXP(float XP)
    {
        skillXP += XP;
        if (skillXP >= skillXPForLevelUp)
        {
            LV += 1;
            skillXP -= skillXPForLevelUp;
            skillLevelUpCanvas.SetActive(true);
            skillLevelUpCanvas.GetComponent<SkillLevelUpCanvas>().LevelUp();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                print(other.GetComponent<Enemy>().damage);
                GetDamage(other.GetComponent<Enemy>().damage);
            }
        }
    }

    IEnumerator FlashWhite()
    {
        spriteRenderer.material = paintWhite;

        float time = 0.5f;
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
}
