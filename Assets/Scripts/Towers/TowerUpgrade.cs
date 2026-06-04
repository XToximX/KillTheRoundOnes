using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerUpgrade : MonoBehaviour
{
    public int CurrentLvl { get; private set; }
    protected TowerBehaviour towerBehaviour;

    private void Start()
    {
        towerBehaviour = GetComponent<TowerBehaviour>();

        CurrentLvl = 0;
    }

    public void LvlUp()
    {
        if(CurrentLvl >= 3)
        {
            Debug.Log("Tower is max lvl");
            return;
        }

        CurrentLvl++;

        switch(CurrentLvl)
        {
            case 1:
                Lvl1(); break;
            case 2:
                Lvl2(); break;
            case 3:
                Lvl3(); break;
        }
    }

    protected abstract void Lvl1();
    protected abstract void Lvl2();
    protected abstract void Lvl3();
}
