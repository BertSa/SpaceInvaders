using UnityEngine;

namespace Invaders
{
    public class Invader : MonoBehaviour
    {
        #region ReadOnlyAndConst

        private const float CoolDownPeriodInSeconds = 1;
        private readonly object _syncLock = new Object();

        #endregion

        #region SerializedFields

        [SerializeField] private InvadersExplosion invadersExplosion;
        [SerializeField] private GameObject bullet;
        [SerializeField] private InvaderTypes type;
        [SerializeField] private Sprite[] walkStateSprites;
        [SerializeField] public InvadersManager invadersManager;

        #endregion

        #region PrivateFields

        private float _timeStamp;
        private SpriteRenderer _spriteRender;
        private int _currentSpriteIndex;

        #endregion


        #region PublicMethods

        public void Fire()
        {
            if (!(_timeStamp <= Time.time)) return;
            var position = transform.position;
            var x = position.x;
            var y = position.y - 1;

            _timeStamp = Time.time + CoolDownPeriodInSeconds;
            Instantiate(bullet, new Vector3(x, y, 5), Quaternion.identity);
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

        #endregion

        #region PrivateMethodes

        private void Start()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            Invoke(nameof(PlayAnimation), 0.5f);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag($"Wall")) return;
            invadersManager.OnChildrenCollisionEnter();
        }

        private void PlayAnimation()
        {
            ChangeWalkState();

            var levelOfAnger = invadersManager.GetComponent<InvadersCount>().GetLevelOfAnger();
            float val;
            switch (levelOfAnger)
            {
                case InvadersCount.LevelOfAnger.NotReallyGoodForYou:
                {
                    val = 0.1f;
                    break;
                }
                case InvadersCount.LevelOfAnger.Rage:
                {
                    val = 0.2f;
                    break;
                }
                case InvadersCount.LevelOfAnger.Mehh:
                {
                    val = 0.3f;
                    break;
                }
                case InvadersCount.LevelOfAnger.Normal:
                {
                    val = 0.4f;
                    break;
                }
                default:
                {
                    val = 0.5f;
                    break;
                }
            }

            Invoke(nameof(PlayAnimation), val);
        }

        private void ChangeWalkState()
        {
            _currentSpriteIndex++;
            if (_currentSpriteIndex >= walkStateSprites.Length) _currentSpriteIndex = 0;

            _spriteRender.sprite = (walkStateSprites[_currentSpriteIndex]);
        }

        #endregion

        #region Enums

        public enum InvaderTypes
        {
            Octopus,
            Crab,
            Squid
        }

        #endregion
    }
}