using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    float DirectionX;
    Vector2 Dir;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        DirectionX = Random.Range(-0.5f, 0.5f);
        int DirectionY = 2;
        Dir = new Vector2(DirectionX, DirectionY);
        Dir.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    
    void Movement() {

        //transform.Translate(Vector3.up * 5.0f * Time.deltaTime + Vector3.left * Time.deltaTime * DirectionX);
        if (player.currentBulletIndex == 3)
        {
            Dir = new Vector2(0, 2);
            Dir.Normalize();
        }
      transform.Translate(Dir * 5.0f * Time.deltaTime);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                player.points += 10;

                Destroy(this.gameObject);
            
            }
            if (collision.gameObject.CompareTag("EnemyPlane")) {
                EnemyPlane enemyPlane = collision.gameObject.GetComponent<EnemyPlane>();
                if (enemyPlane != null) {
                    enemyPlane.takeDamage();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);    
    }
}
