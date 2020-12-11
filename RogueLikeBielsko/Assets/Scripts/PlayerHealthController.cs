using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;
    public float damageInvincLength = 1f;
    private float invincCount;

    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;

        //currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if(invincCount <= 0)
            {
                PlayerControler.instance.bodySR.color = new Color(PlayerControler.instance.bodySR.color.r, PlayerControler.instance.bodySR.color.g, PlayerControler.instance.bodySR.color.b, 1f);
            }
        }
    }
    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
             currentHealth--;
            AudioManager.instance.PlaySFX(11);

            invincCount = damageInvincLength;
            PlayerControler.instance.bodySR.color = new Color(PlayerControler.instance.bodySR.color.r, PlayerControler.instance.bodySR.color.g, PlayerControler.instance.bodySR.color.b, .5f);

            if (currentHealth <= 0)
            {
                PlayerControler.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);

                AudioManager.instance.PlayGameOver();
                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }
    public void MakeInvincibility(float length)
    {
        invincCount = length;
        PlayerControler.instance.bodySR.color = new Color(PlayerControler.instance.bodySR.color.r, PlayerControler.instance.bodySR.color.g, PlayerControler.instance.bodySR.color.b, .5f);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth> maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
