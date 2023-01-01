using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingPart : MonoBehaviour
{
    private Button _button;
    private BuildingData _assingedData; 
    BuildPanelUI _parentDisplay;

    private void Awake()
    {
        
    }

    public void init(BuildingData assingedData,BuildPanelUI parentDisplay)
    {
        _assingedData = assingedData;
        _parentDisplay = parentDisplay; 
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnButtonClick);
        _button.GetComponent<Image>().sprite = _assingedData.Icon;
    }

    private void OnButtonClick()
    {
        _parentDisplay.onclick(_assingedData);
    }
}
