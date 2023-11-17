using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiskCore.EventSender;
using System;
using UnityEngine.Networking;

namespace MiskCore.EventSender.Network
{
    public static class NetworkEventExtension
    {
        public static BaseEventElement Request(string url, Action<UnityWebRequest> getfunc)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            Func<IEnumerator> func = () =>
            {
                IEnumerator Sequence()
                {
                    yield return request.SendWebRequest();
                }
                return Sequence();
            };

            return new EventIteratorElement(new BaseEventElement[] {
                new CoroutineElement(func),
                new CallBackEventElement(() => getfunc(request))
            });
        }
    }

}
