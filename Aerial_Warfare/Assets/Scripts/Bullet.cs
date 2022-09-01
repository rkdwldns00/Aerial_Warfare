using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float range;
    public float age;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.rotation *= Quaternion.Euler(Random.Range(-age, age), Random.Range(-age, age), 0);
        RaycastHit hit;
        Physics.Raycast(transform.position, (transform.rotation) * Vector3.forward, out hit, range);
        if (hit.collider != null/* && transform.tag != hit.transform.GetComponentInParent<Hitable>().tag */&& hit.transform.GetComponentInParent<Hitable>() != null)
        {
            hit.transform.GetComponent<Hitable>().takeDamage(10f);
        }
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * range);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
