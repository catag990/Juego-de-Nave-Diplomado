using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    float DirectionX;
    Vector2 Dir;

    // Start is called before the first frame update
    void Start()
    {

        DirectionX = Random.Range(-0.5f, 0.5f);
        int DirectionY = -2;
        Dir = new Vector2(DirectionX, DirectionY);
        Dir.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        transform.Translate(Dir * 5.0f * Time.deltaTime);
    }



    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
