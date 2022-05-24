using UnityEngine;

namespace Invaders
{
    public class Invader : MonoBehaviour
    {
        private const float CoolDownPeriodInSeconds = 1;

        private readonly object _syncLock = new Object();

        [SerializeField] private InvadersExplosion invadersExplosion;
        [SerializeField] private GameObject bullet;
        [SerializeField] private InvaderTypes type;
        [SerializeField] private Sprite[] walkStateSprites;
        [SerializeField] public InvadersManager invadersManager;

        private float TimeStamp { get; set; }
        private SpriteRenderer SpriteRender { get; set; }
        private int CurrentSpriteIndex { get; set; }


        public void Fire()
        {
            if (TimeStamp > Time.time)
            {
                return;
            }

            var position = transform.position;
            var x = position.x;
            var y = position.y - 1;

            TimeStamp = Time.time + CoolDownPeriodInSeconds;
            Instantiate(bullet, new Vector3(x, y, 5), Quaternion.identity);
        }

        public void Kill()
        {
            lock (_syncLock)
            {
                if (ScoreManager.IsInitialized)
                {
                    ScoreManager.Instance.AddPointsPerTypes(type);
                }

                var position = transform.position;
                Instantiate(invadersExplosion, new Vector3(position.x, position.y), Quaternion.identity);
                invadersManager.MinusOneEnemy();
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            SpriteRender = GetComponent<SpriteRenderer>();
            Invoke(nameof(PlayAnimation), 0.5f);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                invadersManager.OnChildrenCollisionEnter();
            }
        }

        private void PlayAnimation()
        {
            ChangeWalkState();

            var levelOfAnger = invadersManager.GetComponent<InvadersCount>().GetLevelOfAnger();
            var speed = levelOfAnger switch
            {
                LevelOfAnger.NotReallyGoodForYou => 0.1f,
                LevelOfAnger.Rage => 0.2f,
                LevelOfAnger.Mehh => 0.3f,
                LevelOfAnger.Normal => 0.4f,
                _ => 0.5f,
            };

            Invoke(nameof(PlayAnimation), speed);
        }

        private void ChangeWalkState()
        {
            CurrentSpriteIndex++;
            if (CurrentSpriteIndex >= walkStateSprites.Length) CurrentSpriteIndex = 0;

            SpriteRender.sprite = (walkStateSprites[CurrentSpriteIndex]);
        }
    }

    public enum InvaderTypes
    {
        Octopus,
        Crab,
        Squid,
    }
}