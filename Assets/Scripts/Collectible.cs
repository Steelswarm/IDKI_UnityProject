using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public UIManager uiManager;
    public ParticleSystem collectingParticle;
    public bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        //Set UI Manager from Canvas
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        collectingParticle = gameObject.transform.Find("Magic Sparks").GetComponent<ParticleSystem>();
        collectingParticle.Stop();

    }

    void Update()
    {


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.UpdateScore(1);
            collectingParticle.Play();
            Destroy(transform.GetChild(0).gameObject);
            gameObject.GetComponent<BoxCollider>().enabled = false;



        }
    }
}
