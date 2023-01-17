using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Unity.VisualScripting;


public class BuildPanelUI : MonoBehaviour
{
    
    public BuildSideUI sideUI;
    public static UnityAction<BuildingData> OnPartChoosen;

    public BuildingData[] knownBuildingParts;
    public buildingPart buildingButtonPrefab;

    public GameObject itemWindow;
    public GameObject temp;
    public GameObject closeUI;

    public void onclick(BuildingData chosenData)
    {
        OnPartChoosen?.Invoke(chosenData);
        sideUI.UpdateSideDisplay(chosenData);
        closeUI.gameObject.SetActive(false);
        
    }

    public void OnClickedAllParts()
    {
        PopulateButtons();
    }

    public void OnClickEngineParts()
    {
        PopulateButtons(PartType.engine);
    }
    
    public void OnClickWheelsParts()
    {
        PopulateButtons(PartType.wheel);
    }
    
    public void OnClickBodyParts()
    {
        PopulateButtons(PartType.body);
    }

    public void PopulateButtons()
    {
        SpawnButtons(knownBuildingParts);
    }
    
    public void PopulateButtons(PartType chosenPartType)
    {
        var buildParts = knownBuildingParts.Where(p => p.PartType == chosenPartType).ToArray();
        SpawnButtons(buildParts);
    }

    public void SpawnButtons(BuildingData[] buttonData)
    {
        ClearButton();
        foreach (var data in buttonData)
        {
            var spawnedButton = Instantiate(buildingButtonPrefab, itemWindow.transform);
            spawnedButton.init(data,this);
            
        }
    }

    public void ClearButton()
    {
        foreach (var button in itemWindow.transform.Cast<Transform>())
        {
            Destroy(button.gameObject);
        }
    }
}
