using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimationCallBackTest : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public static event UnityAction OnAnimationWalk;
    public static event UnityAction OnAnimationSit;
    public static event UnityAction OnAnimationStay;
    public static event UnityAction OnAnimationDance;
    public static event UnityAction OnAnimationSuccess;
    public static event UnityAction OnAnimationFailed;
    public static event UnityAction OnAnimationIdle;
    public static event UnityAction OnAnimationAttack;
    void Start()
    {
        buttons[0].onClick.AddListener(() => OnAnimationWalk.Invoke());
        buttons[1].onClick.AddListener(() => OnAnimationSit.Invoke());
        buttons[2].onClick.AddListener(() => OnAnimationDance.Invoke());
        buttons[3].onClick.AddListener(() => OnAnimationSuccess.Invoke());
        buttons[4].onClick.AddListener(() => OnAnimationFailed.Invoke());
        buttons[6].onClick.AddListener(() => OnAnimationStay.Invoke());
        buttons[7].onClick.AddListener(() => OnAnimationIdle.Invoke());
        buttons[8].onClick.AddListener(() => OnAnimationAttack.Invoke());
    }
    private void OnDestroy()
    {
        OnAnimationWalk = null;
        OnAnimationSit = null;
        OnAnimationStay = null;
        OnAnimationDance = null;
        OnAnimationSuccess = null;
        OnAnimationFailed = null;
        OnAnimationIdle = null;
        OnAnimationAttack = null;
    }
}
