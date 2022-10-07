using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public UIManager uiManager;
    public ParticleSystem collectingParticle;
	public bool isPlaying;
	public AudioSource audioSource;
	public AudioClip collectSFX;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Set UI Manager from Canvas
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        collectingParticle = gameObject.transform.Find("Magic Sparks").GetComponent<ParticleSystem>();
	    collectingParticle.Stop();
	    audioSource = GetComponent<AudioSource>();

    }

	public void OnTriggerEnter(Collider other) //When player collides with object, destroy the apple and play the Particle effect
    {
        if (other.CompareTag("Player"))
        {
            uiManager.UpdateScore(1);
            collectingParticle.Play();
            Destroy(transform.GetChild(0).gameObject);
	        gameObject.GetComponent<BoxCollider>().enabled = false;
	        audioSource.PlayOneShot(collectSFX, 1.0f);





        }
    }
}
