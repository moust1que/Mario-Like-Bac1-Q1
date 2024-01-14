using System.Collections;
using UnityEngine;

public class Coins : MonoBehaviour {
    public float m_rotationSpeed = 50f;
    [SerializeField] private float m_growthSpeed = 2f;
    [SerializeField] private float m_maxScale = 2f;
	[SerializeField] private GameManager m_gameManager;

    public bool m_collected = false;
    public float m_disappearTimer = 1f;

    public Collider m_coinCollider;

    private void Start() {
        m_coinCollider = GetComponent<Collider>();
    }

    private void Update() {
        if(!m_collected) {
            transform.Rotate(0.0f, m_rotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }else {
            m_disappearTimer -= Time.deltaTime;
            if (m_disappearTimer <= 0f) {
				gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!m_collected && other.CompareTag("Player")) {
            m_collected = true;
			m_gameManager.AddPiece();
        	m_coinCollider.enabled = false;
            StartCoroutine(CollectCoin());
        }
    }

    private IEnumerator CollectCoin() {
        yield return new WaitForSeconds(0.01f);
        while (transform.localScale.x < m_maxScale) {
            float growth = m_growthSpeed * Time.deltaTime;
            transform.localScale += new Vector3(1.0f, growth, 1.0f) * Time.deltaTime;

            m_rotationSpeed *= 1.5f;

            yield return null;
        }
    }
}