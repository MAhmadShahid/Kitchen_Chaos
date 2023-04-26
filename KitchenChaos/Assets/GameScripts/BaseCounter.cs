using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    private KitchenObject m_kitchenObject;

    [SerializeField] private Transform m_counterTopPoint;
    public virtual void Interact(Player player) 
    {
        Debug.LogError("BaseCounter.Interact() not overrided!");
    }

    public void SetKitchenObject(KitchenObject p_kitchenObject)
    {
        m_kitchenObject = p_kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return m_kitchenObject;
    }

    public void ClearKitchenObject()
    {
        m_kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return m_kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_counterTopPoint;
    }
}
