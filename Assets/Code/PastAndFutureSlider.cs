using UnityEngine;
using UnityEngine.InputSystem;

namespace Manager
{
    public class PastAndFutureSlider : MonoBehaviour
    {
        [SerializeField] private Material reduceWidth;
        [SerializeField] private Vector2 limit;
        [SerializeField] private float sliderSpeed;
        float sliderVal;

        private void Start()
        {
            sliderVal = reduceWidth.GetFloat("_CutoffValue");
            reduceWidth.SetFloat("_CutoffValue", limit.x);
        }

        private void OnDisable()
        {
            reduceWidth.SetFloat("_CutoffValue", limit.x);
        }

        public void IncreaseWidth(InputAction.CallbackContext context)
        {
            if (sliderVal < limit.y)
            {
                sliderVal += sliderSpeed;
                reduceWidth.SetFloat("_CutoffValue", sliderVal);
            }

        }

        public void DecreaseWidth(InputAction.CallbackContext context)
        {
            if (sliderVal > limit.x)
            {
                sliderVal -= sliderSpeed;
                reduceWidth.SetFloat("_CutoffValue", sliderVal);
            }
        }
    }
}