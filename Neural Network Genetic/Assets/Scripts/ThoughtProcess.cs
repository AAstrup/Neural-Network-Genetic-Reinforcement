using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class ThoughtProcess
{
    [HideInInspector]
    public float[] biasAndWeights;
    [SerializeField]
    private string m_ThoughtAsString;
    private int index;

    /// <summary>
    /// USED ONLY BY SERIALIZER
    /// </summary>
    public ThoughtProcess()
    {
    }

    public ThoughtProcess(int size, float range)
    {
        biasAndWeights = new float[size];
        for (int i = 0; i < biasAndWeights.Length; i++)
        {
            biasAndWeights[i] = UnityEngine.Random.Range(-range, range);
        }
    }

    public ThoughtProcess(ThoughtProcess firstParent, ThoughtProcess secondParent)
    {
        biasAndWeights = new float[firstParent.biasAndWeights.Length];
        List<int> indexes = new List<int>();
        for (int i = 0; i < firstParent.biasAndWeights.Length; i++)
        {
            indexes.Add(i);
        }
        for (int i = 0; i < firstParent.biasAndWeights.Length / 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, indexes.Count);
            var indexToUse = indexes[randomIndex];
            indexes.RemoveAt(randomIndex);
            biasAndWeights[indexToUse] = firstParent.biasAndWeights[indexToUse];
        }
        foreach (var index in indexes)
        {
            biasAndWeights[index] = secondParent.biasAndWeights[index];
        }
    }

    public void CompleteInspectorInjected()
    {
        string[] stringSplit = m_ThoughtAsString.Split(',');
        biasAndWeights = new float[stringSplit.Length];
        for (int i = 0; i < biasAndWeights.Length; i++)
        {
            biasAndWeights[i] = float.Parse(stringSplit[i].Trim());
        }
    }

    public float GetNext()
    {
        return biasAndWeights[index++];
    }

    public int GetCurrentIndex()
    {
        return index;
    }

    internal string GetThoughtAsString()
    {
        if (m_ThoughtAsString == null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < biasAndWeights.Length; i++)
            {
                stringBuilder.Append(biasAndWeights[i]);
                if(i != biasAndWeights.Length - 1)
                    stringBuilder.Append(", ");
            }
            m_ThoughtAsString = stringBuilder.ToString();
        }
        return m_ThoughtAsString;
    }

    internal void ResetIndex()
    {
        index = 0;
    }
}