using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedState : MonoBehaviour
{
    public Material selectedMaterial; // Creates a slot in the editor for the selected material
    private Material originalMaterial; //Stores the original material
    public AudioClip audioClip; //Creates a slot in the editor for a sound file
    private AudioSource audioSource; //Creates audio playback functionality
    private Mesh originalMesh; //Stores the original mesh
    public List<Mesh> DamageModels; //Create list of damage models to cycle through
    public List<Material> DamageMaterials; //Create list of damage materials to cycle through
    private int Health; // Health
    public RandomPrefabSpawn PrefabSpawnReceiver = null;

    // Start is called before the first frame update
    void Start()
    {
        // setup audio playback
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = audioClip;

        //Store reference to current material and model
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        originalMesh = gameObject.GetComponent<MeshFilter>().mesh;

        //Set Health
        Health = DamageModels.Count;

    }

    // Public methods that will be exposed to the Event Trigger System
    public void playAudio()
    {
        audioSource.Play();
    }
    public void setSelectedMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = selectedMaterial;
    }
    public void setNewMaterial()
    {
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
            //Health = DamageModels.Count;
            //gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
            //gameObject.GetComponent<MeshFilter>().mesh = originalMesh;
            if(PrefabSpawnReceiver != null)
            {
                PrefabSpawnReceiver.SpawnObject(gameObject.transform.parent);
            }
            Destroy(gameObject);
        }
    }
}
