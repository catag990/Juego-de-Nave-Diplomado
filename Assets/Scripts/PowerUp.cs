using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float powerUpTime = 5.0f;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.powerUpInUse){ 
            player.speed = 10.0f;
            powerUpTime -= Time.deltaTime;
            if (powerUpTime < 0){
                player.powerUpInUse = false;
            }
        }
        else{
            player.speed = 5.0f;
            powerUpTime = 5.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player")) {
                Destroy(this.gameObject);
                player.powerUpInUse = true;
                
            }
        }

    }
}
