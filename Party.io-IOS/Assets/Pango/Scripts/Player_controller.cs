using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

    
	public static Player_controller playerController;
	public float x;
    Rigidbody rb;
    public float Resistencia = 10;
    public float VelSalto = 0.5f;
    public float Velocidad = 10;
    bool caido = false;
    bool rotando = false;
    public float VelRotacion = 5;
    public float VelCaps = 0.01f;
    public Vector3 direccion;
    float velin;
    public bool Ensuelo;
    public bool EnMovimiento;
    public float Angulo_rotacion;
  
    public float velocidadrb;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bala")
        {
            Caer();
        }
        if (col.relativeVelocity.magnitude > Resistencia)
        {
            Caer();
        }
    }
    void Caer()
    {
        rb.constraints = RigidbodyConstraints.None;
        caido = true;
        if (velocidadrb < 3f)
        {
            Invoke("recuperar", Random.Range(2,6));
        }
        else
        {
            Caer();
        }
        
    }
    void OnCollisionStay(Collision coli)
    {
        if (coli.gameObject.tag == "Suelo")
        {
            Ensuelo = true;
        }
    }
    void OnCollisionExit(Collision coli)
    {
        if (coli.gameObject.tag == "Suelo")
        {
            Ensuelo = false;
        }
    }

    void recuperar()
    {
        caido = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void Salto()
    {
        if (!caido && Ensuelo)
        {
            rb.AddForce(new Vector3(0, VelSalto, 0), ForceMode.Impulse);
            
        }
        
    }
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        velin = Velocidad;
    }
	
	// Update is called once per frame


   void FixedUpdate()
    {
       
        //actualizar caps
        
        //Movimiento
        Transform Cam = Camera.main.transform;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //direccion = Cam.up;
        

        Vector3 Frente = Cam.TransformDirection(Vector3.forward);
        Frente.y = 0f;
        Frente = Frente.normalized;

        Vector3 right = new Vector3(Frente.z, 0, -Frente.x);
		//Vector3 right = new Vector3(Frente.y, -Frente.x,0 );


        float vel = Input.GetAxis("Horizontal") * Time.deltaTime;
        float delante = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 mov = right * vel;
        Vector3 movdel = Frente * delante;

        
        if (!caido && !rotando)
        {
            rb.MovePosition(rb.position + (mov + movdel) * Velocidad);
            
        }

       


        Vector3 pos_direccion = transform.position + (direccion * horizontal) + (Cam.forward * vertical);
        Vector3 dir = pos_direccion - transform.position;

        dir.y = 0;



        //Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Salto();
        }
        //Girar
        if (horizontal != 0 || vertical != 0)
        {
            if (!caido)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir));
            if (angle != 0)
                EnMovimiento=true;
            {
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), VelRotacion * Time.deltaTime);
            }
            if (angle > Angulo_rotacion)
            {
                rotando = true;
            }
            if (angle <= Angulo_rotacion)
            {
                rotando = false;

            }

        }
        if (horizontal == 0 && vertical == 0 && !caido)
        {
           rb.constraints= RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            EnMovimiento = false;
            
        }
    }
}
