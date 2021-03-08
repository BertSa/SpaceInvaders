using UnityEngine;

public class BottomLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameOver.IsInitialized)
        {
            if (other.gameObject.CompareTag($"Invaders"))
            {
                GameOver.Instance.SetOver(GameOver.WlState.Lost);
            }
        }
    }
}