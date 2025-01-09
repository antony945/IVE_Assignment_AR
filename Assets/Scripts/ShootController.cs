using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.IO;
using TMPro;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletSpawnPoint;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    InputAction touchInput;

    GameObject spawnedObject;
    int spawnedObjectCount = 0;

    [SerializeField]
    GameObject timerController;

    private string logEntry = string.Empty;

    private void Awake()
    {
        touchInput.performed += ctx =>
        {
            if (!GetComponent<Helper>().IsInputOverUI(ctx))
                Shoot();
        };
    }

    private void OnEnable()
    {
        touchInput.Enable();
        timerController.SetActive(true);
    }

    private void OnDisable()
    {
        touchInput.Disable();
        //timerController.SetActive(false); # comment otherwise timer restart every time
    }

    void Shoot()
    {
        spawnedObject = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        // Shoot the bullet by adding velocity to the rigigbody component of the bullet
        spawnedObject.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.transform.forward * bulletSpeed;

        spawnedObjectCount++;

        logEntry = $"Count Shot (ShootController),{spawnedObjectCount},{DateTime.Now} \n";
        File.AppendAllText(GameController.File_Path, logEntry);
    }
}
