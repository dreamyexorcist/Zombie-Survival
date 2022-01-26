using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCamera : MonoBehaviour
{

    [SerializeField] Transform player;

    // Start is called before the first frame update
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 10, player.position.z);
    }

    
}
