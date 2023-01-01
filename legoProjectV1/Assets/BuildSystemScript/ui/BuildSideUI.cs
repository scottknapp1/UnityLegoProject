using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildSideUI : MonoBehaviour
{
    public Image BuildingImage;
    public TMP_Text BuildingText;
    
    private void Start()
    {
        BuildingImage.sprite = null;
        BuildingText.color = Color.clear;
        BuildingText.text = "";
    }

    public void UpdateSideDisplay(BuildingData data)
    {
        BuildingImage.sprite = data.Icon;
        BuildingImage.color = Color.white;


    }
}
