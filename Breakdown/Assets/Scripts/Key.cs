using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameKey", menuName = "Key", order = 1)]
public class Key : ScriptableObject
{
   public float timeStamp;
   public bool FinalKey = false;
   public float TimeToPress = 0.5f;
   public KeyColors colorToPress;
   public bool KeyWasPressedP1, KeyWasPressedP2;

}