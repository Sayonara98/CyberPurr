using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    [SerializeField]
    private GameObject helicopter1;
    [SerializeField]
    private GameObject helicopter2;
    [SerializeField]
    private Transform sky1;
    [SerializeField]
    private Transform sky2;

    private bool canFly = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canFly)
            StartCoroutine(generateHelicopter());
    }

    private IEnumerator generateHelicopter()
    {
        Instantiate(helicopter1, new Vector3(sky1.position.x, sky1.position.y + Random.Range(-1, 2), sky1.position.z), Quaternion.identity);
        Instantiate(helicopter2, new Vector3(sky2.position.x, sky2.position.y + Random.Range(-1, 2), sky2.position.z), Quaternion.identity);
        canFly = false;
        yield return new WaitForSeconds(Random.Range(1, 3));
        canFly = true;
    }
}
