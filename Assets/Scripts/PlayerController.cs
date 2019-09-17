﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody rig;

    private void Awake() {
        rig = GetComponent<Rigidbody>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;

        SetFacing(xInput, zInput);
    }

    private void SetFacing(float xInput, float zInput) {
        Vector3 facingDir = new Vector3(xInput, 0, zInput);

        if (facingDir.magnitude > 0) {
            transform.forward = facingDir;
        }
    }
}
