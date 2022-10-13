using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float moveSpeed;//hareket hızı
    [SerializeField] private float rotateSpeed;//dönüş hızı

    private Touch _touch;//Sadece simulator'de çalışan bir Touch değişkeni oluşturuyoruz
   
    private Vector3 _touchDown;//İlk dokunuş
    private Vector3 _touchUp;//Son dokuşunuş

    private bool _dragStarted;//Sürükleme başladı mı?
    private bool _isMoving;//Hareket ediyor mu?


    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        HandlePlayerInput();
        PlayerDeath();
    }

    private void HandlePlayerInput()
    {
        if (animator)
        {
            animator.SetBool("isRunning",_isMoving);
        }
        
        if (Input.touchCount > 0)//touchCount 0'dan büyük ise; (Ekrana dokunma işlemi varsa)
        {
            _touch = Input.GetTouch(0);//İlk dokunuluan touch bizim tanımladığımız touch'a atandı
         
            if (_touch.phase == TouchPhase.Began)//Dokunma başlıyor ise
            {
                _dragStarted = true;//Sürtünme işledi başladı
                _isMoving = true;//Hareket başladı
                _touchDown = _touch.position;//Dokunulan yerin pozisyonunu alıyop Down'a atıyoruz
                _touchUp = _touch.position;//Dokunulan yerin pozisyonunu alıyop Up'a atıyoruz
            }

            if (_dragStarted) //Ekrana parmak sürme işlemi başladıysa;
            {
                if (_touch.phase == TouchPhase.Moved)//Ekrana dokunup hareket ettiriyor ise;
                {
                    _touchDown = _touch.position;//Hareket ettiği sürece touch.position'u touchDown'a atıyoruz
                    
                }

                if (_touch.phase == TouchPhase.Ended)//Ekrana dokunup elini çekiyor ise;
                {
                    _touchDown = _touch.position;
                    _isMoving = false;//Karakter hareket etmiyor
                    _dragStarted = false;//Sürtünme bitti
                }

                gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(),
                    rotateSpeed * Time.deltaTime);//Player'ın nereden nereye döeneceği
                gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//Player hareketi  
            }
        }
    }
    
    Quaternion CalculateRotation()
    {
        Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return temp;
    }

    Vector3 CalculateDirection()
    {
        Vector3 temp = (_touchDown - _touchUp).normalized;
        temp.z = temp.y;//y değerini z'ye atıyoruz
        temp.y = 0;//y'yi 0'a atıyoruz
        return temp;
    }

    private void PlayerDeath()
    {
        if (transform.localPosition.y <= -3)
        {
         GameManager.Instance.PlayAgain();
                
        }
    }
}
