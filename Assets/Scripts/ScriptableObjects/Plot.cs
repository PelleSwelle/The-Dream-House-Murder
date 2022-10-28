using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plot")]
public class Plot : ScriptableObject
{
    public Character murderer;
    public Character victim;
    public Weapon weapon;
    public TimeOfDay timeOfDay;
    public GeneralLocation location;
    public Motive motive;
    public Character[] witnesses;

    public void print()
    {
        Debug.Log("************");
        Debug.Log($"Murderer: {murderer}\nvictim: {victim}\nTime of day: {timeOfDay}\nweapon: {weapon}\nlocation: {location}\nmotive: {motive}");
        Debug.Log("************");
    }
}

public enum Weapon { huntingRifle, knife, pistol, club, hands, wire, baseballBat, car, lawnmower }
public enum GeneralLocation { woods, murderersHome, victimsHome, publicBathroom, alleyWay, summerhouse }
public enum preciseLocation { pHLocation1, phLocation2, phLocation3, phLocation4 }
public enum Motive { jalousy, accident, mentalDisturbance, hiredJob, enonomicGain, keepSecret, protectOther, moralReason }

public class TimeOfDay
{
    int hour, minutes, seconds;
}