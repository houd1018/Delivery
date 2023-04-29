using Cysharp.Threading.Tasks;
using Isekai.UI;
using Isekai.UI.Views.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Managers
{
    public enum ELayerType
    {
        None = 0,
        DefaultLayer = 1,
        HUDLayer = 2,
        TopbarLayer = 3,
        PopupLayer = 4,
        TransitionLayer = 5,

    }
    public class LayerManager : MonoSingleton<LayerManager>
    {
        Dictionary<ELayerType, Layer> m_layers;
        private void Start()
        {

        }
        public void Initialize()
        {
            m_layers = new Dictionary<ELayerType, Layer>();
            ELayerType[] layerEnums = (ELayerType[])Enum.GetValues(typeof(ELayerType));

            GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/Layer/DefaultLayer");

            if (prefab == null)
            {
                Debug.LogError("Failed to load Prefab: DefaultLayer. Please check whether it in the addressable. ");
                return;
            }

            for (int i = 1; i < layerEnums.Length; i++)
            {
                var go = Instantiate(prefab);
                go.name = layerEnums[i].ToString();
                Layer curLayer = go.GetComponent<Layer>();
                curLayer.LayerType = layerEnums[i];
                m_layers[layerEnums[i]] = curLayer;
                curLayer.CurCanvas.sortingOrder = (int)layerEnums[i];
                DontDestroyOnLoad(go);
                if (layerEnums[i] == ELayerType.TransitionLayer)
                {
                    var transitionScreen = Resources.Load<GameObject>("Prefabs/UI/Screens/"+EScreenType.TransitionScreen.ToString());
                    TransitionScreen screen = Instantiate(transitionScreen, go.transform).GetComponent<TransitionScreen>();
                    ScreenManager.Instance.CurTransitionScreen = screen;
                    LevelManager.Instance.TransitionScreen = screen;
                }
            }

        }

        public Transform GetLayer(ELayerType layer)
        {
            return m_layers[layer].transform;
        }

        public void SetLayerInteractable(ELayerType layer, bool isInteractable)
        {
            m_layers[layer].CurCanvasGroup.interactable = isInteractable;
            m_layers[layer].CurCanvasGroup.blocksRaycasts = isInteractable;
        }

    }

}
