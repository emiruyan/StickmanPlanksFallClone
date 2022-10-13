using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
  public static StackManager instance;//Singleton

  [SerializeField] private float distanceBetweenObjects;//Yerden toplayacağımız ve sahip olduğumuz objenin mesafesi 
  [SerializeField] private Transform prevObject;//Yerden topladığımızdan bir önceki obje
  [SerializeField] private Transform parent;
  
  private void Awake()
  {
    if (instance == null)//Singleton
    { 
      instance = this;
    }
    
  }


  private void Start()
  {
    distanceBetweenObjects = prevObject.localScale.y;//sahip olduğumuz objenin localScale.y'sini distanceBetweenObjects'e atadık
  }

  public void PickUp(GameObject pickedObject, bool needTag = false, string tag = null, bool downOrUp=true)
  {
    //pickedObject = yerdeki çarptığımız obje
    //needTag = Tag'ı değişsin mi? (StackTrigger'da değiştiriyoruz)
    //downOrOp = Topladığımız objeler aşağı doğru mu yukarı doğru mu çoğalacak ?
    
    if (needTag)//needTag true ise;
    {
      pickedObject.tag = tag; //topladığımız objenin tag'ini StackTrigger'da girdiğimiz tag'a çeviriyoruz
    }

    pickedObject.transform.parent = parent;//Topladığımız objeleri Hierarchy üzerinde ki parent objemize atıyoruz
    Vector3 desPos = prevObject.localPosition;//Sahip olduğumuz objenin localPos'unu desPos'a atıyoruz
    desPos.y += downOrUp ? distanceBetweenObjects : -distanceBetweenObjects;//desPos'un y sini koşula göre arttırıyoruz

    pickedObject.transform.localPosition = desPos;//desPos'u topladığımız objenin transformunun localPos'una atıyoruz
    prevObject = pickedObject.transform;//topladığımız objeyi prevObject yapıyoruz (Son Obje)
  }
}
