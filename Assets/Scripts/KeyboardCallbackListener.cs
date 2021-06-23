using System;
using UnityEngine;

namespace Fyber
{
    public class KeyboardCallbackListener : AndroidJavaProxy
    {
        public KeyboardCallbackListener() : base("com.fyber.fairbid.keyboardbannerhelper.KeyboardEvents")
        {
            UnityThread.initUnityThread();
        }

        public void onKeyboardShown(String bannerPlacementId)
        {
            UnityThread.executeInUpdate(() =>
            {
                Banner.Destroy(bannerPlacementId);
            });
        }
        public void onKeyboardHidden(String bannerPlacementId)
        {
            UnityThread.executeInUpdate(() =>
            {
                Banner.Show(bannerPlacementId);
            });
        }

    }
}

