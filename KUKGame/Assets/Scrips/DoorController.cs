using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool doorOpen = false;
    Animator animater;
    public GameObject door;

    private void Start()
    {
        animater = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animater.SetBool("DoorOpen", doorOpen);
    }
}
