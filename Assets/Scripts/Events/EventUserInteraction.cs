using System;
using Enums;
using UnityEngine.Events;

namespace Events
{
    [Serializable]
    public class EventUserInteraction : UnityEvent<UserInteraction>
    {
    }
}