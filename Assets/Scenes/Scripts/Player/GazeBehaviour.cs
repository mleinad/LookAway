using UnityEngine;
using Tobii.Gaming;

public class GazeBehaviour : MonoBehaviour
{

    public static GameObject CurrentGazeTarget;

    [SerializeField]
    private string objectName;
    void Update()
    {
        Ray gazeRay;
        bool rayReady = false;

        if (TobiiAPI.IsConnected)
        {
            
            GazePoint gazePoint = TobiiAPI.GetGazePoint();

            if (!gazePoint.IsValid || !gazePoint.IsRecent())
            {
                CurrentGazeTarget = null;
                return;
            }
            
            gazeRay = Camera.main.ScreenPointToRay(gazePoint.Screen);

        
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
                            //Debug.Log("Hit object: " + hitObject.name);
                        }

                        objectName = CurrentGazeTarget?.ToString();
                        CurrentGazeTarget = hitObject;
                        return;
                    }
                }
            }

        }

        CurrentGazeTarget = null;
    }
    
}
