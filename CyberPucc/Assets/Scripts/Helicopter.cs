using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private GameObject fragment;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private ObjectPool soldierPool;

    [SerializeField]
    private float dropSoldierCooldown = 3f;
    private float countDown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        speed *= Random.Range(3, 5);
        if (speed < 0f)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0)
        {
            StartCoroutine(dropDogs());
            countDown = dropSoldierCooldown;
        }
        transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        countDown -= Time.deltaTime;
    }

    private IEnumerator dropDogs()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        //Instantiate(soldier, transform.position, Quaternion.identity);
        GameObject obj = soldierPool.GetPooledObject();
        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<Animator>().SetBool("isHit", true);
            Instantiate(fragment, transform.position, Quaternion.identity);
            StartCoroutine(Die());
        }
        if (collision.gameObject.tag == "Boundary")
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
