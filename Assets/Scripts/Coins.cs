using System.Collections;
using UnityEngine;

public class Coins : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float growthSpeed = 2f;
    [SerializeField] private float maxScale = 2f;
	[SerializeField] private GameManager gameManager;

    private bool collected = false;
    private float disappearTimer = 1f;

    private Collider coinCollider;

    private void Start() {
        coinCollider = GetComponent<Collider>();
    }

    private void Update() {
        if(!collected) {
            transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }else {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0f) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!collected && other.CompareTag("Player")) {
            collected = true;
			gameManager.AddPiece();
            StartCoroutine(CollectCoin());
        }
    }

    private IEnumerator CollectCoin() {
        yield return new WaitForSeconds(0.01f);
        coinCollider.enabled = false;

        while (transform.localScale.x < maxScale) {
            float growth = growthSpeed * Time.deltaTime;
            transform.localScale += new Vector3(1.0f, growth, 1.0f) *Time.deltaTime;

            rotationSpeed *= 1.5f;

            yield return null;
        }
        Destroy(gameObject);
    }
}