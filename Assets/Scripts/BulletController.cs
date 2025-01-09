using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameController gameController;
    
    private string logEntry = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        // Destroy the bullet after 3 seconds if it doesn't hit anything
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hit an enemy or ally, and update the score accordingly.
        var whois = collision.gameObject.tag;
        Debug.Log("Hitted a object: " + whois);

        if (whois != "PlacementIndicator")
        {
            // Destroy collided object.
            Destroy(collision.gameObject);

            if (whois == "Enemy")
            { // If the bullet hits an enemy, handle that
                gameController.HandleHit();
            }
            else if (whois == "Ally")
            { // If the bullet hits an ally, handle that
                gameController.HandleMiss();
            }
            else
            { // If the bullet hits anything else, log it.
                Debug.Log("Unknown object: " + whois);
            }

            // Update UI
            gameController.UpdateUI();
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}
