using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    [SerializeField] public AnimationCurve healthCurve;
    public float animationDuration = 1f;
    public Slider slider;
    [SerializeField] public Image hurtImage = null;
    [SerializeField] public float hurtTimer = 0.1f;
    [SerializeField] private AudioSource pain;
    public HealthBar healthBar;
    public float time;
    private Coroutine takeDamageCO;
    private Coroutine giveHealth;




    //set max health amount and slider to full
    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        

    }
    //upon input of L we take damage and the damage sfx is played
    //upon input of K we recieve health
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
            


        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GiveHealth(20);


        }
    }
    
    //here we set slider values for max
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

    }
   
    //here we set slider value of the current health
    public void SetHealth(float health)
    {
        slider.value = health;
        
        
        

    }

    //here we use a curve for our damage
    //so over time health slowly reduces and doesnt snap in value
    IEnumerator TakeDamageSmooth(float damage)
    {
        time = 0f;
        float startAmount = currentHealth;
        float targetAmount = currentHealth - damage;
        
        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float curvY = healthCurve.Evaluate(time);
            currentHealth = Mathf.Lerp(startAmount, targetAmount, curvY);  
            slider.value = Mathf.Lerp(startAmount, targetAmount, curvY);

            yield return null;

        }
        currentHealth = targetAmount;
        slider.value = currentHealth;
        pain.Play();






    }

    public void TakeDamage(float damage) 
    {
        if (takeDamageCO != null)
        {
            StopCoroutine(takeDamageCO);

        }
        takeDamageCO = StartCoroutine(TakeDamageSmooth(damage));
    
    
    }

    //here we use a curve for our adding health
    //so over time health slowly goes up and doesnt snap in value

    IEnumerator GiveHealthSmooth(float healing)
    {
        time = 0f;
        float startAmount = currentHealth;
        float targetAmount = currentHealth + healing;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float curvY = healthCurve.Evaluate(time);
            currentHealth = Mathf.Lerp(startAmount, targetAmount, curvY);   
            slider.value = Mathf.Lerp(startAmount, targetAmount, curvY);

            yield return null;

        }
        currentHealth = targetAmount;
        slider.value = currentHealth;
        





    }

    public void GiveHealth(float healing)
    {
        if (giveHealth != null)
        {
            StopCoroutine(giveHealth);

        }
        giveHealth = StartCoroutine(GiveHealthSmooth(healing));


    }


}
