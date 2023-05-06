using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjects_SO m_kitchenObject;
    private IKitchenObjectParent m_kitchenObjectParent;


    public void SetKitchenObjectParent(IKitchenObjectParent p_kitchenObjectParent)
    {

        if(m_kitchenObjectParent != null)
            m_kitchenObjectParent.ClearKitchenObject();

        // update counter here and also tell it to update this kitchen object for its fields
        m_kitchenObjectParent = p_kitchenObjectParent;
        if (m_kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObject already has a parent!");
        }
        m_kitchenObjectParent.SetKitchenObject(this);

        // also update parent 
        transform.parent = m_kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        Debug.Log("Kitchen Object Parent: Changed Successfully");
    }

    public KitchenObjects_SO GetKitchenObjectSO()
    {
        return m_kitchenObject;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return m_kitchenObjectParent;
    }

    public void DestroySelf()
    {
        m_kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjects_SO p_kitchenObjectSO, IKitchenObjectParent p_kitchenObjectParent)
    {
        Transform spawnedKitchenObjectTransform = Instantiate(p_kitchenObjectSO.m_prefab);
        KitchenObject spawnedKitchenObject = spawnedKitchenObjectTransform.GetComponent<KitchenObject>();
        spawnedKitchenObject.SetKitchenObjectParent(p_kitchenObjectParent);

        return spawnedKitchenObject;
    }
}
