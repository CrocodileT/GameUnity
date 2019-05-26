using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button Play;
    public Button Setting;
    public Button Score;
    public Button Exit;
    // Start is called before the first frame update
    void Start()
    {
        Play.onClick.AddListener(toLevel);
        Setting.onClick.AddListener(toSetting);
        Score.onClick.AddListener(toScore);
        Exit.onClick.AddListener(toWindows);
    }
    void toLevel()
    {
        SceneManager.LoadScene("Setting_level");
    }

    void toSetting()
    {
        SceneManager.LoadScene("Settings");
    }

    void toScore()
    {
        SceneManager.LoadScene("Score");
    }

    void toWindows()
    {
        Application.Quit();
    }
}
