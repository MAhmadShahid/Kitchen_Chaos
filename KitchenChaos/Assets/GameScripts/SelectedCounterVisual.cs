using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter m_baseCounter;
    [SerializeField] private GameObject[] m_visualGameObjectArray;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.m_selectedCounter == m_baseCounter)
            Show();
        else
            Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Show()
    {
        foreach(GameObject visualGameObject in m_visualGameObjectArray)
            visualGameObject.SetActive(true);
    }

    void Hide()
    {
        foreach (GameObject visualGameObject in m_visualGameObjectArray)
            visualGameObject.SetActive(false);
    }
}
