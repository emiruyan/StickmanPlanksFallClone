using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private Touch _touch;//Sadece simulator'de çalışır
   
    private Vector3 _touchDown;
    private Vector3 _touchUp;

    private bool _dragStarted;//Sürükleme başladı mı
    private bool _isMoving;


    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        if (animator)
        {
            animator.SetBool("isRunning",_isMoving);
        }
        
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
         
            if (_touch.phase == TouchPhase.Began)//Dokunma başlıyor ise
            {
                _dragStarted = true;
                _isMoving = true;
                _touchDown = _touch.position;
                _touchUp = _touch.position;
            }

            if (_dragStarted) //Ekrana parmak sürme işlemi başladıysa;
            {
                if (_touch.phase == TouchPhase.Moved)//Ekrana dokunup hareket ettiriyor ise;
                {
                    _touchDown = _touch.position;
                    
                }

                if (_touch.phase == TouchPhase.Ended)//Ekrana dokunup elini çekiyor ise;
                {
                    _touchDown = _touch.position;
                    _isMoving = false;
                    _dragStarted = false;
                }

                gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(),
                    rotateSpeed * Time.deltaTime);
                gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
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
        temp.z = temp.y;
        temp.y = 0;
        return temp;
    }
}
