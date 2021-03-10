using UnityEngine;
using UnityEngine.UIElements;

namespace Invaders
{
    public class InvadersExplosion : MonoBehaviour
    {
        [SerializeField] private AudioClip source;

        private void Start()
        {
            var addComponent = GameManager.Instance.gameObject.AddComponent<AudioSource>();
            addComponent.clip = source;
            addComponent.Play();
            Destroy(addComponent, 1);
            Destroy(gameObject, 0.2f);
        }
    }
}