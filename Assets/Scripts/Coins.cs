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
        coinCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!collected)
        {
            transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }

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

            float growth = growthSpeed * Time.deltaTime;
            transform.localScale += new Vector3(1.0f, growth, 1.0f) *Time.deltaTime;

            rotationSpeed *= 1.5f;

            yield return null;
        }
        Destroy(gameObject);
    }
}

