using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public Transform target;
    public float smoothvalue = 5;
    public Vector3 offset;

    // Start is called before the first frame update

    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, Time.deltaTime * smoothvalue);
    }
}
