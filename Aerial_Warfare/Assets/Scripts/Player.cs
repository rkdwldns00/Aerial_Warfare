using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput inputSys;
    UImanager ui;
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
    void Awake()
    {
        ui = FindObjectOfType<UImanager>();
        inputSys = GetComponent<PlayerInput>();
        //StartCoroutine(onOff(1f));
        //rigid = GetComponent<Rigidbody>();
        //rigid.AddForce(transform.rotation * new Vector3(0, 10f, 250f));
    }

    private void OnEnable()
    {
        speed = (maxSpeed + minSpeed) / 2;
        if (transform.CompareTag("team1"))
        {
            ui.speedUI(speed, maxSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("team1")) {
            if (inputSys.drive * (maxSpeed - minSpeed) != 0f)
            {
                speed += inputSys.drive * Time.deltaTime * (maxSpeed - minSpeed) / 5;
                ui.speedUI(speed, maxSpeed);
            }
            ui.heightUI(transform.position.y, maxHeight);
        }
        move(transform.rotation * Vector3.forward * Time.deltaTime * speed / 2);
        transform.Rotate(new Vector3(inputSys.ver * 90, inputSys.yaw * 30, -inputSys.hor * 60) * Time.deltaTime);
        reloadCheck -= Time.deltaTime;
        secondReloadCheck -= Time.deltaTime;
        if (inputSys.fire && reloadCheck<=0f)
        {
            reloadCheck = reloadTime;
            firePosIndex++;
            if(firePosIndex == firstFireTransform.Length)
            {
                firePosIndex = 0;
            }
            GameObject bullet;
            bullet = Instantiate(firstFire, firstFireTransform[firePosIndex]);
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
            bullet = Instantiate(secondFire, secondFireTransform[secondFirePosIndex].position, secondFireTransform[secondFirePosIndex].rotation);
            if (bullet.GetComponent<Bullet>() != null)
            {
                bullet.GetComponent<Bullet>().tag = transform.tag;
            }else if(bullet.GetComponent<Missile>() != null){
                bullet.GetComponent<Missile>().tag = transform.tag;
            }
        }
    }

    void move(Vector3 trans)
    {
        trans.y += Mathf.Cos((speed-(maxSpeed+minSpeed)/2)/(maxSpeed-minSpeed)*4) * Time.deltaTime * maxSpeed * 0.02f;
        trans *= Mathf.Cos(transform.rotation.x / 90);
        if(transform.position.y >= maxHeight * 0.8f && trans.y > 0f)
        {
            trans.y *= (-transform.position.y + maxHeight)/(maxHeight*0.2f);
        }
        transform.position+=trans;

    }
}
