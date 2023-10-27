using UnityEngine;
using UnityEngine.UI;

public class SliderHandVelociy : MonoBehaviour
{
    Slider _powerSlider;
    [SerializeField] Vector3 _vec;
    // Start is called before the first frame update
    void Start()
    {
        _powerSlider = GetComponent<Slider>();
        _powerSlider.minValue = 0;
        _powerSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        VectorSlider();
    }

    void VectorSlider()
    {
        float power = Mathf.Sqrt(Mathf.Pow(_vec.x, 2) + Mathf.Pow(_vec.z, 2));
        _powerSlider.value = power;
    }
}
