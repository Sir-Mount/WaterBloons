using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeteColliderScript : MonoBehaviour
{
   CactusScript parent;
   void Start() {
      parent = GetComponentInParent<CactusScript>();
   }

   void OnCollisionEnter(Collision collision) {
      parent.TouchPlayer(collision);
   }
}
