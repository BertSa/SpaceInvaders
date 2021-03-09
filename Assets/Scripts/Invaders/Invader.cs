using UnityEngine;

namespace Invaders
{
    public class Invader : MonoBehaviour
    {
        private const float CoolDownPeriodInSeconds = 1;
        private float _timeStamp;
        public InvadersExplosion invadersExplosion;
        [SerializeField] public GameObject bullet;
        public InvadersManager invadersManager;
        public InvaderTypes type;
        public Sprite[] walkStateSprites;
        private int _currentSpriteIndex;
        private SpriteRenderer _spriteRender;
        private readonly object _syncLock = new Object();


        public void Start()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            Invoke(nameof(PlayAnimation), 0.5f);
        }

        public void Fire()
        {
            if (!(_timeStamp <= Time.time)) return;
            var position = transform.position;
            var x = position.x;
            var y = position.y - 1;

            _timeStamp = Time.time + CoolDownPeriodInSeconds;
            Instantiate(bullet, new Vector3(x, y, 5), Quaternion.identity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag($"Wall")) return;
            invadersManager.OnChildrenCollisionEnter();
        }

        public void PlayAnimation()
        {
            ChangeWalkState();

            Invoke(nameof(PlayAnimation), 0.5f);
        }

//TODO speed of WalkAnimation
        private void ChangeWalkState()
        {
            _currentSpriteIndex++;
            if (_currentSpriteIndex >= walkStateSprites.Length) _currentSpriteIndex = 0;

            _spriteRender.sprite = (walkStateSprites[_currentSpriteIndex]);
        }

        public enum InvaderTypes
        {
            Octopus,
            Crab,
            Squid
        }

        public void Kill()
        {
            if (!ScoreManager.IsInitialized) return;
            lock (_syncLock)
            {
                ScoreManager.Instance.AddPointsPerTypes(type);
                var position = transform.position;
                Instantiate(invadersExplosion, new Vector3(position.x, position.y),
                    Quaternion.identity);
                invadersManager.MinusOneEnemy();
            }

            Destroy(gameObject);
        }
    }
}