using UnityEngine;

namespace Level
{
    public class BottomLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameOver.IsInitialized) return;
            if (other.gameObject.CompareTag($"Invaders"))
            {
                GameOver.Instance.SetOverWithWinner(GameOver.WlState.Lost);
            }
        }
    }
}