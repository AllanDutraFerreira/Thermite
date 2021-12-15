using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        Reset();

        Sair();
    }

    void Reset()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(0);
        }
      
    }
    void Sair()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Saiu do jogo");
        }


    }

}
