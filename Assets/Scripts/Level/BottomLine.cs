using DesignPatterns;
using Events;
using UnityEngine;

namespace Level
{
    public class BottomLine : Singleton<BottomLine>
    {
        public EventTriggered triggered;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Invaders"))
            {
                triggered.Invoke();
            }
        }
    }
}