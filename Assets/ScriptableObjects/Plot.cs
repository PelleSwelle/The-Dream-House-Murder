using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plot")]
public class Plot : ScriptableObject
{
    public Character murderer;
    public Weapon weapon;
    public Location location;
    public Motive motive;
}
