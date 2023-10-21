using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject target; // Target GameObject to follow
    public float zOffset = 4.0f; // Z offset

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Follow the target GameObject with a Z offset
            Vector3 newPosition = target.transform.position;
            newPosition.z += zOffset;
            transform.position = newPosition;
        }
    }
}
