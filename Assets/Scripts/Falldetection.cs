using UnityEngine;

public class FallDetection : MonoBehaviour {
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private GameObject m_player;

    void Update() {
        if (m_player.transform.position.y <= -2) {
            m_gameManager.DeathPlayer();
        }
    }
}