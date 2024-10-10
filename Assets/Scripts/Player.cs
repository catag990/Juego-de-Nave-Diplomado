using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    public float speed = 5.0f;
    public float fireRate = 0.025f;
    public int lives = 4;
    public int shields = 3;
    public float canFire = 0.0f;
    public float shieldDuration = 5.0f;

    public bool powerUpInUse = false;

    public GameObject BulletPref; //Bala que se va a disparar

    public GameObject Shield;
    public List<GameObject> Weapons;
    public GameObject weapon;

    public List<Bullet> bullets; //Lista de balas usando el script Bullet
    public int currentBulletIndex = 0;

    //To use audio
    public AudioManager audioManager;
    public AudioSource actualAudio;

    public int points;
    public string weaponName = "bala";

    public enum ShipState
    {
        FullHealth,
        SlightlyDamaged,
        Damaged,
        HeavilyDamaged,
        Destroyed
    }
    public ShipState shipState;
    public List<Sprite> shipSprites = new List<Sprite>();


    Vector2 Dir;

    // Start is called before the first frame update
    void Start()
    {
        ChangeShipState();
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
        if (shields > 0 && Input.GetKeyDown("e") && ShieldInUse == false)
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

    //Revisa los bordes del juego, la c�mara va a settear los bordes
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
        if (currentBulletIndex == 3) {
            if (Input.GetKey("space") && Time.time > canFire) {
                Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                canFire = Time.time;
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
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
            currentBulletIndex = 0;
            weaponName = "bala";
            weapon = Weapons[0].gameObject;
            weapon.SetActive(true);
            fireRate = 0.025f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BulletPref = bullets[1].gameObject;
            currentBulletIndex = 1;
            weaponName = "cohete";
            weapon.SetActive(true);
            fireRate = 0.025f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            BulletPref = bullets[2].gameObject;
            currentBulletIndex = 2;
            weaponName = "energia";
            weapon.SetActive(true);
            fireRate = 0.025f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BulletPref = bullets[3].gameObject;
            currentBulletIndex = 3;
            weaponName = "laser";
            weapon.SetActive(true);
            fireRate = 0.025f;
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
                    ShieldInUse = false;
                    
                }

                else if (lives > 1)
                {
                    points -= 5;
                    lives--;
                    ChangeShipState();
                }
                else {
                    lives--;
                    ChangeShipState();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void ChangeShipState() { 
        var currentState = shipState;
        Debug.Log(currentState);   

        var newSprite = shipSprites.Find(x => x.name == currentState.ToString());

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;

        switch (currentState) { 
            case ShipState.FullHealth:
                shipState = ShipState.SlightlyDamaged;
                break;
            case ShipState.SlightlyDamaged:
                shipState = ShipState.Damaged;
                break;
            case ShipState.Damaged:
                shipState = ShipState.HeavilyDamaged;
                break;
            case ShipState.HeavilyDamaged:
                shipState = ShipState.Destroyed;
                break;
            case ShipState.Destroyed:
                break;
        }
    }

    


}
