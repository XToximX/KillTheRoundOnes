using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager I;

    private int currentCoins = 0;

    private int coinsLeftToAdd = 0;
    private bool isAddingCoins = false;

    private int coinsLeftToRemove = 0;
    private bool isRemovingCoins = false;

    [SerializeField] private TextMeshProUGUI coinsDisplay;
    [SerializeField] private int staringCoins = 100;

    private int baseHP = 100;
    public int HP { get; private set; }

    [SerializeField] private TextMeshProUGUI hpDisplay;


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
    }

    public bool Buy(int cost, Action action)
    {
        if (currentCoins >= cost)
        {
            coinsLeftToRemove += cost;
            
            if (!isRemovingCoins)
                StartCoroutine(RemoveCoins());

            action.Invoke();

            return true;
        }

        EffectsManager.I.TextFlash(coinsDisplay);
        return false;
    }

    public void AddCoins(int coins)
    {
        //currentCoins += coins;
        //UpdateCoinsDisplay();

        coinsLeftToAdd += coins;

        if (!isAddingCoins)
            StartCoroutine(AddingCoins());
    }

    private void UpdateCoinsDisplay()
    {
        coinsDisplay.text = currentCoins.ToString();
    }

    IEnumerator AddingCoins()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        
        isAddingCoins = true;

        while(coinsLeftToAdd > 0)
        {
            if(coinsLeftToAdd >= 50)
            {
                currentCoins += 50;
                coinsLeftToAdd -= 50;
            }
            else
            {
                currentCoins += coinsLeftToAdd;
                coinsLeftToAdd = 0;
            }

            UpdateCoinsDisplay();
            yield return wait;
        }

        isAddingCoins = false;
    }

    IEnumerator RemoveCoins()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        isRemovingCoins = true;

        while (coinsLeftToRemove > 0)
        {
            if (coinsLeftToRemove >= 50)
            {
                currentCoins -= 50;
                coinsLeftToRemove -= 50;
            }
            else
            {
                currentCoins -= coinsLeftToRemove;
                coinsLeftToRemove = 0;
            }

            UpdateCoinsDisplay();
            yield return wait;
        }

        isRemovingCoins = false;
    }

    public void TakeDamage(int val)
    {
        HP -= val;
        if (HP < 0)
            Debug.LogWarning("PLAYER DEAD");

        EffectsManager.I.TextFlash(hpDisplay);
        UpdateHPDisplay();
    }

    private void UpdateHPDisplay()
    {
        hpDisplay.text = HP.ToString();
    }
}
