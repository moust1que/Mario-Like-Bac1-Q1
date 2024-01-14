using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_endLevelUI;
	[SerializeField] private GameObject m_winLevelUI;
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_cam;

    //Valeur par defaut du HUD
    private int m_piece = 0;
	private int m_pieceForLive = 0;
    private int m_health = 3;
    private int m_score = 0;
    public float m_timer = 0.0f;
	private float  m_bckpTimer = 0.0f;

    //Valeur des textes sur l'UI
    [SerializeField] private TMPro.TextMeshProUGUI m_scoreTxt;
    [SerializeField] private TMPro.TextMeshProUGUI m_pieceTxt;
	[SerializeField] private TMPro.TextMeshProUGUI m_worldTxt;
    [SerializeField] private TMPro.TextMeshProUGUI m_timerTxt;
    [SerializeField] private TMPro.TextMeshProUGUI m_healthTxt;

	[SerializeField] private List<GameObject> m_enemies = new();
	[SerializeField] private List<Vector3> m_enemiesTransform = new();
	[SerializeField] private List<GameObject> m_luckyBlocks = new();
	[SerializeField] private List<GameObject> m_coins = new();

	public bool m_bigMario = false;

	private void Awake() {
		m_bckpTimer = m_timer;
		for(int i = 0; i < m_enemies.Count; i++) {
			m_enemiesTransform[i] = m_enemies[i].transform.position;
		}
	}

    //Fonction d'ajout d'une piece au HUD
    public void AddPiece() {
        m_pieceForLive++;
    }

    //Fonction d'ajout du score "addScore" au HUD
    public void AddScore(int addScore) {
        m_score += addScore;
    }

    //Fonction de mort du joueur / Enlever une vie
    public void DeathPlayer() {
		if(m_bigMario) {
			MarioMovement marioMovement = GameObject.Find("Mario").GetComponent<MarioMovement>();
			marioMovement.SetSmallMario();
		}else {
			Reset();
			m_health--;
		}
		if (m_health == 0) {
			EndLevel();
		}
    }

	private void Reset() {
		// Reset Enemies
		Enemy enemy;
		for(int i = 0; i < m_enemies.Count; i++) {
			enemy = m_enemies[i].GetComponent<Enemy>();
			m_enemies[i].SetActive(true);
			m_enemies[i].transform.position = m_enemiesTransform[i];
			enemy.m_health = 1;
		}

		// Reset Score
		m_score = 0;

		// Reset Coins
		m_piece = 0;
		m_pieceForLive = 0;

		// Reset Lucky Blocks
		Block block;
		for(int i = 0; i < m_luckyBlocks.Count; i++) {
			block = m_luckyBlocks[i].GetComponent<Block>();
			if(i == 0)
				block.m_bonus = 3;
			else if(i == 5 || i == 7) {
				block.m_bonus = 1;
				m_luckyBlocks[i].SetActive(true);
			}else
				block.m_bonus = 2;
		}

		// Reste Timer
		Timer timer;
		timer = GameObject.Find("TimeTxt").GetComponent<Timer>();
		m_timer = m_bckpTimer;
		timer.m_timeRemaining = timer.m_levelTime;

		// Reset Coins
		Coins coin;
		for(int i = 0; i < m_coins.Count; i++) {
			m_coins[i].SetActive(true);
			m_coins[i].transform.localScale = new Vector3(1.0f, 0.1294303f, 1.0f);
			coin = m_coins[i].GetComponent<Coins>();
			coin.m_rotationSpeed = 50.0f;
			coin.m_collected = false;
			coin.m_coinCollider.enabled = true;
			coin.m_disappearTimer = 1.0f;
		}

		// Reset flag
		GameObject flag;
		flag = GameObject.Find("flag down").gameObject;
		flag.transform.position = new Vector3(214.947861f,22.1906128f,0.839488864f);
		FlagDown flagDown;
		flagDown = GameObject.Find("flag down").GetComponent<FlagDown>();
		flagDown.m_isFlagLowered = false;
		flagDown.m_playerControlsDisabled = false;

		// Reset Mario
		m_player.GetComponent<MarioMovement>().SetSmallMario();
		m_player.GetComponent<MarioMovement>().m_moveSpeed = 20.0f;
		m_player.transform.position = new Vector3(2.0f, 1.05f, 0.0f);
        m_cam.transform.position = new Vector3(2.0f, 13.0f, -10.0f);
		m_player.GetComponent<MarioMovement>().m_velocity = Vector3.zero;
	}

    //Fonction de fin de level
    private void EndLevel() {
        Time.timeScale = 0.0f;
        m_endLevelUI.SetActive(true);
    }

	public void WinLevel() {
		Time.timeScale = 0.0f;
        m_winLevelUI.SetActive(true);
	}

    //Fonction de Restart du niveau
    public void Restart() {
		Time.timeScale = 1.0f;
        m_endLevelUI.SetActive(false);
        m_winLevelUI.SetActive(false);
		m_player.GetComponent<MarioMovement>().enabled = true;
		Reset();
		m_health = 3;
    }

    private void Start() {
		PrintText();

        Time.timeScale = 1;
        m_endLevelUI.SetActive(false);
    }
    /// piece et vie 
    private void Update() {
        if (m_pieceForLive == 3) {
			m_piece += m_pieceForLive;
			m_pieceForLive = 0;
            m_health++;
        }

		PrintText();
    }

	private void PrintText() {
		m_scoreTxt.text = $"Mario\n{m_score}";
        m_pieceTxt.text = $"Pieces\n{m_piece + m_pieceForLive}";
		m_worldTxt.text = "World\n1-1";
        m_timerTxt.text = $"Timer\n{m_timer}";
        m_healthTxt.text = $"Health\n{m_health}";
	}
}