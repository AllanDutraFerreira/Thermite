using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIA : MonoBehaviour
{
    private GameController      _GameController;
    private Animator            morcegoAnimator;
    private Rigidbody2D         morcegoRb;

    private bool                isFolow;

    public float                speed;

    public GameObject           HitBox;
    public bool                 isLookLeft;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        morcegoRb = GetComponent<Rigidbody2D>();
        morcegoAnimator = GetComponent<Animator>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if (_GameController.currentState != gameState.GAMEPLAY) { return; }

        if (isFolow == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _GameController.playerTransform.position, speed * Time.deltaTime);

        }

        if (transform.position.x < _GameController.playerTransform.position.x && isLookLeft == true)
        {
            flip();
        }
        else if(transform.position.x > _GameController.playerTransform.position.x && isLookLeft == false)
        {
            flip();
        }



    }
    private void OnBecameInvisible(){
        isFolow = true;
    }
    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;  //inverte o sinal do x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "HitBox")
        {
          
            Destroy(HitBox);
            _GameController.playSFX(_GameController.sfxEnemyDead, 0.1f);
            morcegoAnimator.SetTrigger("Dead");


        }


    }
    void OnDead()
    {
        Destroy(this.gameObject);
    }
}
