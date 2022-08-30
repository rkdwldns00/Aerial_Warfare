using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Hitable
{
    public float speed;
    public float radius;
    public float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, radius,0);
        foreach(Collider hit in hits)
        {
            if(hit.transform.GetComponent<Hitable>() != null)
            {
                hit.transform.GetComponent<Hitable>().takeDamage(damage);
            }
        }
        Destroy(gameObject);
        base.Die();
    }
}
