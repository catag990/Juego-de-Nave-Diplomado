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
        LifeText.text = "Vidas: " + player.lives;
        ShieldText.text = "Escudos: " + player.shields;
    }

    private void CreateEnemy()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {

            Instantiate(enemyPrefab, new Vector3(Random.Range(-6.0f, 6.0f), 4.0f, 0), Quaternion.identity);
            time = 0.0f;
        }
    }

    
}
