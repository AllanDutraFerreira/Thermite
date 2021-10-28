using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum musicaFase
{
    FLORESTA, CAVERNA, GAMEOVER, THEEND
}

public enum gameState
{
    TITULO, GAMEPLAY, END, GAMEOVER
}


public class GameController : MonoBehaviour
{
    public gameState currentState;
    public GameObject painelTitulo, painelGameOver, painelEnd;


    private Camera           cam;
    public Transform         playerTransform;
    public float             speedCam;

    public Transform         LimiteCamEsc, LimiteCamDir, LimiteCamSup, LimiteCamBaixo;

    [Header("Audio")]
    public AudioSource       sfxSource;
    public AudioSource       musicSource;

    public AudioClip         sfxJump;
    public AudioClip         sfxAtack;
    public AudioClip         sfxCoin;
    public AudioClip         sfxEnemyDead;
    public AudioClip         sfxDamage;
    public AudioClip[]       sfxStep;

    public GameObject[]      fase;

    public musicaFase        musicaAtual;

    public AudioClip         musicFloresta, musicCaverna, musicaGameOver, musicaFim;

    public int        moedasColetadas;
    public Text       moedasTxt;
    public GameObject obj;
    public Image[]    coracoes;
    public int        vida;


    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;

        heartController();

    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == gameState.TITULO && Input.GetKeyDown(KeyCode.Space))
        {
            currentState = gameState.GAMEPLAY;
            painelTitulo.SetActive(false);
        }
        else if (currentState == gameState.GAMEOVER && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (currentState == gameState.END && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    void LateUpdate()
    {

        CamController();

    }

    void CamController()
    {

        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;


        if (cam.transform.position.x < LimiteCamEsc.position.x && playerTransform.position.x < LimiteCamEsc.position.x)
        {
            posCamX = LimiteCamEsc.position.x;
        }
        else if (cam.transform.position.x > LimiteCamDir.position.x && playerTransform.position.x > LimiteCamDir.position.x)
        {
            posCamX = LimiteCamDir.position.x;
        }

        if (cam.transform.position.y < LimiteCamBaixo.position.y && playerTransform.position.y < LimiteCamBaixo.position.y)
        {
            posCamY = LimiteCamBaixo.position.y;
        }
        else if (cam.transform.position.y > LimiteCamSup.position.y && playerTransform.position.y > LimiteCamSup.position.y)
        {
            posCamY = LimiteCamSup.position.y;
        }




        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);


    }
    
    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);

    }

    public void trocarMusica(musicaFase novaMusica)
    {
        AudioClip Clip = null;

        switch (novaMusica)
        {
            case musicaFase.CAVERNA:
                Clip = musicCaverna;
                break;

            case musicaFase.FLORESTA:
                Clip = musicFloresta;
                break;

        }

        StartCoroutine("controleMusica", Clip);

    }
    IEnumerator controleMusica(AudioClip musica)
    {
        float volumeMaximo = musicSource.volume;
        for(float volume = volumeMaximo; volume > 0; volume -= 0.1f)
        {
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = musica;
        musicSource.Play();
        
        for (float volume = 0; volume < volumeMaximo; volume += 0.1f)
        {
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }


    }
    public void getHit()
    {
        vida -= 1;
        heartController();
        if(vida <= 0)
        {
            playerTransform.gameObject.SetActive(false);
            painelGameOver.SetActive(true);
            currentState = gameState.GAMEOVER;
            trocarMusica(musicaFase.GAMEOVER);
        }
    }

    public void getCoint()
    {
        Text coinObject = GameObject.Find("coinTxt").GetComponent<Text>();

        moedasColetadas += 1;
        //        moedasTxt.text = moedasColetadas.ToString();
        coinObject.text = moedasColetadas.ToString();
    }


    public void heartController()
    {
        foreach(Image h in coracoes)
        {
            h.enabled = false;
        }
        for(int v = 0; v < vida; v++)
        {
            coracoes[v].enabled = true;
        }
    }

    public void theEnd()
    {
        currentState = gameState.END;
        painelEnd.SetActive(true);
        trocarMusica(musicaFase.THEEND);
    }

}
