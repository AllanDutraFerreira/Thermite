using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIA : MonoBehaviour
{
    private                GameController _GameController;
    private                Rigidbody2D slimeRb;
    private                Animator slimeAnimator;

    public float           speed;
    public float           timeToWalk;

    public                 GameObject HitBox;
    public bool            isLookLeft;

    private int            h;



    // Start is called before the first frame update
    void Start()
    {

        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        slimeRb = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();

        StartCoroutine("slimeWalk");

    }

    // Update is called once per frame
    void Update()
    {
        if(_GameController.currentState != gameState.GAMEPLAY) { return; }

        if (h > 0 && isLookLeft == true)
        {
            flip();
        }
        else if (h < 0 && isLookLeft == false)
        {
            flip();
        }

        slimeRb.velocity = new Vector2(h * speed, slimeRb.velocity.y);

        

        if(h != 0)
        {
            slimeAnimator.SetBool("isWalk", true);
        }
        else
        {
            slimeAnimator.SetBool("isWalk", false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "HitBox")
        {
            h = 0;
            StopCoroutine("slimeWalk");
            Destroy(HitBox);
            _GameController.playSFX(_GameController.sfxEnemyDead, 0.1f);
            slimeAnimator.SetTrigger("Dead");
        }


    }

    IEnumerator slimeWalk()
    {
        int rand = Random.Range(0,100);
        if(rand < 33)
        {
            h = -1;
        }
        else if(rand < 66)
        {
            h = 0;
        }
        else if (rand < 100)
        {
            h = 1;
        }

        yield return new WaitForSeconds(timeToWalk);
        StartCoroutine("slimeWalk");
    }



    void OnDead()
    {
        Destroy(this.gameObject);
    }

    void flip()
    {

        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;  //inverte o sinal do x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
