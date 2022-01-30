using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float lerpRateMove;
    public GameObject avatar;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, AutoAdjustPosition(), lerpRateMove);
    }

    Vector3 AutoAdjustPosition()
    {
        return new Vector3(avatar.transform.position.x, avatar.transform.position.y, transform.position.z);
    }
}