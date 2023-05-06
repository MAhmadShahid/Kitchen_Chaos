using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjects_SO m_kitchenObjectSO;
    public override void Interact(Player player)
    {
        // if player wants to pick up object from the counter
        if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            Debug.Log($"Player Picked The Kitchen Object: {player.GetKitchenObject().GetKitchenObjectSO().m_objectName}");
        }
        // if player wants to place an object on an empty counter
        else if (!HasKitchenObject() && player.HasKitchenObject())
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            Debug.Log($"Player Player The Kitchen Object: {GetKitchenObject().GetKitchenObjectSO().m_objectName} on counter");
        }
    }

    public override void InteractAlternate(Player player)
    {
        // if no kitchen object no need to cut
        if (!HasKitchenObject())
            return;

        // else if kitchen object is available, cut it
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(m_kitchenObjectSO, this);
    }
}

