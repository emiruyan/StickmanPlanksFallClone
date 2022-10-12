using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public Vector3 _offset;


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + _offset, Time.deltaTime *  5);
    }
}
