using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider m_Slider;                             
    public Image m_FillImage;                           
    public Color m_FullHealthColor = Color.green;       
    public Color m_ZeroHealthColor = Color.red;

    private float m_CurrentHealth;
    private float m_StartingHealth = 100f;

    public float defence;

    public void Initialize(float health, float def)
    {
        m_StartingHealth = health;
        m_CurrentHealth = m_StartingHealth;
        defence = def;
        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount * defence;
        SetHealthUI();
        if (m_CurrentHealth <= 0)
            Death();
    }

    private void Death()
    {
        gameObject.GetComponent<IDeath>().Death();
    }

    private void SetHealthUI()
    {
        m_Slider.value = m_CurrentHealth / m_StartingHealth * 100;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }
}
