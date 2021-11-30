using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int             PontuacaoDoJogador1;
    
    public int             PontuacaoDoJogador2;

    public Text            TextoDaPontuacao;

    public AudioSource     somDoGol;

    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Saiu do jogo");
        }


    }

    public void AumentarPontuacaoDoJogador1()
    {
        PontuacaoDoJogador1 += 1;
        AtualizarTextoDaPontuacao();
    }

    public void AumentarPontuacaoDoJogador2()
    {
        PontuacaoDoJogador2 += 1;
        AtualizarTextoDaPontuacao();
    }

    public void AtualizarTextoDaPontuacao()
    {
        TextoDaPontuacao.text = PontuacaoDoJogador1 + " x " + PontuacaoDoJogador2;

        somDoGol.Play();

    }
}
