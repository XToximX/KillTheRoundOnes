using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager I;

    private int currentCoins = 0;

    [SerializeField] private TextMeshProUGUI coinsDisplay;
    [SerializeField] private int staringCoins = 5;

    private int baseHP = 10;
    public int HP { get; private set; }

    [SerializeField] private TextMeshProUGUI hpDisplay;

    [SerializeField] AudioClip hpAudioClip;

    [SerializeField] GameObject deathCanva;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    private void Start()
    {
        currentCoins = staringCoins;
        UpdateCoinsDisplay();

        HP = baseHP;
        UpdateHPDisplay();

        deathCanva.SetActive(false);
    }

    public bool Buy(int cost, Action action)
    {
        if (currentCoins >= cost)
        {
            currentCoins -= cost;
            UpdateCoinsDisplay();

            action.Invoke();

            return true;
        }

        EffectsManager.I.TextFlash(coinsDisplay);
        return false;
    }

    public void AddCoins(int coins)
    {
        currentCoins += coins;
        UpdateCoinsDisplay();
    }

    private void UpdateCoinsDisplay()
    {
        coinsDisplay.text = currentCoins.ToString();
    }

    public void TakeDamage(int val)
    {
        HP -= val;

        EffectsManager.I.TextFlash(hpDisplay);
        AudioManager.I.PlaySFX(hpAudioClip);

        UpdateHPDisplay();

        if (HP <= 0)
            Death();
    }

    private void UpdateHPDisplay()
    {
        hpDisplay.text = HP.ToString();
    }

    private void Death()
    {
        TimeManager.I.PauseTime();
        InputController.I.DisableInput();
        deathCanva.SetActive(true);
    }
}
