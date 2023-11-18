using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_TEMP_v2 : MonoBehaviour {
    private Rigidbody m_RigidBody;

    private bool m_IsJumpPressed = false, m_OnGround = true;

    [SerializeField]
    private float m_JumpHeight = 10.0f, m_Speed = 10.0f;

    void Start() {
        m_RigidBody = GetComponent<Rigidbody>();

        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX |
                                  RigidbodyConstraints.FreezeRotationY |
                                  RigidbodyConstraints.FreezeRotationZ |
                                  RigidbodyConstraints.FreezePositionZ;

        m_RigidBody.useGravity = true;
    }

    void Update() {
        m_IsJumpPressed = Input.GetButtonDown("Jump");

        if(m_IsJumpPressed && m_OnGround) {
            m_RigidBody.AddForce(new Vector3(0, m_JumpHeight, 0), ForceMode.Impulse);
            m_OnGround = false;
        }
    }

    void FixedUpdate() {
        float curSpeed = m_Speed * Input.GetAxis("Horizontal");

        gameObject.transform.position = new Vector3(gameObject.transform.position.x + curSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            m_OnGround = true;
            Debug.Log("Grounded");
        }
    }
}