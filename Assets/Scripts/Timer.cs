using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
	[SerializeField] GameManager gameManager;
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeRemaining;

	[SerializeField] private float levelTime = 20.0f;

	private float time;
	private float timerInterval = 1.0f;
	private float tick;
	
	private void Awake() {
		timeRemaining = levelTime;
		time = (int)Time.time;
		tick = timerInterval;
	}

	private void Update() {
		time = (int)Time.time;

		if(time == tick) {
			tick = time + timerInterval;
			TimerExecute();
		}
    }

	private void TimerExecute() {
        timerText.text = $"Timer\n{timeRemaining}";
		if(timeRemaining > 0.0f)
			timeRemaining--;
		else if(timeRemaining == 0.0f) {
			gameManager.DeathPlayer();
			timeRemaining = levelTime;
		}
	}
}