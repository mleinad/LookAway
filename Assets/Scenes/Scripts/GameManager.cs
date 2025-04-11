using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GazeItemCollector gazeItemCollector;

    [SerializeField]
    BoxCollider boxCollider;

    private void Start()
    {
        boxCollider.enabled = false;
    }

    private void Update()
    {
        if (gazeItemCollector.state)
        {
            boxCollider.enabled = true;
        }
    }
}
