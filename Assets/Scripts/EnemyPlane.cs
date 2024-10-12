using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    float speed;
    public int health = 3;
    public GameObject explosionPrefab;
    public GameObject BulletPref;
    Vector2 Dir;
    float DirectionX;
    float angle;
    private Vector3 centerPosition;
    public float fireInterval = 2f;
    public float timeLastAtack = 0f;
    public float radius = 2f;
    public float descendSpeed = 0.5f;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        centerPosition = transform.position;
        speed = Random.Range(1.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }

    public void Movement()
    {
        angle += speed/2 * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        centerPosition.y -= descendSpeed * Time.deltaTime;

        transform.position = new Vector3(centerPosition.x + x, centerPosition.y + y, 0);
    }

    public void Fire() {
        timeLastAtack += Time.deltaTime;

        if (timeLastAtack >= fireInterval) {
            timeLastAtack = 0.0f;
            Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

        }


    }

    public void takeDamage()
    {
        health -= 1;

        if (health <= 0)
        {
            Destroy(this.gameObject);
            player.points += 50;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }



}
