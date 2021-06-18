using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField]
    private GameObject turretPos;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float fireSpeed = 10f;
    private bool canShoot = true;

    private Camera cam;
    private Vector3 mousePos;
    private float angle;
    private bool isGameOver = false;

    [SerializeField]
    private ObjectPool objectsPool;


    public void Replay()
    {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButton(0) && canShoot)
            {
                StartCoroutine(Shoot());
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Soldier")
        {
            gameManager.GameOver();
            isGameOver = true;
        }
    }
}
