using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter m_containerCounter;
    private Animator m_animator;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log($"In Container Visual: {gameObject.transform.name}");
        m_containerCounter.m_OnPlayerGrabbedObject += containerCounter_OnPlayerGrabbedObject;
    }

    private void containerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        m_animator.SetTrigger(OPEN_CLOSE);
    }
}
