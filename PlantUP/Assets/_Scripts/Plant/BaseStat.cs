using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour {

    private float baseValue;
    private float currentValue;

    /// <summary>
    /// Konstruktor des Stat.
    /// </summary>
    /// <param name="baseValue">Fester Basiswert des Stat</param>
    /// <param name="currentValue">Momentaner Wert des Stat</param>
    public BaseStat(float baseValue, float currentValue) {
        this.baseValue = baseValue;
        this.currentValue = currentValue;
    }
    /// <summary>
    /// Getter für den momentanen Wert des Stat.
    /// </summary>
    /// <returns>Momentaner Wert</returns>
    public float getCurrentValue() {
        return currentValue;
    }
    /// <summary>
    /// Getter für den Basiswert des Stat.
    /// </summary>
    /// <returns>Basiswert</returns>
    public float getBaseValue() {
        return baseValue;
    }
    /// <summary>
    /// Setter für den momentanen Wert des Stat.
    /// </summary>
    /// <param name="value">Neuer Wert für den momentanen Wert</param>
    public void setCurrentValue(float value) {
        currentValue = value;
    }
}
