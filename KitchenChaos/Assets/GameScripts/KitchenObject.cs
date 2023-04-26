using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjects_SO m_kitchenObject;
    private IKitchenObjectParent m_kitchenObjectParent;

    public KitchenObjects_SO GetKitchenObjectSO()
    {
        return m_kitchenObject;
    } 

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
    }

    public KitchenObjects_SO GetKitchenObjectsSO()
    {
        return m_kitchenObject;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return m_kitchenObjectParent;
    }
}
