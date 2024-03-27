using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPeteScript : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Child");
            transform.parent.GetComponent<CactusScript>().CollisionDetected(collision);
        }
    }