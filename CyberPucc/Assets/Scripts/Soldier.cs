using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private float parachuteSpeed = 1.0f;

    private bool onTheGround = false;

    private bool isHitByBullet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onTheGround)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 moveToPlayerDir = player.transform.position - transform.position;
            transform.Translate(moveToPlayerDir.normalized * Time.deltaTime);
            if (transform.position.x > player.transform.position.x)
                GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (isHitByBullet)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else
        {
            transform.Translate(parachuteSpeed * Time.deltaTime * Vector3.down);
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (isHitByBullet == true)
            {
                StartCoroutine(Die());
            }
            else if (onTheGround == false)
            {
                onTheGround = true;
                GetComponent<Animator>().SetBool("onTheGround", onTheGround);
            }
        }
        
        if (collision.gameObject.tag == "Bullet")
        {
            isHitByBullet = true;
            respawn();
            Score.score++;
        }
    }

    private IEnumerator Die()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    public void respawn()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().isTrigger = false;
        isHitByBullet = false;
        onTheGround = false;
        GetComponent<Animator>().SetBool("onTheGround", onTheGround);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        respawn();
    }
}
