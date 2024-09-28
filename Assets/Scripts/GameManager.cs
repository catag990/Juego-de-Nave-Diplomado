using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 1.5f;
    public float time = 0.0f;
    public Player player;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI PointsText;
    public TextMeshProUGUI TimeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateEnemy();
        UpdateCanvas();
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

    
}
