using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform gameArea;
    [SerializeField] GameObject losePanel;

    List<Enemy> enemies;

    public bool isGameOver { get; private set; } = false;
    private void Awake()
    {
        enemies = new List<Enemy>();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Enemy>())
            {
                Enemy enemyScript = child.GetComponent<Enemy>();
                enemies.Add(enemyScript);
                enemyScript.GameArea = gameArea;
            }
        }
    }

    public void ShowLosePanel()
    {
        isGameOver = true;
        losePanel.SetActive(true);
    }
}
