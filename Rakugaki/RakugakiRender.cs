using UnityEngine;
using VRUIControls;
using System.Collections;

namespace Rakugaki
{
    public class RakugakiRender : MonoBehaviour
    {
        private RakugakiController _rakugakiParent;
        protected GameObject _lineObject = null;
        private LineRenderer render;
        protected VRPointer _vrPointer;
        private float _triggerTime;

        public void init(RakugakiController instance)
        {
            _rakugakiParent = instance;
            _vrPointer = GetComponent<VRPointer>();
            Logger.log?.Debug($"{name}: init()");
            if (_vrPointer == null)
                Logger.log?.Debug($"{name}: vrPointer Null");
            _triggerTime = 0;
        }

        void Update()
        {
            if (_vrPointer != null)
            {
                if (_vrPointer.vrController != null)
                {
                    if (_vrPointer.vrController.triggerValue > 0.9f)
                    {
                        if (_triggerTime == 0)
                            _triggerTime = Time.time;

                        if (Time.time-_triggerTime>Plugin.instance.drawDelay)
                        {
                            if (_lineObject == null)
                            {
                                _lineObject = new GameObject($"RakugakiObject_{_rakugakiParent.drawCount++}");
                                _lineObject.transform.SetParent(_rakugakiParent.transform);
                                _lineObject.layer = 5;
                                render = _lineObject.AddComponent<LineRenderer>();
                                render.material = new Material(Shader.Find("Sprites/Default"));
                                render.startColor = render.endColor = Plugin.instance.penColor;
                                render.startWidth = render.endWidth = Plugin.instance.penSize;
                                render.positionCount = 0;
                            }
                            int NextPositionIndex = render.positionCount;
                            render.positionCount = NextPositionIndex + 1;
                            render.SetPosition(NextPositionIndex, _vrPointer.vrController.transform.position);
                        }
                    }
                    else
                    {
                        _triggerTime = 0;
                        if (_lineObject != null)
                            _lineObject = null;
                    }
                }
            }
        }
    }
}
