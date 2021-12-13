using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDosCarros : MonoBehaviour
{
    public float         Speed;
    public bool          Carro1;
    public bool          Carro2;


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        Move1();
    }

    void Move1()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);

        }

    }
    
}
