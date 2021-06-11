using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private float parachuteSpeed = 1.0f;

    private bool onTheGround = false;

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
        else
        {
            transform.Translate(parachuteSpeed * Time.deltaTime * Vector3.down);
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (onTheGround == false)
            {
                onTheGround = true;
                GetComponent<Animator>().SetBool("onTheGround", onTheGround);
            }
        }
        if (collision.gameObject.tag == "Soldier")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
