using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.UI.ViewModels.Screens
{
    public class LoadingViewModel : ViewModel
    {
        private float m_loadingProgress;
        public Action OnLoadingComplete;
        public float LoadingProgress
        {
            get => m_loadingProgress;
            set
            {
                ChangePropertyAndNotify(ref m_loadingProgress, value);
            }
        }
        public float MinLoadingTime = 0;
        public string LoadingTips { get; private set; }
        public LoadingViewModel(float minLoadingTime,string loadingTips)
        {
            MinLoadingTime = minLoadingTime;
            LoadingTips = loadingTips;
        }
    
        public void LoadingComplete()
        {
            //ScreenManager.Instance.TransitionTo(EScreenType.MainMenuScreen, ELayerType.DefaultLayer, new MainMenuViewModel()).Forget();
            OnLoadingComplete?.Invoke();
        }
    }
}
