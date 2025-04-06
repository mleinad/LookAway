using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class GazeBehaviour : MonoBehaviour
{
    [Tooltip("Toggle this to use the mouse instead of eye tracking (for testing)")]
    public bool useMouseForDebug = false;

    public static GameObject CurrentGazeTarget;
    public Camera gazeCamera;

    void Start()
    {
        gazeCamera = Camera.main;
    }



    void Update()
    {
        Ray gazeRay;
        bool rayReady = false;

        if (!useMouseForDebug && TobiiAPI.IsConnected)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();

            if (!gazePoint.IsValid || !gazePoint.IsRecent())
            {
                CurrentGazeTarget = null;
                return;
            }

            gazeRay = Camera.main.ScreenPointToRay(gazePoint.Screen);
            rayReady = true;
        }
        else
        {
            gazeRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            rayReady = true;
        }

        if (rayReady)
        {
            Debug.DrawRay(gazeRay.origin, gazeRay.direction * 20f, Color.green);
        }

        RaycastHit hit;

        if (Physics.Raycast(gazeRay, out hit))
        {
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject != null)
                {
                    MeshRenderer mesh = hitObject.GetComponent<MeshRenderer>();
                    if (mesh != null)
                    {
                        Debug.Log("Hit object: " + hitObject.name);
                    }

                    CurrentGazeTarget = hitObject;
                    return;
                }
            }
        }

        CurrentGazeTarget = null;
    }
}
