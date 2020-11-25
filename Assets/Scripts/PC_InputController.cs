using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(MovementControler))]
public class PC_InputController : MonoBehaviour
{

    MovementControler _playerMovement;
    DateTime _strikeClickTime;
    float _move;
    bool _jump;
    bool _crawling;
    bool _canAtak;

    private void Start()
    {
        _playerMovement = GetComponent<MovementControler>();
    }

    // Update is called once per frame
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");     //Движение по клавишам
        if (Input.GetButtonUp("Jump")) //прижок
        {
            _jump = true;
        }

        _crawling = Input.GetKey(KeyCode.C); // Проверка на присидание po klave

        if(Input.GetKey(KeyCode.E))
        {
            _playerMovement.StartCasting();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            _strikeClickTime = DateTime.Now;
            _canAtak = true;
        }

        if (Input.GetButtonUp("Fire1")) //удар
        {
            float holdTime = (float)(DateTime.Now - _strikeClickTime).TotalSeconds;
            if (_canAtak)
            {
                _playerMovement.StartStrike(holdTime);
            }
            _canAtak = false;
        }

        if ((DateTime.Now - _strikeClickTime).TotalSeconds >= _playerMovement.ChargeTime * 1 && _canAtak) // сил удар 
        {
            _playerMovement.StartStrike(_playerMovement.ChargeTime);
            _canAtak = false;
        }


    }

    private void FixedUpdate()
    {
        _playerMovement.Move(_move, _jump, _crawling);
        _jump = false;  
    }

}
