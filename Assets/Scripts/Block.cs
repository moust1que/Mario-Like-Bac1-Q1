using UnityEngine;

public class Block : MonoBehaviour {
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private LayerMask m_playerLayer;
	private MarioMovement m_marioMovement;
    public int m_bonus;
	// private bool m_mushroomTime = false;

    private void OnCollisionEnter(Collision collision) {
		//Detecte si c'est le joueur qui collisionne
        if (IsPlayerCollision(collision)) {
			//Detecte si la collision vient du dessous
            if (IsCollisionFromBottom(collision)) {
                if (m_bonus == 1) { //Block cassable
					if(m_gameManager.m_bigMario){
                    	m_bonus = 0;
                    	gameObject.SetActive(false);
                    	m_gameManager.AddScore(50); //Ajout du score
					}
                }else if (m_bonus == 2) { //Block piece
                    m_bonus = 0;
                    m_gameManager.AddPiece(); //Ajout de 1 piece
                    m_gameManager.AddScore(200); //Ajout du score
                }else if (m_bonus == 3) { //Block champi
                    m_bonus = 0;
					m_marioMovement = collision.gameObject.GetComponent<MarioMovement>();
					m_marioMovement.SetBigMario();
                }
            }
        }
    }

	//Fonction de detection si c'est le joueur qui collisionne
    private bool IsPlayerCollision(Collision collision) {
        return (m_playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

	//Fonction de detection si la collision vient du dessous
    private bool IsCollisionFromBottom(Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            if (contact.normal.y > 0.8f) {
                return true;
            }
        }
        return false;
    }
}