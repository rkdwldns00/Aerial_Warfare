using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput inputSys;
    UImanager ui;
    public float minSpeed;
    public float maxSpeed;
    public float maxHeight;
    float realspeed;
    float speed
    {
        get { return realspeed; }
        set {
            if(value > maxSpeed) {
                realspeed = maxSpeed;
            }
            else if(value < minSpeed)
            {
                realspeed = minSpeed;
            }
            else
            {
                realspeed = value;
            }
            
        }
    }
    void Start()
    {
        inputSys = GetComponent<PlayerInput>();
        speed = maxSpeed;
        ui = FindObjectOfType<UImanager>();
        ui.speedUI(speed, maxSpeed);
        ui.heightUI(transform.position.y, maxHeight);
        //rigid = GetComponent<Rigidbody>();
        //rigid.AddForce(transform.rotation * new Vector3(0, 10f, 250f));
    }

    // Update is called once per frame
    void Update()
    {
        if (inputSys.drive * Time.deltaTime * (maxSpeed - minSpeed) != 0f)
        {
            speed += inputSys.drive * Time.deltaTime * (maxSpeed - minSpeed) / 5;
            ui.speedUI(speed, maxSpeed);
        }
        ui.heightUI(transform.position.y, maxHeight);
        transform.position += transform.rotation * Vector3.forward * Time.deltaTime * speed / 2;
        Debug.DrawLine(transform.position, transform.position + transform.rotation * Vector3.forward * 50);
        transform.Rotate(new Vector3(inputSys.ver * 90, inputSys.yaw * 30, -inputSys.hor * 60) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //rigid.AddForce(transform.rotation * new Vector3(0, 4, 50f));
        //Debug.Log(rigid.velocity);
    }
}
