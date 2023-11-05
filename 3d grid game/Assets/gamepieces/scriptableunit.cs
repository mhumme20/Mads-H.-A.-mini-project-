using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
[CreateAssetMenu(fileName= "New unit", menuName = "Scriptable Unit")]
public class scriptableunit : ScriptableObject
{
    public Faction Faction;
    public baseUnit unitPrefab;


}

public enum Faction
{
    Hero= 0,
    Enemy= 1
}