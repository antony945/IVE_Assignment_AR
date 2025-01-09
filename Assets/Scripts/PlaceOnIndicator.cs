using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject placementIndicator;

    [SerializeField]
    List<GameObject> placedPrefabs = new List<GameObject>();

    GameObject spawnedObject;

    [SerializeField]
    InputAction touchInput;

    [SerializeField]
    Button playButton;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField]
    GameController gameController;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        
        touchInput.performed += ctx =>
        {
            if (!GetComponent<Helper>().IsInputOverUI(ctx))
                MultiPlaceObject();
        };
        
        placementIndicator.SetActive(false);
        playButton.interactable = false;
    }

    private void OnEnable()
    {
        touchInput.Enable();
    }

    private void OnDisable()
    {
        touchInput.Disable();
        placementIndicator.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            placementIndicator.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

            if (!placementIndicator.activeInHierarchy)
            {
                placementIndicator.SetActive(true);
            }
        }
        else
        {
            // This hides indicator while plane not detected in the center of the screen
            placementIndicator.SetActive(false);
        }
    }

    private void Spawn()
    {

        // Decide randomly which prefab to spawn
        int index = Random.Range(0, placedPrefabs.Count);

        // Check if there is already a gameObject there
        if (Physics.OverlapSphere(placementIndicator.transform.position, 0.01f).Length > 0)
        {
            return;
        }

        spawnedObject = Instantiate(placedPrefabs[index], placementIndicator.transform.position, placementIndicator.transform.rotation);

        // Make the object always look at the camera
        Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
        lookPos.y = 0;
        spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);

        // Check tag of spawned object
        if (spawnedObject.CompareTag("Enemy"))
        {
            //Enable the button if at least one enemy
            playButton.interactable = true;
            gameController.HandleEnemySpawn();
        }
        else if (spawnedObject.CompareTag("Ally"))
        {
            gameController.HandleAllySpawn();
        }
    }

    void SinglePlaceObject()
    {
        if (!placementIndicator.activeInHierarchy)
            return;

        if (spawnedObject == null)
        {
            Spawn();
        }
        else
        {
            spawnedObject.transform.SetPositionAndRotation(placementIndicator.transform.position, placementIndicator.transform.rotation);
        }

    }

    void MultiPlaceObject()
    {
        if (!placementIndicator.activeInHierarchy)
            return;

        Spawn();
    }
}
