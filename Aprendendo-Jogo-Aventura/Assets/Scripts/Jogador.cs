using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    private GameController _GameController;

    private Rigidbody2D        PlayerRB;
    private Animator           PlayerAnimator;
    private SpriteRenderer     playerSr;


    public float               speed;
    public float               jumpforce;

    public bool                isLookLeft;

    public Transform           groundCheck;
    private bool               isGrounded;
    private bool               isAtack;

    public Transform          Mao;
    public GameObject         hitBoxPreFab;


    public Color              hitColor;
    public Color              noHitColor;

    public int                maxHP;

    // Start is called before the first frame update
    void Start(){

        PlayerRB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        playerSr = GetComponent<SpriteRenderer>();


        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        _GameController.playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimator.SetBool("isGrounded", isGrounded);

        if(_GameController.currentState != gameState.GAMEPLAY)
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
            PlayerAnimator.SetInteger("h", 0);
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if(isAtack == true && isGrounded == true)
        {
            h = 0;
        }

        if(h > 0 && isLookLeft == true)
        {
            flip();
        }
        else if (h < 0 && isLookLeft == false)
        {
            flip();
        }


        float speedY = PlayerRB.velocity.y;

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            _GameController.playSFX(_GameController.sfxJump, 0.3f);
            PlayerRB.AddForce(new Vector2(0, jumpforce));
        }

        if (Input.GetButtonDown("Fire1") && isAtack == false)
        {
            _GameController.playSFX(_GameController.sfxAtack, 0.3f);
            isAtack = true;
            PlayerAnimator.SetTrigger("atack");
        }

        PlayerRB.velocity = new Vector2(h * speed, speedY);

        PlayerAnimator.SetInteger("h", (int)h);

        PlayerAnimator.SetFloat("speedY", speedY);

        PlayerAnimator.SetBool("isAtack", isAtack);
    }
    
    void FixedUpdate(){
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Coletavel")
        {
            _GameController.playSFX(_GameController.sfxCoin, 0.4f);
            _GameController.getCoint();
            Destroy(col.gameObject);


        }
        else if (col.gameObject.tag == "damage")
        {
            _GameController.getHit();
            if(_GameController.vida > 0)
            {
                StartCoroutine("damageController");
            }
            else if(col.gameObject.tag == "abismo")
            {
                _GameController.playSFX(_GameController.sfxDamage, 0.4f);
                _GameController.vida = 0;
                _GameController.heartController();
                _GameController.painelGameOver.SetActive(true);
                _GameController.currentState = gameState.GAMEOVER;
                _GameController.trocarMusica(musicaFase.GAMEOVER);
            }
            else if(col.gameObject.tag == "flag")
            {
                _GameController.theEnd();
            }
            
        }

    }



    //funçoes feitas pelo programador

    void flip()
    {

        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;  //inverte o sinal do x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void OnEndAtack()
    {
        isAtack = false;
    }

    void hitBoxAtack()
    {
        GameObject hitBoxTemp = Instantiate(hitBoxPreFab, Mao.position, transform.localRotation);
        Destroy(hitBoxTemp, 0.2f);
    }

    void footstep()
    {
        _GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 0.3f);
    }

    IEnumerator damageController()
    {
        _GameController.playSFX(_GameController.sfxDamage, 0.5f);

        maxHP -= 1;
        if(maxHP <= 0)
        {
            Debug.LogError("GameError");
        }

        this.gameObject.layer = LayerMask.NameToLayer("Invencivel");

        playerSr.color = hitColor;
        yield return new WaitForSeconds(0.3f);
        
        playerSr.color = noHitColor;

        for(int i = 0; i < 5; i++)
        {
            playerSr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSr.enabled = true;
            yield return new WaitForSeconds(0.2f);


        }
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        playerSr.color = Color.white;

    }


}
