using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player not carrying anything
            }
            
        }
        else
        {
            //There is KitchenObject
            if(player.HasKitchenObject())
            {
                // Player is carrying something
                if(player.GetKitchenObject() is PlateKitchenObject)
                {
                    // Player is holding a Plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    plateKitchenObject.AddIngredient(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                }
            }
            else 
            {
                // Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
