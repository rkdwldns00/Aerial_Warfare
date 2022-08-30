using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hitable : MonoBehaviour
{ 
    public float maxHp;
    public GameObject explosion;
    GameManager gameManager;
    float realHp;
    public float Hp {
        get
        {
            return realHp;
        }
        private set
        {
            if(value <= 0f)
            {
                realHp = 0f;
            }
            else
            {
                realHp = value;
            }
        }
    }
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        //StartCoroutine(onOff(1f));
    }

    private void OnEnable()
    {
        Hp = maxHp;
        if (transform.GetComponent<Player>() != null)
        {
            if (transform.CompareTag("team1"))
            {
                transform.position = gameManager.spawnPoint1.position;
                transform.rotation = gameManager.spawnPoint1.rotation;
            }
            else if (transform.CompareTag("team2"))
            {
                transform.position = gameManager.spawnPoint2.position;
                transform.rotation = gameManager.spawnPoint2.rotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf && transform.position.y <= 0f && transform.GetComponent<Player>() != null)
        {
            takeDamage(999);
        }
    }

    public void takeDamage(float damage)
    {

        Hp -= damage;
        if (Hp <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        //if (gameObject.GetComponent<Player>() != null)
        //{
        //}
        gameManager.Respawn(gameObject);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.transform.GetComponentInParent<Hitable>())
        {
            float damage = collision.transform.GetComponentInParent<Hitable>().Hp;
            collision.transform.GetComponentInParent<Hitable>().takeDamage(Hp);
            takeDamage(damage);
        }
    }

    IEnumerator onOff(float timer)
    {
        yield return new WaitForSeconds(timer);
        this.enabled = false;
        this.enabled = true;
    }
}
