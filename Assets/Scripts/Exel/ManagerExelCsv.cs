using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerExelCsv : MonoBehaviour
{
    public TextAsset csvFile; 

    private char endLine = '\n'; 
    private char endChar = ';'; 

    void Start()
    {
        readCsv();
    }
    // Read data from CSV file
    private void readCsv()
    {
        string[] doc = csvFile.text.Split(endLine);
        foreach (string line in doc)
        {
            string[] Caracts = line.Split(endChar);
            foreach (string Carac in Caracts)
            {
//                Debug.Log(Carac);
            }
        }
    }
}
