using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviourPun
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode frontKey;
    public KeyCode backKey;
    public KeyCode engineOnOffKey;
    public KeyCode fireKey;
    public KeyCode secondFireKey;
    public int yaw { get; private set; }
    public float hor { get; private set; }
    public float ver{ get; private set; }
    public int drive { get; private set; }
    public bool engineOnOff { get; private set; }
    public bool fire { get; private set; }
    public bool secondFire { get; private set; }
    void Start()
    {
        engineOnOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
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
        if (Input.GetKeyDown(engineOnOffKey))
        {
            engineOnOff = !engineOnOff;
        }
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        fire = Input.GetKey(fireKey);
        secondFire = Input.GetKey(secondFireKey);
    }
}
