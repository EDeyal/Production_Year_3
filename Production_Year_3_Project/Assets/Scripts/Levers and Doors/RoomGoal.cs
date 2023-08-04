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
    [SerializeField] bool isNotChecking = false;

    private void Awake()
    {
        CheckValidation();
    }
    private void Update()
    {
        if (isNotChecking)
        {
            return;
        }
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
                RoomsDoor.OpenDoor();
                isNotChecking = true;
                break;
            case Conditions.Key:
                foreach (var key in _keysToGet)
                {
                    if (key.IsCollected == false)
                    {
                        return;
                    }

                }            
                RoomsDoor.CanOpen = true;
                RoomsDoor.TurnOffPopUp(RoomsDoor.DoesntHaveKey);
                RoomsDoor.TurnOnPopUp(RoomsDoor.HaveKey);
                isNotChecking = true;

                break;
            case Conditions.Leaver:
                foreach (var leaver in _LeaversToActive)
                {
                    if (leaver.IsActivated == false)
                    {
                        return;
                    }
                }
                RoomsDoor.OpenDoor();
                isNotChecking = true;
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
            _enemiesToKill = goalEnemies;
        }
    }

    public void CheckValidation()
    {
        switch (activeConditions)
        {
            case Conditions.killAllEnemies:
                if (RoomsDoor == null)
                    throw new System.Exception($"RoomGoal {gameObject.name} Has no Door");
                break;
            case Conditions.Key:
                if (_keysToGet.Count == 0)
                    throw new System.Exception($"RoomGoal  {gameObject.name} Has no Keys");
                if (RoomsDoor == null)
                    throw new System.Exception($"RoomGoal {gameObject.name} Has no Door");
                if(RoomsDoor.keyLock == null)
                    throw new System.Exception($"RoomGoal{gameObject.name} has no keyLock");
                break;
            case Conditions.Leaver:
                if (_LeaversToActive.Count == 0)
                    throw new System.Exception($"RoomGoal {gameObject.name} Has no Leavers");
                if (RoomsDoor == null)
                    throw new System.Exception($"RoomGoal {gameObject.name} Has no Door");
                break;
            default:
                break;
        }


    }
}
