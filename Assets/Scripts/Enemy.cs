using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed;
    public int health = 1;
    private GameObject explosionPrefab;
    Vector2 Dir;
    float DirectionX;

    // Start is called before the first frame update
    void Start()
    {
        DirectionX = Random.Range(-2.5f, 2.5f);
        int DirectionY = -2;
        Dir = new Vector2(DirectionX, DirectionY);
        Dir.Normalize();
        speed = Random.Range(1.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();    
    }

    public void Movement() { 
        transform.Translate(Dir * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    

}
