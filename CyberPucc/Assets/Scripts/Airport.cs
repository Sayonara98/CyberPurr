using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    //[SerializeField]
    //private GameObject helicopter1;
    //[SerializeField]
    //private GameObject helicopter2;
    [SerializeField]
    private Transform sky1;
    [SerializeField]
    private Transform sky2;

    [SerializeField]
    private ObjectPool helicoptersPool1;
    [SerializeField]
    private ObjectPool helicoptersPool2;

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
        GameObject helicopter1 = helicoptersPool1.GetPooledObject();
        GameObject helicopter2 = helicoptersPool2.GetPooledObject();
        if (helicopter1 != null)
        {
            Vector3 pos = new Vector3(sky1.position.x, sky1.position.y + Random.Range(-1, 2), sky1.position.z);
            helicopter1.transform.position = pos;
            helicopter1.transform.rotation = Quaternion.identity;
            helicopter1.SetActive(true);
        }
        if (helicopter2 != null)
        {
            Vector3 pos = new Vector3(sky2.position.x, sky2.position.y + Random.Range(-1, 2), sky2.position.z);
            helicopter2.transform.rotation = Quaternion.identity;
            helicopter2.transform.position = pos;
            helicopter2.SetActive(true);
        }
        canFly = false;
        yield return new WaitForSeconds(Random.Range(1, 3));
        canFly = true;
    }
}
