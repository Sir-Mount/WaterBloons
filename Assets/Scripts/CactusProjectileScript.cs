using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusProjectileScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TODO: Do damage
        }
        Destroy(gameObject);
    }
}
