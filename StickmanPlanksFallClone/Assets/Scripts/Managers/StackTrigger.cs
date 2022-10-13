using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTrigger : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)//İki nesne çarpışıp triggerlandığında;
    {
        if (other.tag == "Brick")//Çarptığımız objenin tag'i "Brick" ise;
        {
            StackManager.instance.PickUp(other.gameObject, true, "Untagged");
            //StackManager içinde ki PickUp fonksiyonunu çalıştır
        }
    }
}
