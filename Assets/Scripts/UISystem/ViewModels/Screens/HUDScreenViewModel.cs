using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.UI.ViewModels.Screens
{
    public class HUDScreenViewModel : ViewModel
    {
        private int m_depth;
        public int Depth
        {
            get => m_depth;
            set
            {
                ChangePropertyAndNotify(ref m_depth, value);
            }
        }


        public CharacterStats_SO m_playerData;
        public HUDScreenViewModel(CharacterStats_SO playerData)
        {
            m_playerData = playerData;
            EventSystem.Instance.Subscribe<DepthEvent>(typeof(DepthEvent), onDepthChanged);
        }
        void onDepthChanged(DepthEvent e)
        {
            Depth = (int)e.Depth;
        }
    }
}

