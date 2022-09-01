using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    PlayerInput inputSys;
    UImanager ui;
    Rigidbody rigid;
    public Transform cameraPos;
    public GameObject firstFire;
    public GameObject secondFire;
    public Transform[] firstFireTransform;
    public Transform[] secondFireTransform;
    public float minSpeed;
    public float maxSpeed;
    public float maxHeight;
    public float reloadTime;
    public float secondReloadTime;
    float reloadCheck;
    float secondReloadCheck;
    float realspeed;
    int firePosIndex = 0;
    int secondFirePosIndex = 0;
    float speed
    {
        get { return realspeed; }
        set
        {
            if (value > maxSpeed)
            {
                realspeed = maxSpeed;
            }
            else if (value < minSpeed)
            {
                realspeed = minSpeed;
            }
            else
            {
                realspeed = value;
            }

        }
    }
    void Awake()
    {
        inputSys = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        //StartCoroutine(onOff(1f));
        //rigid = GetComponent<Rigidbody>();
        //rigid.AddForce(transform.rotation * new Vector3(0, 10f, 250f));
    }

    private void OnEnable()
    {
        ui = FindObjectOfType<UImanager>();
        rigid.velocity = Vector3.zero;
        speed = (maxSpeed + minSpeed) / 2;
        if (photonView.IsMine)
        {
            ui.speedUI(speed, maxSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (inputSys.drive * (maxSpeed - minSpeed) != 0f)
            {
                speed += inputSys.drive * Time.deltaTime * (maxSpeed - minSpeed) / 5;
                ui.speedUI(speed, maxSpeed);
            }
            ui.heightUI(transform.position.y, maxHeight);
        }
        if (inputSys.engineOnOff)
        {
            move(transform.rotation * Vector3.forward * Time.deltaTime * speed / 2);
        }
        else
        {
            rigid.velocity += Vector3.down * 50f * Time.deltaTime;
            rigid.velocity *= 1-Time.deltaTime / 3f;
        }
        transform.Rotate(new Vector3(inputSys.ver * 90, inputSys.yaw * 30, -inputSys.hor * 60) * Time.deltaTime * 2f);
        reloadCheck -= Time.deltaTime;
        secondReloadCheck -= Time.deltaTime;
        if (inputSys.fire && reloadCheck <= 0f)
        {
            reloadCheck = reloadTime;
            firePosIndex++;
            if (firePosIndex == firstFireTransform.Length)
            {
                firePosIndex = 0;
            }
            GameObject bullet;
            bullet = PhotonNetwork.Instantiate(firstFire.name, firstFireTransform[firePosIndex].position, firstFireTransform[firePosIndex].rotation);
            bullet.GetComponent<Bullet>().tag = transform.tag;
        }

        if (inputSys.secondFire && secondReloadCheck <= 0f)
        {
            secondReloadCheck = secondReloadTime;
            secondFirePosIndex++;
            if (secondFirePosIndex == secondFireTransform.Length)
            {
                secondFirePosIndex = 0;
            }
            GameObject bullet;
            bullet = PhotonNetwork.Instantiate(secondFire.name, secondFireTransform[secondFirePosIndex].position, secondFireTransform[secondFirePosIndex].rotation);
            if (bullet.GetComponent<Bullet>() != null)
            {
                bullet.GetComponent<Bullet>().tag = transform.tag;
            }
            else if (bullet.GetComponent<Missile>() != null)
            {
                bullet.GetComponent<Missile>().tag = transform.tag;
            }
        }

        
    }

    void move(Vector3 trans)
    {
        trans *= 100;
        Debug.Log(trans.z+"  "+rigid.velocity.z);
        if(Mathf.Abs(rigid.velocity.x) > Mathf.Abs(trans.x) && rigid.velocity.x * trans.x > 0f)
        {
            trans.x = 0;
        }
        if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(trans.y) && rigid.velocity.y * trans.y > 0f)
        {
            trans.y = 0;
        }
        if (Mathf.Abs(rigid.velocity.z) > Mathf.Abs(trans.z) && rigid.velocity.z * trans.z > 0f)
        {
            trans.z = 0;
        }
        trans.y += Mathf.Cos((speed - (maxSpeed + minSpeed) / 2) / (maxSpeed - minSpeed) * 4) * Time.deltaTime * maxSpeed * 0.02f;
        trans *= Mathf.Cos(transform.rotation.x / 90);
        if (transform.position.y >= maxHeight * 0.8f && trans.y > 0f)
        {
            trans.y *= (-transform.position.y + maxHeight) / (maxHeight * 0.2f);
        }
        //transform.position+=trans;
        rigid.AddForce(trans * 100);
    }
}
