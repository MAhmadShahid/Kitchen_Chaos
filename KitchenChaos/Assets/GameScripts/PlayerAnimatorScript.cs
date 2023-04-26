using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    // references
    [SerializeField] private Player m_playerScript;
    private Animator m_animator;

    // fields
    private const string IS_WALKING = "IsWalking";
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetBool(IS_WALKING, m_playerScript.IsPlayerWalking());
    }
}
