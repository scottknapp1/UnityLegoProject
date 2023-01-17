using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public BuildPanelUI buildpanel;
    public bool UIopen = false;

    private void Start()
    {
        buildpanel.gameObject.SetActive(false);
        SetMouseCurser(buildpanel.gameObject.activeInHierarchy);
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            buildpanel.gameObject.SetActive(!buildpanel.gameObject.activeInHierarchy);
            if(buildpanel.gameObject.activeInHierarchy)buildpanel.PopulateButtons();
            SetMouseCurser(buildpanel.gameObject.activeInHierarchy);
            UIopen = !UIopen;
        }
    }

    private void SetMouseCurser(bool newState)
    {
        Cursor.visible = newState;
        Cursor.lockState = newState ? CursorLockMode.Confined : CursorLockMode.Locked;
    }





}
