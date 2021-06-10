using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private GameObject turretPos;
    [SerializeField]
    private GameObject bullet;

    private Camera cam;
    private Vector3 mousePos;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = mousePos - turretPos.transform.position;
        lookDir.y = (lookDir.y < 0) ? 0 : lookDir.y;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        turretPos.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90f);
    }
}
