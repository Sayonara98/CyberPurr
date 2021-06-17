using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private GameObject turretPos;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float fireSpeed = 8f;
    private bool canShoot = true;

    private Camera cam;
    private Vector3 mousePos;
    private float angle;

    [SerializeField]
    private ObjectPool objectsPool;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = mousePos - turretPos.transform.position;
        lookDir.y = (lookDir.y < 0) ? 0 : lookDir.y;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        turretPos.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90f);
    }

    private IEnumerator Shoot()
    {
        //GameObject missile = Instantiate(bulletPrefab, turretPos.transform.position, turretPos.transform.rotation) as GameObject;
        //missile.transform.Translate(0.0f, 0.4f, 0.0f);
        GameObject missile = objectsPool.GetPooledObject();
        if (missile != null)
        {
            missile.transform.position = turretPos.transform.position;
            missile.transform.rotation = turretPos.transform.rotation;
            missile.SetActive(true);

        }
        Rigidbody2D bulletRb = missile.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(turretPos.transform.up * fireSpeed, ForceMode2D.Impulse);
        canShoot = false;
        turretPos.SetActive(false);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        turretPos.SetActive(true);
    }
}
