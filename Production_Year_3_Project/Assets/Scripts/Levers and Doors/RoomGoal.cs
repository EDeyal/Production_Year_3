using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGoal : MonoBehaviour, ICheckValidation
{
    [SerializeField] Door RoomsDoor;
    [SerializeField] public Conditions activeConditions;
    List<BaseEnemy> _enemiesToKill;
    [SerializeField] List<Key> _keysToGet;
    [SerializeField] List<Leaver> _LeaversToActive;
    [SerializeField] bool isInRoom = false;
    [SerializeField] bool isOpened = false;

    private void Awake()
    {
        CheckValidation();
        //init Roomgoal
    }
    private void Update()
    {
        switch (activeConditions)
        {
            case Conditions.killAllEnemies:
                foreach (var enemy in _enemiesToKill)
                {
                    if (enemy.gameObject.activeInHierarchy)
                    {
                        return;
                    }
                }
                //if all enemies are killed
                //Open Door Or animate door              
                break;
            case Conditions.Key:
                foreach (var key in _keysToGet)
                {
                    if (key.IsCollected == false)
                    {
                        return;
                    }
                }
                //if a key was collected
                //Open Door Or animate door

                break;
            case Conditions.Leaver:
                foreach (var leaver in _LeaversToActive)
                {
                    if (leaver.IsActivated == false)
                    {
                        return;
                    }
                }
                //if a Leaver was Activated
                //Open Door Or animate door

                break;
        }
    }
    public enum Conditions
    {
        killAllEnemies,
        Key,
        Leaver
    }
    public void SetRoomEnemies(List<BaseEnemy> goalEnemies)
    {
        if (activeConditions == Conditions.killAllEnemies)
        {

        }
    }

    public void CheckValidation()
    {
        if (_keysToGet.Count == 0)
            throw new System.Exception($"RoomGoal  {gameObject.name} Has no Keys");
        
        if (_LeaversToActive.Count == 0)     
            throw new System.Exception($"RoomGoal {gameObject.name} Has no Leavers");
        
        if (RoomsDoor == null)
            throw new System.Exception($"RoomGoal {gameObject.name} Has no Door");
    }
}
