using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{     


    public float speed = 1f;
    public float rotSpeed = 20f;
   
    void Update()
    {   float newRoty = 0;
        float newX = transform.position.x; 
        float newY = transform.position.y;
        float newZ = transform.position.z;
   

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newZ = transform.position.z + speed * Time.deltaTime;
            newRoty = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            newZ = transform.position.z - speed * Time.deltaTime;
            newRoty = 180;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newX = transform.position.x - speed * Time.deltaTime;
            newRoty = -90;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newX = transform.position.x + speed * Time.deltaTime;
            newRoty = 90;
        }

        transform.position = new Vector3(newX, newY, newZ);
     //   transform.rotation = Quaternion.Euler(0, newRoty, 0);
        transform.rotation = Quaternion.Lerp(
            Quaternion.Euler(0, newRoty, 0),
            transform.rotation,
            Time.deltaTime * rotSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if(collision.gameObject.name == "Sphere")
        {
            transform.localScale = new Vector3(2, 2, 2);

        }
        if (collision.gameObject.name == "Cube")
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
