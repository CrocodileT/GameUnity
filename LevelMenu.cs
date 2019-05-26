using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Button Level1;
    public Button Home;
    // Start is called before the first frame update
    void Start()
    {
        Level1.onClick.AddListener(toGame);
        Home.onClick.AddListener(HOME);
    }

    void toGame()
    {
        SceneManager.LoadScene("Game");
    }

    void HOME()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
