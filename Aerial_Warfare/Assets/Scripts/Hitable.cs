using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hitable : MonoBehaviourPun
{ 
    public float maxHp;
    public GameObject explosion;
    GameManager gameManager;
    float realHp;
    public float Hp
    {
        get
        {
            return realHp;
        }
        private set
        {
            if (value <= 0f)
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

    private void Start()
    {
        Hp = maxHp;
    }

    private void OnEnable()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Hp = maxHp;
        photonView.RPC("UpdateHp", RpcTarget.Others, Hp);
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
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(gameObject.activeSelf && transform.position.y <= 0f && transform.GetComponent<Player>() != null)
        {
            takeDamage(999);
        }
    }

    [PunRPC]
    public void takeDamage(float damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Hp -= damage;
            photonView.RPC("UpdateHp",RpcTarget.Others,Hp);
            photonView.RPC("takeDamage",RpcTarget.Others,damage);
        }
        if (Hp <= 0f)
        {
            Die();
            photonView.RPC("Die", RpcTarget.Others);
        }
    }

    [PunRPC]
    public void UpdateHp(float newHealth)
    {
        Hp = newHealth;
    }

    [PunRPC]
    protected virtual void Die()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        //if (gameObject.GetComponent<Player>() != null)
        //{
        //}
        if (PhotonNetwork.IsMasterClient)
        {
            //photonView.RPC("Die", RpcTarget.Others);
            //photonView.RPC("RespawnRequest", RpcTarget.All);
        }
        
        RespawnRequest();
    }

    [PunRPC]
    void RespawnRequest()
    {
        gameManager.Respawn(gameObject);
    }

    [PunRPC]
    public void spawnSet(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(collision != null && collision.transform.GetComponentInParent<Hitable>() && !collision.transform.GetComponentInParent<Hitable>().CompareTag(transform.tag))
        {
            float damage = collision.transform.GetComponentInParent<Hitable>().Hp;
            collision.transform.GetComponentInParent<Hitable>().takeDamage(Hp);
            takeDamage(damage);
        }
    }
}
