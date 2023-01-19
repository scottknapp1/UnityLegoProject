using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class buildTool : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask buildModeMask;
    [SerializeField] private LayerMask deleteModeMask;
    [SerializeField] private int defaultLayerInt = 8;
    [SerializeField] private Transform rayStart;
    [SerializeField] private Material buildingMaterialPositive;
    [SerializeField] private Material buildingMaterialNegative;
    [SerializeField] private addtoparent _addtoparent;
    
    
    private bool deleteModeEnabled;
    private Camera _camera;
    
    public GameObject sideUI;
    private building spawnedParts;
    private building targetPart;
    
    public bool engineBuilt = false;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        BuildPanelUI.OnPartChoosen += ChoosePart;
    }
    
    private void OnDisable()
    {
        BuildPanelUI.OnPartChoosen -= ChoosePart;
    }

    private void ChoosePart(BuildingData data)
    {
        if (deleteModeEnabled)
        {
            if(targetPart != null && targetPart.DeleteOverlay)
            {
                targetPart.stopDeleteOverlay();
                targetPart = null;
                deleteModeEnabled = false;
            }
        }
        DeleteObjectPreview();
        
        var go = new GameObject
        {
            layer = defaultLayerInt,
            name = "Build Preview"
        };

        spawnedParts = go.AddComponent<building>();
        spawnedParts.init(data);
    }

    private void Update()
    {
        if (spawnedParts && Keyboard.current.escapeKey.wasPressedThisFrame) DeleteObjectPreview();
        
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            deleteModeEnabled = !deleteModeEnabled;
            
        }
        
        if (deleteModeEnabled)
        {
            DeleteModeLogic();
        }
        else
        {
            BuildModeLogic();
        }
    }

    private void DeleteObjectPreview()
    {
        if(spawnedParts != null)
        {
            Destroy(spawnedParts.gameObject);
            spawnedParts = null;
        }
    }

    private bool IsRayHitting(LayerMask layermask,out RaycastHit hitInfo)
    {
        var ray = new Ray(rayStart.position, _camera.transform.forward * rayDistance);
        return Physics.Raycast(ray, out hitInfo, rayDistance, layermask);
    }

    private void BuildModeLogic()
    {
        if(targetPart!= null && targetPart.DeleteOverlay)
        {
            targetPart.stopDeleteOverlay();
            targetPart = null;
        }

        if (gameObject == null) return;

        PositionBuildingPreView();
        
    }

    private void PositionBuildingPreView()
    {
        if (spawnedParts != null)
        {
            spawnedParts.UpdateMaterial(
                spawnedParts.IsOverLapping ? buildingMaterialNegative : buildingMaterialPositive);

        }


        if (IsRayHitting(buildModeMask, out RaycastHit hitInfo) && spawnedParts != null)
        {
            var gridPos = snapToGrid.Gridposision3D(hitInfo.point, 1.0f);
            spawnedParts.transform.position = gridPos;

            if (Mouse.current.leftButton.wasPressedThisFrame && !spawnedParts.IsOverLapping)
            {
                if (sideUI.activeInHierarchy)
                {
                    return;
                }

                if (spawnedParts.AssingedData.PartType == PartType.engine && engineBuilt)
                {
                    return;
                }

                if (spawnedParts.AssingedData.PartType == PartType.engine)
                {
                    engineBuilt = true;
                }

                if (engineBuilt)
                {
                    spawnedParts.PlaceBuildPart();
                    var dataCopy = spawnedParts.AssingedData;
                    spawnedParts = null;
                    ChoosePart(dataCopy);
                    _addtoparent.AddTolist(spawnedParts.gameObject);
                }

            }
        }

    }

    private void DeleteModeLogic()
    {
        if (IsRayHitting(deleteModeMask, out RaycastHit hitInfo)) 
        {
            var detectPart = hitInfo.collider.gameObject.GetComponentInParent<building>();

            if (detectPart == null) return;

            if (targetPart == null)
            {
                targetPart = detectPart;
            }

            if (detectPart != targetPart && targetPart.DeleteOverlay)
            {
                targetPart.stopDeleteOverlay();
                targetPart = detectPart;
            }

            if (detectPart == targetPart && !targetPart.DeleteOverlay)
            {
                targetPart.startDeleteOverlay(buildingMaterialNegative);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (targetPart.AssingedData.PartType == PartType.engine)
                {
                    engineBuilt = false;
                }
                Destroy(targetPart.gameObject);
                targetPart = null;
            }
        }
        else
        {
            if(targetPart!= null && targetPart.DeleteOverlay)
            {
                targetPart.stopDeleteOverlay();
                targetPart = null;
            }
        }
        
        
    }
}
