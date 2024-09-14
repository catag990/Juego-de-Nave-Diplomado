using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    public float speed = 5.0f;
    public float fireRate = 0.025f;
    public int lives = 3;
    public int shields = 3;
    public float canFire = 0.0f;
    public float shieldDuration = 5.0f;

    public GameObject BulletPref; //Bala que se va a disparar

    public GameObject Shield;

    public List<Bullet> bullets; //Lista de balas usando el script Bullet

    //To use audio
    public AudioManager audioManager;
    public AudioSource actualAudio;

    Vector2 Dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBoundaries();
        ChangeWeapon();
        UseShields();
        Fire();
    }

    public bool ShieldInUse = false;
    public void UseShields()
    {
        if (shields > 0 && Input.GetKeyDown("e"))
        {
            ShieldInUse = true;
            shields--;
            Shield.SetActive(true);
            
        }
    }
    //Movimiento del personaje, se usa WASD para mover al jugador
    void Movement() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Dir = new Vector2(horizontalInput, verticalInput).normalized;
 

        //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        //transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        transform.Translate(Dir * speed * Time.deltaTime);
    }

    //Revisa los bordes del juego, la cámara va a settear los bordes
    void CheckBoundaries() { 
        var cam = Camera.main;
        float xMax = cam.orthographicSize * cam.aspect;
        float yMax = cam.orthographicSize;
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y, 0);
        }
        else if (transform.position.x < -xMax) {
            transform.position = new Vector3(xMax, transform.position.y, 0);
        }
        if (transform.position.y > yMax) {
            transform.position = new Vector3(transform.position.x, -yMax , 0);
        }
        else if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, 0);
        }
    }

    void Fire() {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            canFire = Time.time + fireRate;

            actualAudio.Play();
        }
        
    }


    
    public void ChangeWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BulletPref = bullets[0].gameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BulletPref = bullets[1].gameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            BulletPref = bullets[2].gameObject;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy")) {
                Destroy(collision.gameObject);

                if (ShieldInUse) { 
                    Shield.SetActive(false);
                }

                else if (lives > 1)
                {
                    lives--;
                    Debug.Log("Lives: " + lives);
                }
                else {
                    lives--;
                    Destroy(this.gameObject);
                }
            }
        }
    }


}
