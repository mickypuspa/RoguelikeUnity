using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Transform trackingTarget;

    // Update is called once per frame
    void Update()
    {
        if (trackingTarget != null)
        {
            transform.position = new Vector3(trackingTarget.position.x,
             trackingTarget.position.y, transform.position.z);
        }
    }
}
