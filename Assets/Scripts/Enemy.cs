using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private GameManager m_gameManager;

    public int m_health = 1;
    [SerializeField] private bool m_isHit = false;

    //de lorenzo code
    [SerializeField] private LayerMask m_playerLayer;

    // Update is called once per frame
    private void Update() {
        //Si la vie = 0, on esactive l'enemie et on ajoute le score
        if(m_health == 0) {
			gameObject.SetActive(false);
            m_gameManager.AddScore(500);
        }
        //Si l'enemie est touché par le joueur, on le fait mourir
        if (m_isHit) {
            m_isHit = false;
            m_gameManager.DeathPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision) {
		if (IsPlayerCollision(collision)) { //Detecte si c'est le joueur qui le collisionne
            if (CollisionSide(collision) == "Top") { //Si la collision vient du haut
                m_health = 0; //Definition de la vie à 0
            }else if(CollisionSide(collision) == "Side") { //Si la collision vient du coté
                m_isHit = true; //Definition de m_isHit à vrai
            }
        }
    }

    //Fonction de detection du joueur
    private bool IsPlayerCollision(Collision collision) {
        return (m_playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

    //Fonction de detection de où vient la collision
    private string CollisionSide(Collision collision) {
		foreach(ContactPoint contact in collision.contacts) {
			if(contact.normal.y < -0.5f)
				return "Top";
		}
		return "Side";
    }
}