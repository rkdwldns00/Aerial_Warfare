using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode frontKey;
    public KeyCode backKey;
    public int yaw { get; private set; }
    public float hor { get; private set; }
    public float ver{ get; private set; }
    public int drive { get; private set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey))
        {
            yaw = -1;
        }else if (Input.GetKey(rightKey))
        {
            yaw = 1;
        }
        else
        {
            yaw=0; 
        }
        if (Input.GetKey(frontKey))
        {
            drive = 1;
        }
        else if (Input.GetKey(backKey))
        {
            drive = -1;
        }
        else
        {
            drive = 0;
        }
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }
}
