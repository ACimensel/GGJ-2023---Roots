using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public bool IsBad;
    public MeshRenderer MeshRenderer;
    public Material GoodMaterial;
    public Material BadMaterial;



    public Vector3 CurrentScale;
    private Vector3 _startingScale;

    public Vector3 MaxScale;

    public Vector3 GrowthSpeed;

    //[SerializeField] float growthSpeed = 5f;
    //[SerializeField] int finalGrowthStage = 100;
    //[SerializeField] int currentGrowthStage = 0;

    [SerializeField] ParticleSystem ReadyParticles;

    public bool FullyGrown;
    //[SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip audioClip;

    void Awake()
    {
        _startingScale = transform.localScale; // Store the starting scale
        transform.localScale = Vector3.zero;
    }

    //void OnEnable()
    //{
    //    currentScale = Vector3.zero;
    //}

    private void Update()
    {
        CurrentScale = transform.localScale;

        if (!FullyGrown && CurrentScale.magnitude <= MaxScale.magnitude)
        {
            UpdateGrowth();
        }
        else if (!FullyGrown)
        {
            OnFullyGrown();
        }
    }

    protected virtual void UpdateGrowth()
    {
        transform.localScale = transform.localScale + GrowthSpeed * Time.deltaTime;
    }

    public virtual void OnFullyGrown()
    {
        transform.localScale = MaxScale;
        ReadyParticles.Play();
        FullyGrown = true;
        ScoreCounterManager.Singleton.Count();
    }


    protected virtual void Initialize()
    {

    }

    protected virtual void UpdatePosition()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ScoreCounterManager.Singleton.Count();
        if (other.TryGetComponent<PlantGrowth>(out PlantGrowth plantGrowth))
        {
            if (plantGrowth.IsBad && !this.IsBad)//if other plant is bad, make this plant bad
            {

                Corrupt();
            }
            else if (this.IsBad && !plantGrowth.IsBad)
            {

                plantGrowth.Corrupt();
            }
        }
    }

    public void Corrupt()
    {
        this.IsBad = true;
        MeshRenderer.material = BadMaterial;
        gameObject.layer = LayerMask.NameToLayer("Bad");
        ScoreCounterManager.Singleton.Count();
    }

    //public void Harvest()
    //{
    //    if (FullyGrown)
    //    {
    //        print("Harvest");
    //    }
    //    else
    //    {
    //        print("HARVESTED TOO EARLY");
    //    }


    //}
}