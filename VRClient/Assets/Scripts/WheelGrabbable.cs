﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGrabbable : OVRGrabbable
{

    public Transform handler;

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(Vector3.zero, Vector3.zero);
        Debug.Log("Grand End");
        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;

        Rigidbody rbhandler = handler.GetComponent<Rigidbody>();
        rbhandler.velocity = Vector3.zero;
        rbhandler.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (Vector3.Distance(handler.position, transform.position) > 0.4f)
        {
            //grabbedBy.ForceRelease(this);
        }
    }
}
