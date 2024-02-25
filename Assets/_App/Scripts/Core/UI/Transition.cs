using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] 
    private GameObject _meny;
    [SerializeField]
    private Image _globalMask;

    private const float TransitionDuration = 1.0f;

    private static Animator Animator;

    // TODO remove public static event.
    public static System.Action OnTransitionStart;
    public static System.Action OnTransitionEnd;

    private static bool _startSessionTransition = false;

    public static void Action()
    {
        if (Animator == null)
            return;

        Animator.Play("Action");
        _startSessionTransition = !_startSessionTransition;
    }

    public async Task Action_()
    {
        float currentTime = 0;
        Color startColor = _globalMask.color;
        Color endColor = startColor.a == 0 ? Color.white : Color.clear;

        while (currentTime < TransitionDuration)
        {
            currentTime += Time.deltaTime;
            _globalMask.color = Color.Lerp(startColor, endColor, currentTime / TransitionDuration);
            await Task.Yield();
        }
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
}
