using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler m_OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjects_SO m_kitchenObjectSO;

    private void Start()
    {
        Debug.Log($"In Container: {gameObject.transform.name}");
    }
    public override void Interact(Player player)
    {
        // if player already holds an object, don't spawn another one
        if (player.HasKitchenObject()) return;

        KitchenObject.SpawnKitchenObject(m_kitchenObjectSO, player);

        m_OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }


}
