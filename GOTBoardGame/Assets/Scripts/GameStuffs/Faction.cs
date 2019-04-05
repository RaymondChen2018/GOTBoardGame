using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionHouse
{
    Arryn,
    Baratheon,
    Greyjoy,
    Lannister,
    Martell,
    Stark,
    Targaryen,
    Tyrell,
    None
}
public abstract class Faction : MonoBehaviour {
    int _powerTokens = 5;
    public int powerTokens
    {
        get{return _powerTokens;}
        private set{_powerTokens = value;}
    }
    int _supplyBarrels = 0;
    public int supplyBarrels
    {
        get { return _supplyBarrels; }
        private set { _supplyBarrels = value; }
    }
    public int availableMaxSupply
    {
        get { return -1; }
    }

    [SerializeField] private int garrisonStrength = 2;
    [SerializeField] private Area garrisonArea;

    protected abstract FactionHouse type {
        get;
    }

    public void usePowerTokens(int count)
    {
        powerTokens -= count;
    }
    public void awardPowerTokens(int count)
    {
        powerTokens += count;
    }
}
