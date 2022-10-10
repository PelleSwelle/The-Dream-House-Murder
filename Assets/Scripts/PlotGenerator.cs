using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGenerator
{
    Character murderer;
    Plot plot;

    /// <summary>
    /// generate a plot with random variables assigned
    /// </summary>
    /// <returns></returns>
    public void generatePlot()
    {
        Weapon weapon = (Weapon)Random.Range(0, System.Enum.GetValues(typeof(Weapon)).Length);
        Location location = (Location)Random.Range(0, System.Enum.GetValues(typeof(Location)).Length);
        Motive motive = (Motive)Random.Range(0, System.Enum.GetValues(typeof(Motive)).Length);
        TimeOfDay timeOfDay = new TimeOfDay();

        ScriptableObject.Instantiate(plot, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

public enum Weapon { huntingRifle, knife, pistol, club, hands, wire, baseballBat, car }
public enum Location { woods, murderersHome, victimsHome, publicBathroom, alleyWay }
public enum Motive { jalousy, accident, mentalDisturbance, hiredJob, enonomicGain, keepSecret, protectOther, moralReason }

/// <summary>
/// Class for handling a time of the day
/// </summary>
class TimeOfDay
{
    int hour, minute, second;
    public TimeOfDay()
    {
        this.hour = Random.Range(0, 25);
        this.minute = Random.Range(0, 61);
        this.second = Random.Range(0, 61);
    }
}