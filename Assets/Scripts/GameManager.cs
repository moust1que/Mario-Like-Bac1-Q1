using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameEnded = false;
    public GameObject endLevelUI;
    public GameObject player;
    public GameObject cam;

    //Valeur par defaut du HUD
    public int piece = 0;
	private int pieceForLive = 0;
    public int health = 3;
    public int score = 0;
    public int timer = 0;

    //Valeur des textes sur l'UI
    public TMPro.TextMeshProUGUI scoreTxt;
    public TMPro.TextMeshProUGUI pieceTxt;
	public TMPro.TextMeshProUGUI worldTxt;
    public TMPro.TextMeshProUGUI timerTxt;
    public TMPro.TextMeshProUGUI healthTxt;

	public List<GameObject> enemies = new();
	public List<GameObject> luckyBlocks = new();

    //Fonction d'ajout d'une piece au HUD
    public void AddPiece()
    {
        pieceForLive++;
        // pieceTxt.text = $"Pieces\n{piece + pieceForLive}";
    }

    //Fonction d'ajout du score "addScore" au HUD
    public void AddScore(int addScore)
    {
        score += addScore;
        scoreTxt.text = $"Score\n{score}";
    }

    //Fonction de mort du joueur / Enlever une vie
    public void DeathPlayer()
    {
		Reset();
        health--;
        healthTxt.text = "Health: " + health.ToString();
        if (health == 0)
        {
            EndLevel();
        }
    }

	private void Reset() {
		// Reset player
		player.transform.position = new Vector3(2.0f, 1.05f, 0.0f);
        cam.transform.position = new Vector3(2.0f, 13.0f, -10.0f);

		// Reset Enemies
		Enemy enemy;
		for(int i = 0; i < enemies.Count; i++) {
			enemy = enemies[i].GetComponent<Enemy>();
			enemies[i].SetActive(true);
			enemy.health = 1;
		}

		// Reset Score
		score = 0;
		scoreTxt.text = $"Score\n{score}";

		// Reset Coins
		piece = 0;
        pieceTxt.text = $"Pieces\n{piece}";

		// Reset Lucky Blocks
		Block block;
		for(int i = 0; i < luckyBlocks.Count; i++) {
			block = luckyBlocks[i].GetComponent<Block>();
			if(i == 0)
				block.bonus = 3;
			else if(i == 5 || i == 7) {
				block.bonus = 1;
				luckyBlocks[i].SetActive(true);
			}else
				block.bonus = 2;
		}
	}

    //Fonction de fin de level
    public void EndLevel()
    {
        isGameEnded = true;
        Time.timeScale = 0;
        endLevelUI.SetActive(true);
    }

    //Fonction de Restart du niveau
    public void Restart()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
    }

    void Start()
    {
		PrintText();

        Time.timeScale = 1;
        endLevelUI.SetActive(false);
    }
    /// piece et vie 
    void Update()
    {
        if (pieceForLive == 3) {
			piece += pieceForLive;
			pieceForLive = 0;
			Debug.Log("in");
            health++;
        }

		PrintText();
    }

	private void PrintText() {
		scoreTxt.text = $"Score\n{score}";
        pieceTxt.text = $"Pieces\n{piece + pieceForLive}";
		worldTxt.text = "World\n1-1";
        timerTxt.text = $"Timer\n{timer}";
        healthTxt.text = $"Health\n{health}";
	}
}