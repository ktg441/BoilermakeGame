﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWheel : MonoBehaviour
{

    public Transform target;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(target.transform.position);
    }
}
