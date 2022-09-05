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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Collider[] cols = Physics.OverlapSphere(transform.position, raderRange);
        GameObject target = null;
        foreach(Collider col in cols){
            if(col.GetComponentInParent<Hitable>() != null && !col.GetComponentInParent<Hitable>().CompareTag(transform.tag) && target == null)
            {
                target = col.gameObject;
                break;
            }
        }
        if (target != null)
        {
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

    protected override void Die()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            //base.Die();
            return;
        }
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, radius,0);
        foreach(Collider hit in hits)
        {
            if(hit.transform.GetComponent<Hitable>() != null)
            {
                hit.transform.GetComponent<Hitable>().takeDamage(damage);
            }
        }
        PhotonNetwork.Destroy(gameObject);
        //Destroy(gameObject);
        base.Die();
    }
}
