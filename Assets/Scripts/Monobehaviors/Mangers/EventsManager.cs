using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    internal GameObjectEvent PlayerKilled;
    private void Start()
    {
        PlayerKilled = new GameObjectEvent();
        PlayerKilled.AddListener(GameManager.Instance.OnPlyerKilled);
    }

}

[System.Serializable] public class VoidEvent : UnityEvent { }
[System.Serializable] public class GameObjectEvent : UnityEvent<Player> { }