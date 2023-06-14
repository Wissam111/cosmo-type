
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ExistMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject ExitMenuPanel;
    [SerializeField] private GameObject Loading;
    [SerializeField] private bool enablePause;
    public static bool MenuIsActive = false;
    private bool isGameOver = false;
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)|| isGameOver) return;

        if (MenuIsActive)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    // show the game menu
    public void ShowMenu()
    {
        if (enablePause)
        {
            Time.timeScale = 0f;
        }
        MenuIsActive = true;
        ExitMenuPanel.SetActive(true);
    }

    // hide the game menu
    public void HideMenu()
    {
        if (enablePause)
        {
            Time.timeScale = 1f;
        }
        MenuIsActive = false;
        ExitMenuPanel.SetActive(false);
    }


    public void LeaveMultiPlayerLobby()
    {
        HideMenu();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MAIN_MENU");
    }

    public void LoadMainMenu()
    {
        HideMenu();
        SceneManager.LoadScene("MAIN_MENU");
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

}
