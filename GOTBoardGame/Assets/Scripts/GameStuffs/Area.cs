using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum AreaType
{
    Land,
    Sea,
}
public enum AreaProperty
{
    SupplyIcon,
    PowerIcon
}
public enum MusterLevel
{
    None,
    StrongHold,
    Castle,
    PortStrongHold,
    PortCastle
}
public class Area : MonoBehaviour {
    [Header("Adjacent areas")]
    [Tooltip("Drag area objects from the scene & Drop them here")]
    [SerializeField] private List<Area> neighboors;
    [Header("Property Settings")]
    [SerializeField] private AreaType areaType = AreaType.Land;
    [SerializeField] private MusterLevel MusterLevel = MusterLevel.None;
    [Tooltip("Example: 2 supplyIcon & 1 powerIcon; change the size to 3 and set drop-downs to those icons")]
    [SerializeField] private AreaProperty[] icons;
    public int powerPoints
    {
        get
        {
            switch (MusterLevel)
            {
                case MusterLevel.None:
                    return 0;
                case MusterLevel.Castle:
                    return 1;
                case MusterLevel.PortCastle:
                    return 1;
                case MusterLevel.StrongHold:
                    return 2;
                case MusterLevel.PortStrongHold:
                    return 2;
            }
            return -1;
        }
    }
    public bool canProduceShip
    {
        get
        {
            if(MusterLevel == MusterLevel.PortCastle || MusterLevel == MusterLevel.PortStrongHold)
            {
                return true;
            }
            return false;
        }
    }
    public int numSupplyIcons
    {
        get{return countProperty(AreaProperty.SupplyIcon);}
    }
    public int numPowerIcons
    {
        get{return countProperty(AreaProperty.PowerIcon);}
    }

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
        get{return _dominator;}
        private set { _dominator = value; }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < neighboors.Count; i++)
        {
            if (neighboors[i] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, neighboors[i].transform.position);
            }
        }
        Handles.Label(transform.position, gameObject.name);
    }

    void Start()
    {
        for (int i = 0; i < neighboors.Count; i++)
        {
            if (neighboors[i] != null)
            {
                if (!neighboors[i].neighboors.Contains(this))
                {
                    neighboors.Add(this);
                }
            }
        }
    }
    int countProperty(AreaProperty property)
    {
        int ret = 0;
        for (int i = 0; i < icons.Length; i++)
        {
            if (icons[i] == property)
            {
                ret++;
            }
        }
        return ret;
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
