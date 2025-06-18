using System;
using System.Collections;
using UnityEngine;

namespace ExpiProject.Others
{
    public class Transition : MonoBehaviour
    {
        public static Transition instance;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            instance = this;
        }

        public void FadeIn(Action callback = null)
        {
            StartCoroutine(FadeInCoroutine(callback));
        }

        public void FadeOut(Action callback = null)
        {
            StartCoroutine(FadeOutCoroutine(callback));
        }

        private IEnumerator FadeInCoroutine(Action callback = null)
        {
            animator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            callback?.Invoke();
        }

        private IEnumerator FadeOutCoroutine(Action callback = null)
        {
            animator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            callback?.Invoke();
        }
    }
}