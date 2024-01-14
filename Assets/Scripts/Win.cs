using UnityEngine;

public class Win : MonoBehaviour {
	[SerializeField] private GameManager m_gameManager;

	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player"))
			m_gameManager.WinLevel();
	}
}