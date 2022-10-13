using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
{
    private static volatile T instance;

    public static T Instance//property - Instance oluşturuyoruz
    {
        get
        {
            if (instance == null)//instace null ise;
            {
                instance = FindObjectOfType(typeof(T)) as T;//sahnedeki objenin tipine göre bulup instance'a ata
            }
            
            return instance;//instance'ı döndür
        }
    }
}
