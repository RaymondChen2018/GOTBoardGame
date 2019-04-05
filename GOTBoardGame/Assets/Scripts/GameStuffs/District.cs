using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour {
    int _unitActive = 0;
    public int unitActive{
        get
        {
            return _unitActive;
        }
        private set { _unitActive = value; }
    }
    Faction _dominator = null;
    public Faction dominator
    {
        get
        {
            return _dominator;
        }
        private set { _dominator = value; }
    }
    public void invade(Faction faction, int numberUnits)
    {
        unitActive = numberUnits;
        dominator = faction;
    }
    public void moveIn(int numberUnits)
    {
        unitActive += numberUnits;
    }
    public void moveOut(int numberUnits)
    {
        unitActive -= numberUnits;
        if(unitActive == 0)
        {
            if(dominator.powerTokens > 0)
            {
                dominator.usePowerTokens(1);
            }
            else
            {
                dominator = null;
            }
        }
    }
}
