using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDosCarros2 : MonoBehaviour
{
    public float Speed;
    public bool Carro1;
    public bool Carro2;


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        Move2();
    }

    void Move2()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);

        }

    }

}
