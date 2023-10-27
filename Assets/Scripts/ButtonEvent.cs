using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public GameObject menuPanel; // �޴� â (�г� ��)�� ����Ű�� ����
    private bool isMenuOpen = false; // �޴� â�� ���� �ִ��� ����

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