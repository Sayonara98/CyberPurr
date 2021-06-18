using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StartCoroutine(CleanFragment());
        }
    }

    private IEnumerator CleanFragment()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }    
}
