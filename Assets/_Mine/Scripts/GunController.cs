using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunController : MonoBehaviour
{
    [SerializeField] Rigidbody2D bullet;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] bool canShoot = true;

    Transform pivot;
    Transform cam;

    void Start()
    {
        pivot = transform.parent;
        cam = Camera.main.transform;
    }

    void Update()
    {
        Vector2 camPos = new Vector2(cam.localPosition.x * Screen.width * 0.04f, cam.localPosition.y * Screen.height * 0.06f);
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        Vector2 aimPos = mousePos + camPos;
        float aimAngle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        pivot.localRotation = Quaternion.Euler(0, 0, aimAngle);

        if ((Input.GetButton("Fire1") || Input.GetAxis("Trigger") > 0) && canShoot)
        {
            Shoot();
            StartCoroutine(FireRate(fireRate));
        }
    }

    void Shoot()
    {
        Rigidbody2D instance = Instantiate(bullet, spawnPoint.position, spawnPoint.localRotation);
        instance.velocity = transform.right * 100;
    }

    IEnumerator FireRate(float rate)
    {
        canShoot = false;
        yield return new WaitForSeconds(rate);
        canShoot = true;
    }
}
