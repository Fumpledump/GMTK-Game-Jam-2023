using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Handler : MonoBehaviour
{
    public enum Position {
        Left,
        Right,
        Middle
    }

    Dictionary<Position, GameObject> slots = new Dictionary<Position, GameObject>();


    void Start()
    {
                
    }


    void Update()
    {
        
    }
}
