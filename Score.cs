using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Button Home;
    // Start is called before the first frame update
    void Start()
    {
        Home.onClick.AddListener(HOME);
    }

    void HOME()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
