using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 50;
    int currenHealth;
    public TextMeshProUGUI text;
    public GameObject healthtext;

    private void Start()
    {
        currenHealth = MaxHealth;
    }

    private void Update()
    {
        text.text = currenHealth + "";

        if (currenHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(healthtext);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        currenHealth -= damage;      
    }
   
    




   
}
