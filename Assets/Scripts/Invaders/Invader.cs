using UnityEngine;

public class Invader : MonoBehaviour
{
    private const float CoolDownPeriodInSeconds = 1;
    private float _timeStamp;
    [SerializeField] public GameObject bullet;
    public WaveController waveController;
    public InvaderTypes type;
    public Sprite[] walkStateSprites;
    private int _currentSpriteIndex;
    private SpriteRenderer _spriteRender;


    public void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        Invoke(nameof(PlayAnimation), 0.5f);
    }

    public void Fire()
    {
        var position = transform.position;
        var x = position.x;
        var y = position.y - 1;

        if (_timeStamp <= Time.time)
        {
            _timeStamp = Time.time + CoolDownPeriodInSeconds;
            Instantiate(bullet, new Vector3(x, y, 5), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag($"Wall")) return;
        waveController.OnChildrenCollisionEnter();
    }

    private void OnDestroy()
    {
        if (!LevelControler.IsInitialized) return;
        LevelControler.Instance.EnemyCount--;
    }

    public void PlayAnimation()
    {
        ChangeWalkState();

        Invoke(nameof(PlayAnimation), 0.5f);
    }

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
}