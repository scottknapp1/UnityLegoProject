using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]

public class building : MonoBehaviour
{
    private Renderer _renderer;
    private Material _defaultMaterial;

    private BuildingData _assingedData;
    private BoxCollider _boxCollider;
    private GameObject _graphic;

    private Transform _colliders;
    private bool isOverLapping;

    public bool IsOverLapping => isOverLapping;

    private bool deleteOverlay;
    public bool DeleteOverlay => deleteOverlay;
    public BuildingData AssingedData => _assingedData;

    public void init(BuildingData data)
    {
        _assingedData = data;
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.size = _assingedData.BuildingSize;
        _boxCollider.center = new Vector3(0, (_assingedData.BuildingSize.y + .2f) * 0.5f, 0);
        _boxCollider.isTrigger = true;

        //var rb = gameObject.AddComponent<Rigidbody>();
        //rb.isKinematic = false;

        _graphic = Instantiate(data.PreFab, transform);
        _renderer = _graphic.GetComponentInChildren<Renderer>();
        _defaultMaterial = _renderer.material;


        _colliders = _graphic.transform.Find("Colliders");
        if (_colliders != null) _colliders.gameObject.SetActive(false);
    }

    public void PlaceBuildPart()
    {
        _boxCollider.enabled = false;
        if(_colliders != null ) _colliders.gameObject.SetActive(true);
        UpdateMaterial(_defaultMaterial);
        gameObject.layer = 10;
        gameObject.name = _assingedData.DisplayName;

    }

    public void UpdateMaterial(Material newMaterial)
    {
        if(_renderer == null)return;
        if(_renderer.material != newMaterial)
        {
            _renderer.material = newMaterial;
        }
    }
    
    public void startDeleteOverlay(Material deleteMat)
    {
        UpdateMaterial(deleteMat);
        deleteOverlay = true;
    }

    public void stopDeleteOverlay()
    {
        UpdateMaterial(_defaultMaterial);
        deleteOverlay = false;
    }

    private void OnTriggerStay(Collider other)
    {
        isOverLapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOverLapping = false;
    }
}

