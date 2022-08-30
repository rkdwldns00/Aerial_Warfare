using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
