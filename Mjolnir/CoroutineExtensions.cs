using System;
using System.Collections;
using UnityEngine;

namespace Mjolnir
{
    internal static class CoroutineExtensions
    {
        public static void DelayedMethod(float seconds, Action method)
        {
            Mjolnir.Instance.StartCoroutine(InternalDelayedMethod(seconds, method));
        }

        private static IEnumerator InternalDelayedMethod(float seconds, Action method)
        {
            yield return new WaitForSeconds(seconds);
            method();
        }

        public static IEnumerator CoroutineWait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}
