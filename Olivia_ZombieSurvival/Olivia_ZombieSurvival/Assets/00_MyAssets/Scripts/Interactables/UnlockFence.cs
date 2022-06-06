using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFence : MonoBehaviour
{    
    [SerializeField] private GameObject openFence;
    [SerializeField] private FirstPersonController fpsController;

    

    private void Start()
    {
        openFence.SetActive(false);
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (fpsController != null)
        {
            openFence.SetActive(true);
            Destroy(this.gameObject);
        }
    }

   

}
