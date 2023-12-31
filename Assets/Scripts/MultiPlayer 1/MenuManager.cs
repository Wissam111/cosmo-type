using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField]
    Menu[] menus;

    void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].GetMenuName() == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].IsOpen())
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].IsOpen())
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
/*
    private void Update()
    {
        photonView.RPC(nameof(StatsChanged), photonView.Owner);
    }

    [PunRPC]
    public void StatsChanged()
    {
        string accurecy = charactersTyped > 0 ? Math.Round((float)charactersCorrect / charactersTyped * 100) + "%" : "0%";
        playerProperties["accurecy"] = accurecy;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }*/
