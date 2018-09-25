using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides the references to the unity components for SingleComponentInstanceLocator
/// </summary>
public class GameContext : MonoBehaviour
{
    public static GameContext instance;
    private Dictionary<Type, object> componentDendencies;

    private void Awake()
    {
        instance = this;
        componentDendencies = new Dictionary<Type, object>();
    }

    /// <summary>
    /// MAY NOT BE CALLED IN AWAKE TO ENSURE EVERY OBJECT IS SETUP AND READY TO USE
    /// </summary>
    /// <typeparam name="T">Type of single instance component wanted</typeparam>
    /// <returns></returns>
    public T GetDependency<T>()
    {
        if (!componentDendencies.ContainsKey(typeof(T)))
        {
            var comp = GetComponentInChildren<T>(true);
            componentDendencies.Add(typeof(T), comp);
        }

        return (T) componentDendencies[typeof(T)];
    }

    internal void ReloadScene()
    {
        instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}