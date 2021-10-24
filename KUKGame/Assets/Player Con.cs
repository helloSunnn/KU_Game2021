using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;
        float newZ = transform.position.z + 0.01f;
        transform.position = new Vector3(newX, newY, newZ);

    }
}
