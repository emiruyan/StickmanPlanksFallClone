using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick")
        {
            StackManager.instance.PickUp(other.gameObject, true, "Untagged");
        }
    }
}
