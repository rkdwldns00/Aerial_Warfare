using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
//using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

// ������(��ġ ����ŷ) ������ �� ������ ���
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "2"; // ���� ����
    public static int teamId = 1;
    public Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; // �� ���� ��ư
    public Button team1Button;
    public Button team2Button;

    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        team1Button.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
        joinButton.interactable = false;
        connectionInfoText.text = "������ ������ ���� ��...";
    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "�¶��� : ������ ������ �����";
    }

    // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ� ��...";
        PhotonNetwork.ConnectUsingSettings();
    }

    // �� ���� �õ�
    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "�� ���� ����, ���ο� �� ����...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "�� ���� ����";
        PhotonNetwork.LoadLevel("Main");
    }

    public void setTeam(int i)
    {
        teamId = i;
        if (i == 1)
        {
            team1Button.interactable = false;
            team2Button.interactable = true;
        }
        else
        {
            team1Button.interactable = true;
            team2Button.interactable = false;
        }
    }
}