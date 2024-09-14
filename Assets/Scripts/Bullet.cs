using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    float DirectionX;
    Vector2 Dir;
    // Start is called before the first frame update
    void Start()
    {
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
      transform.Translate(Dir * 5.0f * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);

            }
        }
    }
}
