using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Text scoreValue;
        [SerializeField] private Text livesValue;


        private void Start()
        {
            ScoreManager.Instance.ValuesForHud.AddListener(UpdateScoreValue);
            LifeManager.Instance.ValuesForHud.AddListener(UpdateLivesValue);
        }

        private void UpdateLivesValue(int value)
        {
            livesValue.text = value.ToString();
        }

        private void UpdateScoreValue(int value)
        {
            scoreValue.text = value.ToString();
        }
    }
}