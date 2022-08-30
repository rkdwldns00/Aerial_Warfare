using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ocean;
    public Transform oceanCentor;
    public float respawnTimer;
    public Hitable team1Carrier;
    public Hitable team2Carrier;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    UImanager ui;
    void Start()
    {
        ui = FindObjectOfType<UImanager>();
        int scale = (int)ocean.transform.localScale.x;
        for (int i = -50; i < 50; i++)
        {
            for (int j = -50; j < 50; j++)
            {
                Instantiate(ocean, new Vector3(i*scale*10f, 0, j*scale*10f),Quaternion.identity, oceanCentor);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ui.team1Slider(team1Carrier.Hp / team1Carrier.maxHp);
        ui.team2Slider(team2Carrier.Hp / team2Carrier.maxHp);
    }

    public void Respawn(GameObject player)
    {
        StartCoroutine(respawnRotin(player));
    }

    public IEnumerator respawnRotin(GameObject player)
    {
        yield return new WaitForSeconds(respawnTimer);
        //yield return null;
        if (player != null)
        {
            player.SetActive(true);
        }
    }
}
