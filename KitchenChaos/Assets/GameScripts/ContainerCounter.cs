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
        if (player.HasKitchenObject()) return;

        Transform kitchenObjectTransform = Instantiate(m_kitchenObjectSO.m_prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        m_OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }


}
