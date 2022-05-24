using System;
using Enums;
using UnityEngine.Events;

namespace Events
{
    [Serializable]
    public class EventGameState : UnityEvent<GameState, GameState>
    {
    }
}