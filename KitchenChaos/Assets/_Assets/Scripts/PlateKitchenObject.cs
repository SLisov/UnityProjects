using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectsSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectsSO> ();
    }

    public void AddIngredient(KitchenObjectsSO kitchenObjectSO)
    {
        kitchenObjectSOList.Add(kitchenObjectSO);
    }
}
