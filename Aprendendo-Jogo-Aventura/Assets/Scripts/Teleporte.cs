using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporte : MonoBehaviour
{
    private GameController         _GameController;

    public Transform               pontoSaida;
    public Transform               posCamera;

    public Transform               LimiteCamEsc, LimiteCamDir, LimiteCamSup, LimiteCamBaixo;

    public musicaFase              novaMusica;

    // Start is called before the first frame update
    void Start()
    {

        _GameController = FindObjectOfType(typeof(GameController)) as GameController;


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            _GameController.trocarMusica(musicaFase.CAVERNA);
            col.transform.position = pontoSaida.position;
            Camera.main.transform.position = posCamera.position;

            _GameController.LimiteCamEsc = LimiteCamEsc;
            _GameController.LimiteCamDir = LimiteCamDir;
            _GameController.LimiteCamSup = LimiteCamSup;
            _GameController.LimiteCamBaixo = LimiteCamBaixo;

            
        }
    } 
}
