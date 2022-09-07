using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class GameManager : MonoBehaviourPun
{
    public GameObject ocean;
    public Transform oceanCentor;
    public GameObject PlayerPrefab;
    public float respawnTimer;
    public Hitable team1Carrier;
    public Hitable team2Carrier;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public CinemachineVirtualCamera virtualCamera;
    UImanager ui;
    GameObject localPlayer;
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

    private void OnEnable()
    {
        if (LobbyManager.teamId == 1)
        {
            localPlayer = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPoint1.position, spawnPoint1.rotation);
            localPlayer.tag = "team1";
        }
        else
        {
            localPlayer = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPoint2.position, spawnPoint2.rotation);
            localPlayer.tag = "team2";
        }
        localPlayer.GetComponent<Hitable>().sendTag(localPlayer.tag);
        virtualCamera.Follow = localPlayer.GetComponent<Player>().cameraPos;
        virtualCamera.LookAt = localPlayer.GetComponent<Player>().cameraPos;
    }
    

    // Update is called once per frame
    void Update()
    {
        ui.team1Slider(team1Carrier.Hp / team1Carrier.maxHp);
        ui.team2Slider(team2Carrier.Hp / team2Carrier.maxHp);
    }

    public void Respawn(GameObject player)
    {
        /*if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }*/
        player.SetActive(false);
        StartCoroutine(respawnRotin(player));
    }

    public IEnumerator respawnRotin(GameObject player)
    {
        yield return new WaitForSeconds(respawnTimer);
        //yield return null;
        if (player != null)
        {
            if (player.transform.CompareTag("team1"))
            {
                player.GetComponent<Hitable>().spawnSet(spawnPoint1);
            }else if (player.transform.CompareTag("team2"))
            {
                player.GetComponent<Hitable>().spawnSet(spawnPoint2);
            }
            player.SetActive(true);
        }
    }
}
