using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Invaders
{
    public class InvadersManager : MonoBehaviour
    {
        #region PrivateFields

        private int _direction;
        private bool _movingX;
        private float _min = 0;
        private float _max = 5;
        private float _speed = 5;
        private float _previousY;
        private InvadersCount _invadersCount;
        private GameObject[] _col;

        #endregion

        #region SerializedFields

        [SerializeField] private Transform verticalLayoutGroup;
        [SerializeField] [Range(3, 10)] private int nbOfColumn = 3;
        [SerializeField] private Invader[] invaders;

        #endregion

        #region PublicMethods

        public void OnChildrenCollisionEnter()
        {
            if (!_movingX) return;
            _movingX = false;
            _previousY = transform.position.y;
        }

        public void MinusOneEnemy()
        {
            _invadersCount.MinusOneEnemy();
        }

        #endregion

        #region PrivateMethods

        private void Start()
        {
            _invadersCount = GetComponent<InvadersCount>();
            _direction = -1;
            _previousY = transform.position.y;
            _movingX = true;

            _col = new GameObject[nbOfColumn];
            for (var j = 0; j < nbOfColumn; j++)
            {
                _col[j] = Instantiate(verticalLayoutGroup.gameObject, transform);

                foreach (var t in invaders)
                {
                    var instantiate = Instantiate(t, _col[j].transform);
                    instantiate.invadersManager = this;
                }
            }

            Invoke(nameof(RemoveLayout), 0.5f);

            var rand = Random.Range(_min, _max);
            Invoke(nameof(SelectForFire), rand);
        }

        private void Update()
        {
            var levelOfAnger = _invadersCount.GetLevelOfAnger();
            switch (levelOfAnger)
            {
                case InvadersCount.LevelOfAnger.NotReallyGoodForYou:
                    _speed = 10;
                    break;
                case InvadersCount.LevelOfAnger.Rage:
                    _speed = 5;
                    break;
                case InvadersCount.LevelOfAnger.Mehh:
                    _speed = 4;
                    break;
                case InvadersCount.LevelOfAnger.Normal:
                    _speed = 3;
                    break;
                default:
                    _speed = 1;
                    break;
            }

            var t = transform;
            if (_movingX)
            {
                t.position += Vector3.right * (_direction * Time.deltaTime * _speed);
            }
            else
            {
                if (Math.Abs(transform.position.y - _previousY - (-1)) < 0.1 && !_movingX)
                {
                    _direction *= -1;
                    _movingX = true;
                }
                else
                {
                    t.position += Vector3.down * (Time.deltaTime * _speed);
                }
            }
        }

        //TODO Make it better
        private void SelectForFire()
        {
            var levelOfAnger = _invadersCount.GetLevelOfAnger();
            switch (levelOfAnger)
            {
                case InvadersCount.LevelOfAnger.NotReallyGoodForYou:
                {
                    _max = 1;
                    break;
                }
                case InvadersCount.LevelOfAnger.Rage:
                {
                    _max = 2;
                    break;
                }
                case InvadersCount.LevelOfAnger.Mehh:
                {
                    _max = 3;
                    break;
                }
                case InvadersCount.LevelOfAnger.Normal:
                {
                    _max = 4;
                    break;
                }
                default:
                {
                    _max = 5;
                    break;
                }
            }

            var rand = Random.Range(_min, _max);
            if (transform.childCount <= 0) return;
            var randomCol = (int) Math.Floor(Random.Range(_min, transform.childCount - 1));
            var column = transform.GetChild(randomCol);
            if (column.childCount > 0)
            {
                column.GetChild(column.childCount - 1).gameObject.GetComponent<Invader>()
                    .Invoke(nameof(Invader.Fire), 0f);
            }

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

        #endregion
    }
}