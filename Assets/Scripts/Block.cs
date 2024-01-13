using UnityEngine;

public class Block : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LayerMask playerLayer;
    public int bonus;

    private void OnCollisionEnter(Collision collision) {
		//Detecte si c'est le joueur qui collisionne
        if (IsPlayerCollision(collision)) {
			//Detecte si la collision vient du dessous
            if (IsCollisionFromBottom(collision)) {
                if (bonus == 1) { //Block cassable
                    bonus = 0;
                    gameObject.SetActive(false);
                    gameManager.AddScore(50); //Ajout du score
                }else if (bonus == 2) { //Block piece
                    bonus = 0;
                    gameManager.AddPiece(); //Ajout de 1 piece
                    gameManager.AddScore(200); //Ajout du score
                }else if (bonus == 3) { //Block champi
                    bonus = 0;
                    Debug.Log("Block champi !");
                }
            }
        }
    }

	//Fonction de detection si c'est le joueur qui collisionne
    private bool IsPlayerCollision(Collision collision) {
        return (playerLayer.value & 1 << collision.gameObject.layer) > 0;
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