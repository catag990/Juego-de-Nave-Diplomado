using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public float spawnTime = 1.5f;
    public float time = 0.0f;
    public float timePU = 0.0f;
    public Player player;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI PointsText;
    public TextMeshProUGUI TimeText;

    

    // Start is called before the first frame update
    void Start()
    {
        
        player.weaponName = "bala";
    }

    // Update is called once per frame
    void Update()
    {
        CreateEnemy();
        UpdateCanvas();
        CreatePowerUp();
    }

    void UpdateCanvas()
    {
        LifeText.text = "vidas: " + player.lives;
        ShieldText.text = "escudos: " + player.shields;
        WeaponText.text = "arma: " + player.weaponName;
        PointsText.text = "puntos: " + player.points;
        TimeText.text = "tiempo: " + ((int)Time.time);
    }

    private void CreateEnemy()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {

            Instantiate(enemyPrefab, new Vector3(Random.Range(-6.0f, 6.0f), 3.0f, 0), Quaternion.identity);
            time = 0.0f;
        }
    }

    private void CreatePowerUp(){
        var cam = Camera.main;
        float xMax = cam.orthographicSize * cam.aspect;
        float yMax = cam.orthographicSize;
        timePU += Time.deltaTime;
        if (timePU > spawnTime*2){
            
            Instantiate(powerUpPrefab, new Vector3(Random.Range(-xMax, xMax), Random.Range(-yMax, yMax), 0), Quaternion.identity);
            timePU = 0.0f;
        }

    }
    
}
