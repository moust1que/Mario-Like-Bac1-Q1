using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
    [SerializeField] private float _speedX = 10.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _speedY = 0.25f;
    private const float _gravity = -9.81f;

    void Update() {
        CharacterController controller = GetComponent<CharacterController>();

        Vector3 forward = transform.TransformDirection(Vector3.right);
        float curSpeed = _speedX * UnityEngine.Input.GetAxis("Horizontal");
        controller.SimpleMove(forward * curSpeed);

        Vector3 jump = transform.TransformDirection(Vector3.up);

        if(IsGrounded() && UnityEngine.Input.GetKey(KeyCode.Space)) {
            Vector3 movementVelocity = new Vector3();
            movementVelocity.y = Mathf.Sqrt(_speedY * _jumpHeight * _gravity * -1.0f);
            movementVelocity.y *= 1.0f;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, movementVelocity.y, gameObject.transform.position.z);
        }else {

        }
    }

    private bool IsGrounded() {
        if(gameObject.transform.position.y > 1.1f)
            return false;

        return true;
    }
}
