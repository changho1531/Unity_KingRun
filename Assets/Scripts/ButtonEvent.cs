using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public GameObject menuPanel; // 메뉴 창 (패널 등)을 가리키는 변수
    private bool isMenuOpen = false; // 메뉴 창이 열려 있는지 여부

    public void ClickStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickMain()
    {
        SceneManager.LoadScene("TitmeScene");
    }

    public void ClickReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickEnd()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (menuPanel != null)
        {
            isMenuOpen= !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);
        }
    }
}
