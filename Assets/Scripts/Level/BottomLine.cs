using UnityEngine;

namespace Level
{
    public class BottomLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Invaders"))
            {
                return;
            }

            if (GameManager.IsInitialized)
            {
                GameManager.Instance.UpdateGameState(GameState.Lost);
            }
        }
    }
}