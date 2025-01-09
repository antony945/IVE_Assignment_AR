using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Helper : MonoBehaviour
{
    // Helper function to check if a position is over a UI element
    bool IsPointerOverUI(Vector2 position)
    {
        // Create a pointer event data to simulate the event
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = position
        };

        // Perform a raycast to check UI elements
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        //// Debug the results
        //if (results.Count > 0)
        //{
        //    Debug.Log("UI Elements hit by raycast:");
        //    foreach (var result in results)
        //    {
        //        Debug.Log($"- Name: {result.gameObject.name}, Layer: {LayerMask.LayerToName(result.gameObject.layer)}");
        //    }
        //}
        //else
        //{
        //    Debug.Log("No UI elements hit by raycast.");
        //}

        // Return true if there is any result, meaning the position is over a UI element
        return results.Count > 0;
    }

    // Helper function to check if the input ctx is over a UI element
    public bool IsInputOverUI(InputAction.CallbackContext  ctx)
    {
        // Check if there is UI element there
        if (ctx.control.device is Pointer device)
        {
            // Position of touching
            Vector3 touchPos = device.position.ReadValue();

            return IsPointerOverUI(touchPos);
        }

        return false;
    }
}
