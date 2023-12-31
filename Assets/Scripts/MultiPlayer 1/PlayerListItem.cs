using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_Text playerNameText;

    [SerializeField]
    TMP_Text statusText;

    [SerializeField]
    TMP_Text readyText;
    Player player;
    public Image playerAvatar;
    private GameObject readyBtn;
    private LobbyManagement manager;
    private CharcaterPickerManager characterPickerManager;
    private string IS_READY = "isReady";
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        playerProperties[IS_READY] = false;
        manager = GameObject
            .FindGameObjectWithTag("LobbyManagement")
            .GetComponent<LobbyManagement>();
        readyBtn = GameObject.FindGameObjectWithTag("ReadyButton");
        characterPickerManager = GameObject
            .FindGameObjectWithTag("CharcaterPickerManager")
            .GetComponent<CharcaterPickerManager>();
        Button myButton = readyBtn.GetComponent<Button>();
        readyText = readyBtn.GetComponentInChildren<TMP_Text>();
        myButton.onClick.AddListener(OnReadyClicked);
    }

    public void SetUp(Player player)
    {
        this.player = player;
        playerNameText.text = player.NickName;
        UpdatePlayerStatus(player);
        UpdatePlayerAvater(player);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (this.player == otherPlayer)
        {
            Destroy(gameObject);
            manager.CheckGameReady();
        }
    }

    public override void OnLeftRoom() => Destroy(gameObject);

    public void OnReadyClicked()
    {
        bool isReady = (bool)playerProperties[IS_READY];
        if (isReady)
        {
            playerProperties[IS_READY] = false;
        }
        else
        {
            playerProperties[IS_READY] = true;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(
        Player targetPlayer,
        ExitGames.Client.Photon.Hashtable changedProps
    )
    {
        if (this.player != targetPlayer)
            return;

        Debug.Log(changedProps);
        Debug.Log("Players List Updated");
        if (changedProps.ContainsKey("playerAvatar"))
        {
            UpdatePlayerAvater(targetPlayer);
        }

        if (changedProps.ContainsKey(IS_READY))
        {
            UpdatePlayerStatus(targetPlayer);
            manager.CheckGameReady();
        }
    }

    public void UpdatePlayerStatus(Player player)
    {
        if (!player.CustomProperties.ContainsKey(IS_READY))
        {
            playerProperties[IS_READY] = false;
            return;
        }

        bool isReady = (bool)player.CustomProperties[IS_READY];
        if (isReady)
        {
            statusText.text = "Ready";
            statusText.color = Color.green;
            UpdateReadyText("UNREADY");
        }
        else
        {
            statusText.text = "Not ready";
            statusText.color = Color.red;
            UpdateReadyText("READY");
        }
    }

    public void UpdateReadyText(string text)
    {
        if (this.player != PhotonNetwork.LocalPlayer)
            return;

        readyText.text = text;
    }

    public void UpdatePlayerAvater(Player player)
    {
        if (!characterPickerManager)
            return;
        characterPickerManager.UpdatePlayerAvater(player, playerAvatar);
    }
}
