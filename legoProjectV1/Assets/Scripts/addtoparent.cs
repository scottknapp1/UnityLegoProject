using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class addtoparent : MonoBehaviour
{
   public GameObject[] objToFind;
   public GameObject ParentPart;
   private void Start()
   {
      }

   private void Update()
   {
      
      ParentPart = GameObject.Find("Engine").transform.GetChild(0).gameObject;
      
      if(GameObject.FindGameObjectsWithTag("parts")is null)return;
      objToFind = GameObject.FindGameObjectsWithTag("parts");
      foreach (var part in objToFind)
      {
         part.transform.root.parent = ParentPart.transform;
      }
   }

   public void AddTolist(GameObject gameObject)
   {
      objToFind.Append(gameObject);
      gameObject.transform.parent = ParentPart.transform;
      if (objToFind[0]is null) return;
      objToFind[0].transform.root.parent = ParentPart.transform;
   }
   
}

