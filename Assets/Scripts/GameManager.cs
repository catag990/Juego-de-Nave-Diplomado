using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public float spawnTime = 1.5f;
    public float time = 0.0f;
    public float timePU = 0.0f;
    public Player player;

    public Image[] lifeIcons;  // Arreglo para las imágenes de vida
    public Sprite fullLifeIcon; // Ícono cuando la vida está llena
    public Sprite emptyLifeIcon;

    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI PointsText;
    public TextMeshProUGUI TimeText;

    

    // Start is called before the first frame update
    void Start()
    {
        
        player.weaponName = "bala";

        UpdateLifeIcons();
    }

    // Update is called once per frame
    void Update()
    {
        CreateEnemy();
        UpdateCanvas();
        CreatePowerUp();
        UpdateLifeIcons();
    }

    void UpdateCanvas()
    {
        //LifeText.text = "vidas: " + player.lives;
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

        if (timePU > spawnTime * 6)
        {

            Instantiate(powerUpPrefab, new Vector3(Random.Range(-xMax, xMax), Random.Range(-yMax, yMax), 0), Quaternion.identity);
            timePU = 0.0f;
        }
        
    }

    void UpdateLifeIcons()
    {
      
               
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < player.lives)
            {
                lifeIcons[i].sprite = fullLifeIcon; // Ícono de vida llena
            }
            else
            {
                lifeIcons[i].sprite = emptyLifeIcon; // Ícono de vida vacía
            }

            
        }
    }

}
