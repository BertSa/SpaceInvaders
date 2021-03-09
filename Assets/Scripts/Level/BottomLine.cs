using UnityEngine;

namespace Level
{
    public class BottomLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameOver.IsInitialized || !other.gameObject.CompareTag($"Invaders")) return;

            GameOver.Instance.SetOverWithWinner(GameOver.WlState.Lost);
        }
    }
}