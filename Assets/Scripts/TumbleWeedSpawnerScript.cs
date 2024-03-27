using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TumbleWeedSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject tumbleWeebPrefab;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float tumbleSpeed;
    
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        GameObject newTumbleWeed = Instantiate(tumbleWeebPrefab, generateLocation(), transform.rotation);
        newTumbleWeed.transform.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * tumbleSpeed, ForceMode.Impulse);
        Destroy(newTumbleWeed, 30);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(spawn());
    }

    private Vector3 generateLocation()
    {
        return new Vector3(Random.Range(-20, 20), 1, transform.position.z);
    }
}
