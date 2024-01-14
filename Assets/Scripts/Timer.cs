using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
	[SerializeField] private GameManager m_gameManager;
    [SerializeField] private TextMeshProUGUI m_timerText;
 
 

    public float m_timeRemaining;

	public float m_levelTime;

	private float m_time;
	private float m_timerInterval = 1.0f;
	private float m_tick;
	
	private void Awake() {
		m_levelTime = m_gameManager.GetComponent<GameManager>().m_timer;
		m_timeRemaining = m_levelTime;
		m_time = (int)Time.time;
		m_tick = m_timerInterval;
	}
 

	private void Update() {
		m_time = (int)Time.time;

		if(m_time == m_tick) {
			m_tick = m_time + m_timerInterval;
			TimerExecute();
		}
 
    }

	private void TimerExecute() {
		m_gameManager.GetComponent<GameManager>().m_timer = m_timeRemaining;
		if(m_timeRemaining > 0.0f)
			m_timeRemaining--;
		else if(m_timeRemaining == 0.0f) {
			m_gameManager.DeathPlayer();
			m_timeRemaining = m_levelTime;
		}
 

	}
}