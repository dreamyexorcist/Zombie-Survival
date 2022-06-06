using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    [SerializeField] private GameObject spawningZombies;
    [SerializeField] private FirstPersonController fpsController;



    private void Start()
    {
        spawningZombies.SetActive(false);
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (fpsController != null)
        {
            spawningZombies.SetActive(true);
            //Destroy(this.gameObject);
        }
    }
}
