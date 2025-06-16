using TMPro;
using UnityEngine;

namespace SevenTam
{
    public class DefrostingPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _defrostingValueText;

        private void Awake()
        {
            EventBus.OnDefrostingCounterChanged += DefrostingCounterChangedHandler;
        }

        private void OnDestroy()
        {
            EventBus.OnDefrostingCounterChanged -= DefrostingCounterChangedHandler;
        }

        private void DefrostingCounterChangedHandler(int counter)
        {
            _defrostingValueText.text = counter.ToString();
        }
    }
}