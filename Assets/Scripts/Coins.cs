using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float growthSpeed = 2f;
    public float maxScale = 2f;

    private bool collected = false;
    private float disappearTimer = 1f;

    private Collider coinCollider;

    void Start()
    {
        // Get the Collider component on Start
        coinCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!collected)
        {
            // Rotate the coin
            transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }

        // Disappear after a certain time
        if (collected)
        {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true;
            StartCoroutine(CollectCoin());
        }
    }

    IEnumerator CollectCoin()
    {

        yield return new WaitForSeconds(0.01f);
        coinCollider.enabled = false;

        while (transform.localScale.x < maxScale)
        {
            // Increase the size of the coin uniformly
            float growth = growthSpeed * Time.deltaTime;
            transform.localScale += new Vector3(1.0f, growth, 1.0f) *Time.deltaTime;

            // Increase rotation speed
            rotationSpeed *= 1.5f;

            yield return null;
        }

        // Optional: Perform any additional actions after the coin has reached its maximum size.

        // Destroy the coin once collection is complete
        Destroy(gameObject);
    }
}

