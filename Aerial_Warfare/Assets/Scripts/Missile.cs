using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Hitable
{
    public float speed;
    public float radius;
    public float damage;
    public float raderRange;
    public float trackingPower;
    float timer = 0f;
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        timer += Time.deltaTime;
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        move(transform.rotation * Vector3.forward * speed * Time.deltaTime);
        Collider[] cols = Physics.OverlapSphere(transform.position, raderRange);
        GameObject target = null;
        foreach (Collider col in cols)
        {
            if (col.GetComponentInParent<Hitable>() != null && !col.GetComponentInParent<Hitable>().CompareTag(transform.tag) && target == null)
            {
                target = col.gameObject;
                break;
            }
        }
        if (target != null && timer >= 0.5f)
        {
            /*Quaternion look = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = look;*/
            transform.LookAt(target.transform.position);
        }
    }

    /*    private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<Hitable>() != null)
            {
                Die();
            }
        }*/

    [PunRPC]
    protected override void Die()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            //base.Die();
            Destroy(gameObject);
            return;
        }
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, radius, 0);
        foreach (Collider hit in hits)
        {
            if (hit.transform.GetComponent<Hitable>() != null)
            {
                hit.transform.GetComponent<Hitable>().takeDamage(damage);
            }
        }
        photonView.RPC("Die", RpcTarget.Others);
        PhotonNetwork.Destroy(gameObject);
        //Destroy(gameObject);
        base.Die();
    }

    void move(Vector3 trans)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        trans *= 100;
        if (Mathf.Abs(rigid.velocity.x) > Mathf.Abs(trans.x) && rigid.velocity.x * trans.x > 0f)
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
        //transform.position+=trans;
        rigid.AddForce(trans * trackingPower);
    }
}
