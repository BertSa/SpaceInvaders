using UnityEngine;

namespace Invaders
{
    public class InvadersExplosion : MonoBehaviour
    {
        private void Start()
        {
            Invoke(nameof(KillMe), 0.1f);
        }

        private void KillMe()
        {
            Destroy(gameObject);
        }
    }
}