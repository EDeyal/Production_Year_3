using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGoal : MonoBehaviour
{
    [SerializeField] Door RoomsDoor;
    [SerializeField] public Conditions activeConditions;
    [SerializeField] bool isInRoom = false;
    [SerializeField] bool isOpened = false;

    private void Awake()
    {
        //init Roomgoal
    }
    private void Update()
    {
        switch (activeConditions)
        {
            case Conditions.killAllEnemies
               //if all enemies are killed
               //Open Door Or animate door
               :
                break;
            case Conditions.Key
               //if a key was collected
               //Open Door Or animate door
                :
                break;
            case Conditions.Leaver
               //if a Leaver was Activated
               //Open Door Or animate door
                :
                break;
        }
    }
    public enum Conditions
    {
        killAllEnemies,
        Key,
        Leaver
    }
}
