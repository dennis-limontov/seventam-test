using TMPro;
using UnityEngine;

namespace SevenTam
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _resultText;

        #region Lose settings
        [SerializeField]
        private string _loseGameText = "You lost!";

        [SerializeField]
        private Color _loseTextColor = Color.red;
        #endregion

        #region Win settings
        [SerializeField]
        private string _winGameText = "You won!";

        [SerializeField]
        private Color _winTextColor = Color.green;
        #endregion

        private void OnDestroy()
        {
            EventBus.OnGameEnded -= GameEndedHandler;
        }

        private void Start()
        {
            EventBus.OnGameEnded += GameEndedHandler;
            gameObject.SetActive(false);
        }

        private void GameEndedHandler(bool isWin)
        {
            gameObject.SetActive(true);
            if (isWin)
            {
                _resultText.text = _winGameText;
                _resultText.color = _winTextColor;
            }
            else
            {
                _resultText.text = _loseGameText;
                _resultText.color = _loseTextColor;
            }
        }

        public void OnRestartClicked()
        {
            /*gameObject.SetActive(false);
            EventBus.OnRestartClicked?.Invoke();*/
        }
    }
}