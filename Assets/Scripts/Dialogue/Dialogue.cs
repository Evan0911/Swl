using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //Modifie la zone d'entrer de la donn�e
    [TextArea(3,10)]
    public string[] sentences;
}
