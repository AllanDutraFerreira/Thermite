using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoCarro1 : MonoBehaviour
{
    public float Speed;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

    }


}
