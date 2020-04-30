using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            winPanel.SetActive(true);
        }
    }
}
