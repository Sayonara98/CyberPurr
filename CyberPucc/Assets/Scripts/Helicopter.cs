using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private GameObject soldier;

    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        speed *= Random.Range(3, 5);
        if (speed < 0f)
            GetComponent<SpriteRenderer>().flipX = true;
        StartCoroutine(dropDogs());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
    }

    private IEnumerator dropDogs()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        Instantiate(soldier, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<Animator>().SetBool("isHit", true);
        }
    }
}
