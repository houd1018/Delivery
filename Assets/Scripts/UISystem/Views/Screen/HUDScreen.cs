using Isekai.UI.Models;
using Isekai.UI.ViewModels.Screens;
using Isekai.UI.Views.Widgets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Isekai.UI.Views.Screens
{
    public class HUDScreen : Screen<HUDScreenViewModel>
    {
        [SerializeField]
        private Transform m_maxHealth;
        [SerializeField]
        private Transform m_curHealth;
        [SerializeField]
        private Transform m_teleportTime;
        [SerializeField]
        private TextMeshProUGUI m_depth;
        public override void OnEnterScreen()
        {
            
        }

        public override void OnExitScreen()
        {
            
        }
        private void Update()
        {
            refreshHealth();
        }
        void refreshHealth()
        {
            if (ViewModel != null)
            {
                SetCurHealth(ViewModel.m_playerData.currentHealth);
                SetMaxHealth(ViewModel.m_playerData.maxHealth);
            }
            refreshTeleport();
        }
        void refreshTeleport()
        {
            SetTeleportTime(GameModel.Instance.TeleportTime);
        }
        public void SetCurHealth(int health)
        {
            foreach (Transform item in m_curHealth)
            {
                item.gameObject.SetActive(false);
            }
            for (int i = 0; i < health; i++)
            {
                m_curHealth.GetChild(i).gameObject.SetActive(true);
            }
        }
        public void SetMaxHealth(int health)
        {
            foreach (Transform item in m_maxHealth)
            {
                item.gameObject.SetActive(false);
            }
            for (int i = 0; i < health; i++)
            {
                m_maxHealth.GetChild(i).gameObject.SetActive(true);
            }
        }

        public void SetTeleportTime(int time)
        {
            foreach (Transform item in m_teleportTime)
            {
                item.gameObject.SetActive(false);
            }
            for (int i = 0; i < GameModel.Instance.TeleportTime; i++)
            {
                m_teleportTime.GetChild(i).gameObject.SetActive(true);
            }
        }
        public void SetDepth(int depth)
        {
            m_depth.text = $"{depth.ToString()} M";
        }
        public override void HandleViewModelPropertyChanged(object sender, PropertyValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Depth):
                    SetDepth(ViewModel.Depth);
                    break;
                default:
                    break;
            }
        }
    }
}

