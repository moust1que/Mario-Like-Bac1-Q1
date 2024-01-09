using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameEnded = false;
    public GameObject endLevelUI;
    public GameObject player;
    public GameObject cam;

    //Valeur par d�faut du HUD
    public int piece = 0;
    public int health = 3;
    public int score = 0;
    public int timer = 0;

    //Valeur des textes sur l'UI
    public TMPro.TextMeshProUGUI scoreTxt;
    public TMPro.TextMeshProUGUI pieceTxt;
	public TMPro.TextMeshProUGUI worldTxt;
    public TMPro.TextMeshProUGUI timerTxt;
    public TMPro.TextMeshProUGUI healthTxt;

    //Fonction d'ajout d'une piece au HUD
    public void AddPiece()
    {
        piece++;
        pieceTxt.text = "Pieces: " + piece.ToString();
    }

    //Fonction d'ajout du score "addScore" au HUD
    public void AddScore(int addScore)
    {
        score = score + addScore;
        scoreTxt.text = "Score: " + score.ToString();
    }

    //Fonction de mort du joueur / Enlever une vie
    public void DeathPlayer()
    {
        health--;
        player.transform.position = new Vector3(2.0f, 1.06f, 0.0f);
        cam.transform.position = new Vector3(2.0f, 13.0f, -10.0f);
        healthTxt.text = "Health: " + health.ToString();
        if (health == 0)
        {
            EndLevel();
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
        scoreTxt.text = "Score\n" + score.ToString();
        pieceTxt.text = "Pieces\n" + piece.ToString();
		worldTxt.text = "World\n1-1";
        timerTxt.text = "Timer\n" + timer.ToString();
        healthTxt.text = "Health\n" + health.ToString();

        Time.timeScale = 1;
        endLevelUI.SetActive(false);
    }
}
