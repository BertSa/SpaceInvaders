using System;
using DesignPatterns;
using Enums;
using Events;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Invaders
{
    public class InvadersManager : Singleton<InvadersManager>
    {
        public EventTriggered triggered = new();

        private int _direction;
        private bool _movingX;
        private float _min = 0;
        private float _max = 5;
        private float _speed = 1;
        private float _previousY;
        private GameObject[] _col;

        private int NumberOfInvadersAtStart { get; set; }
        private int NumberOfInvadersAlive { get; set; }

        [SerializeField] private Transform verticalLayoutGroup;
        [SerializeField] private Invader squid;
        [SerializeField] private Invader crab;
        [SerializeField] private Invader octo;

        private void Start()
        {
            _direction = -1;
            _previousY = transform.position.y;
            _movingX = true;

            InitWave();

            Invoke(nameof(RemoveLayout), 0.5f);

            var rand = Random.Range(_min, _max);
            Invoke(nameof(SelectForFire), rand);
        }

        private void InitWave()
        {
            var wave = WaveManager.Instance.Current;

            NumberOfInvadersAtStart = wave.NbOfColumn * wave.Invaders.Length;
            NumberOfInvadersAlive = wave.NbOfColumn * wave.Invaders.Length;

            _col = new GameObject[wave.NbOfColumn];
            for (var i = 0; i < wave.NbOfColumn; i++)
            {
                _col[i] = Instantiate(verticalLayoutGroup.gameObject, transform);

                foreach (var type in wave.Invaders)
                {
                    var invader = type switch
                    {
                        InvaderTypes.Crab => crab,
                        InvaderTypes.Octopus => octo,
                        InvaderTypes.Squid => squid,
                        _ => throw new ArgumentOutOfRangeException(),
                    };

                    Instantiate(invader, _col[i].transform);
                }
            }
        }

        private void Update()
        {
            var levelOfAnger = GetLevelOfAnger();
            _speed = levelOfAnger switch
            {
                LevelOfAnger.NotReallyGoodForYou => 7,
                LevelOfAnger.Rage => 4,
                LevelOfAnger.Mehh => 3,
                LevelOfAnger.Normal => 2,
                _ => 1,
            };

            var t = transform;
            if (_movingX)
            {
                t.position += Vector3.right * (_direction * Time.deltaTime * _speed);
            }
            else if (Math.Abs(transform.position.y - _previousY - (-0.5)) < 0.1)
            {
                _direction *= -1;
                _movingX = true;
            }
            else
            {
                t.position += Vector3.down * (Time.deltaTime * _speed);
            }
        }

        public void OnChildrenCollisionEnter()
        {
            if (!_movingX)
            {
                return;
            }

            _movingX = false;
            _previousY = transform.position.y;
        }

        public void MinusOneEnemy()
        {
            NumberOfInvadersAlive--;
            if (NumberOfInvadersAlive == 0)
            {
                triggered?.Invoke();
            }
        }

        private void SelectForFire()
        {
            var levelOfAnger = GetLevelOfAnger();
            _max = levelOfAnger switch
            {
                LevelOfAnger.NotReallyGoodForYou => 1,
                LevelOfAnger.Rage => 2,
                LevelOfAnger.Mehh => 3,
                LevelOfAnger.Normal => 4,
                _ => 5,
            };

            if (transform.childCount <= 0)
            {
                return;
            }

            var randomCol = (int)Math.Floor(Random.Range(_min, transform.childCount - 1));

            var column = transform.GetChild(randomCol);
            if (column.childCount > 0)
            {
                var child = column.GetChild(column.childCount - 1);
                var invader = child.gameObject.GetComponent<Invader>();
                invader.Invoke(nameof(Invader.Fire), 0f);
            }

            var rand = Random.Range(_min, _max);
            Invoke(nameof(SelectForFire), rand);
        }

        private void RemoveLayout()
        {
            foreach (var t in _col)
            {
                var layoutGroup = t.GetComponent<VerticalLayoutGroup>();
                layoutGroup.enabled = false;
            }
        }

        public LevelOfAnger GetLevelOfAnger()
        {
            var instanceEnemiesAtStart = (double)NumberOfInvadersAtStart / 100;

            if (NumberOfInvadersAlive < instanceEnemiesAtStart * 10)
                return LevelOfAnger.NotReallyGoodForYou;
            if (NumberOfInvadersAlive < instanceEnemiesAtStart * 25)
                return LevelOfAnger.Rage;
            if (NumberOfInvadersAlive < instanceEnemiesAtStart * 50)
                return LevelOfAnger.Mehh;
            if (NumberOfInvadersAlive < instanceEnemiesAtStart * 75)
                return LevelOfAnger.Normal;

            return LevelOfAnger.Fun;
        }
    }
}