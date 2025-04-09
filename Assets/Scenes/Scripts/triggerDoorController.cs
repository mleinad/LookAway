using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor1 = null;
    [SerializeField] private Animator myDoor2 = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string doorOpen1 = "DoorOpen";
    [SerializeField] private string doorClose1 = "DoorClose";

    [SerializeField] private string doorOpen2 = "DoorOpen2";
    [SerializeField] private string doorClose2 = "DoorClose2";

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == PlayerController.Instance.transform)
        {
            if(openTrigger)
            {
                myDoor1.Play(doorOpen1, 0, 0.0f);
                myDoor2.Play(doorOpen2, 0, 0.0f);
                gameObject.SetActive(false);
            }

            else if(closeTrigger)
            {
                myDoor1.Play(doorClose1, 0, 0.0f);
                myDoor2.Play(doorClose2, 0, 0.0f);
                gameObject.SetActive(false);
            }
        } 
    }
}
