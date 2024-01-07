using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeunFunnies : MonoBehaviour
{
    public ParticleSystem leParticleSystem;
    public Sprite[] spriteList;
    public float changeInterval = 1.0f;

    private Material particleMaterial;
    private int currentIndex = 0;

    void Start()
    {
        if (leParticleSystem == null)
        {
            leParticleSystem = GetComponent<ParticleSystem>();
        }

        if (leParticleSystem != null)
        {
            ParticleSystemRenderer particleRenderer = leParticleSystem.GetComponent<ParticleSystemRenderer>();
            if (particleRenderer != null)
            {
                particleMaterial = new Material(particleRenderer.material);
                particleRenderer.material = particleMaterial;
            }
            else
            {
                Debug.LogError("Particle system does not have a ParticleSystemRenderer component.");
            }
        }
        else
        {
            Debug.LogError("ParticleImageChanger script is not attached to a GameObject with a ParticleSystem component.");
        }

        // Start the image changing coroutine
        StartCoroutine(ChangeImageCoroutine());
    }

    IEnumerator ChangeImageCoroutine()
    {
        while (true)
        {
            // Change the particle system's image to a random sprite from the list
            ChangeParticleImage(GetRandomSprite());

            // Wait for the specified interval before changing the image again
            yield return new WaitForSeconds(changeInterval);
        }
    }

    Sprite GetRandomSprite()
    {
        if (spriteList.Length == 0)
        {
            Debug.LogError("Sprite list is empty. Add sprites to the list in the inspector.");
            return null;
        }

        // Choose a random sprite from the list
        int randomIndex = Random.Range(0, spriteList.Length);
        return spriteList[randomIndex];
    }

    void ChangeParticleImage(Sprite sprite)
    {
        if (particleMaterial != null)
        {
            particleMaterial.SetTexture("_MainTex", sprite.texture);
        }
    }
}
