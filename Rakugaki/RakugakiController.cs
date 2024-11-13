using UnityEngine;
using Rakugaki.HarmonyPatches;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using Rakugaki.UI;
using Rakugaki.Configuration;
using System.Collections.Generic;

namespace Rakugaki
{
    public class RakugakiController : MonoBehaviour
    {
        public static RakugakiController instance { get; private set; }
        internal static RakugakiRender _controller;
        public int drawCount = 0;
        internal ModMainFlowCoordinator mainFlowCoordinator;

        private void Awake()
        {
            if (instance != null)
            {
                Logger.log?.Warn($"Instance of {this.GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            instance = this;
            Logger.log?.Debug($"{name}: Awake()");

        }

        private void OnDestroy()
        {
            Logger.log?.Debug($"{name}: OnDestroy()");
            instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }

        private void Start()
        {
            if (PluginConfig.Instance.SaveDrawState)
                LoadDrawData();
        }

        public void UndoDraw()
        {
            foreach(Transform child in this.transform)
            {
                if(child.gameObject.name== $"RakugakiObject_{drawCount-1}")
                {
                    Destroy(child.gameObject);
                    drawCount--;
                    break;
                }
            }
        }
        public void AllEraseDraw()
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
            drawCount=0;
        }

        public void SetRender()
        {
            if (_controller) Destroy(_controller);
            if (VRPointerPatch.Instance != null)
            {
                var pointer = VRPointerPatch.Instance;
                _controller = pointer.gameObject.AddComponent<RakugakiRender>();
                _controller.init(this);
            }
        }
        public void UnsetRender()
        {
            if (_controller)
                Destroy(_controller.GetComponent<RakugakiRender>());
                
        }
        public void SaveDrawData()
        {
            int i;
            LineRenderer renderer;
            PluginConfig.DrawDataElements element;
            PluginConfig.Instance.DrawData = new List<PluginConfig.DrawDataElements>();
            foreach (Transform child in this.transform)
            {
                element = new PluginConfig.DrawDataElements();
                renderer = child.gameObject.GetComponent<LineRenderer>();
                element.DrawColor = $"#{ColorUtility.ToHtmlStringRGB(renderer.startColor)}";
                element.PenSize = renderer.startWidth;
                var positions = new Vector3[renderer.positionCount];
                int count = renderer.GetPositions(positions);
                for (i = 0; i < count; i++)
                    element.DrawElements.Add(positions[i]);
                PluginConfig.Instance.DrawData.Add(element);
            }
        }

        public void LoadDrawData()
        {
            LineRenderer render;
            GameObject _lineObject;
            Color color;
            drawCount = 0;
            foreach(PluginConfig.DrawDataElements elements in PluginConfig.Instance.DrawData)
            {
                _lineObject = new GameObject($"RakugakiObject_{drawCount++}");
                _lineObject.transform.SetParent(this.transform);
                _lineObject.layer = 5;
                render = _lineObject.AddComponent<LineRenderer>();
                render.material = new Material(Shader.Find("Sprites/Default"));
                ColorUtility.TryParseHtmlString(elements.DrawColor,out color);
                render.startColor = render.endColor = color;
                render.startWidth = render.endWidth = elements.PenSize;
                render.positionCount = elements.DrawElements.Count;
                render.SetPositions(elements.DrawElements.ToArray());
            }
        }

    }
}
