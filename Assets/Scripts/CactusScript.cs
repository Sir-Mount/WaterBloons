using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CactusScript : MonoBehaviour
{
   [SerializeField]
   private GameObject player;

   [SerializeField] private float shootingMTTH;

   [SerializeField] private GameObject projectilePrefab;

   [SerializeField] private float damageSeconds;

   private enum states
   {
      Normal,
      Damaged,
      Dead
   }

   private states currentState = states.Normal;

   private void Start()
   {
      StartCoroutine(shoot(0));
      StartCoroutine(shoot(1));
   }

   void Update()
   {
      if (currentState == states.Dead) return;
      
      Vector3 newtarget = player.transform.position;
      newtarget.y = transform.position.y;
     
      transform.LookAt(newtarget);
   }
   
   IEnumerator shoot(int childNumber)
   {
      if (currentState == states.Dead) yield break;
      GameObject newProjectile = Instantiate(projectilePrefab, transform.GetChild(childNumber).transform.position + transform.forward, transform.rotation);
      
      Vector3 newtarget = player.transform.position;
      newProjectile.transform.LookAt(newtarget);
      
      yield return new WaitForSeconds(Random.Range(shootingMTTH * 0.8f, shootingMTTH * 1.2f));
      StartCoroutine(shoot(childNumber));
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         if (currentState == states.Normal)
         {
            currentState = states.Damaged;
            StartCoroutine(waitToHeal());
         }
         else if (currentState == states.Damaged)
         {
            currentState = states.Dead;
         }
      }
   }

   IEnumerator waitToHeal()
   {
      yield return new WaitForSeconds(damageSeconds);
      if (currentState == states.Dead) yield break;
      
      currentState = states.Normal;
   }
}
