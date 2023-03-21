using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedState : MonoBehaviour
{
    public Material selectedMaterial; // Creates a slot in the editor for the selected material
    private Material originalMaterial; //Stores the original material
    public List<AudioClip> audioClip; //Creates a slot in the editor for a sound file
    private AudioSource audioSource; //Creates audio playback functionality
    private Mesh originalMesh; //Stores the original mesh
    public List<Mesh> DamageModels; //Create list of damage models to cycle through
    public List<Material> DamageMaterials; //Create list of damage materials to cycle through
    private int Health; // Health
    public RandomPrefabSpawn PrefabSpawnReceiver = null; //Set the receiver that we call to
    Renderer ObjectRender; //Used to make the object visible or invisible
    ParticleSystem Explosion; //Set Particle System

    // Start is called before the first frame update
    void Start()
    {
        // setup audio playback
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = audioClip[0];

        //Store reference to current material and model
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        originalMesh = gameObject.GetComponent<MeshFilter>().mesh;

        //Store reference to Explosion
        Explosion = gameObject.GetComponent<ParticleSystem>();

        //Set Health
        Health = DamageModels.Count;

    }

    // Public methods that will be exposed to the Event Trigger System
    public void playAudio()
    {
        //When activated, play audio
        audioSource.Play();
    }
    public void setSelectedMaterial()
    {
        //Set material for when object is selected
        gameObject.GetComponent<MeshRenderer>().material = selectedMaterial;
    }
    public void setNewMaterial()
    {
        //Set new Material to one in the material's list matching the health variable
        gameObject.GetComponent<MeshRenderer>().material = DamageMaterials[Health];
    }

    public void TakeDamage()
    {
        // Debug Message
        Debug.Log(gameObject.GetComponent<MeshFilter>().mesh);

        if (Health > 0)
        {
            Health--;
            gameObject.GetComponent<MeshFilter>().mesh = DamageModels[Health];
        }
        else
        {
            audioSource.clip = audioClip[1];
            audioSource.time = 0.3f;
            audioSource.Play();
            Explosion.Play();

            //Set object render to false so it's invisisble before being deleted.
            ObjectRender = GetComponent<MeshRenderer>();
            ObjectRender.enabled = false;

            //Start timer for when to destroy and spawn the next object
            StartCoroutine(DestroyThenSpawn());
        }
    }

    IEnumerator DestroyThenSpawn()
    {
        //Wait 5 seconds
        yield return new WaitForSeconds(5);
        //Send a signal to our prefab spawner to activate one of its functions
        if (PrefabSpawnReceiver != null)
        {
            //Spawn the object into this one's parent - that being the Prefab Spawner itself
            PrefabSpawnReceiver.SpawnObject(gameObject.transform.parent);
        }
        //Destroy this current object
        Destroy(gameObject);
    }
}
