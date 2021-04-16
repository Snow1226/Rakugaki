using UnityEngine;
using VRUIControls;

namespace Rakugaki
{
    public class RakugakiRender : MonoBehaviour
    {
        private RakugakiController _rakugakiParent;
        protected GameObject _lineObject = null;
        private LineRenderer render;
        protected VRPointer _vrPointer;

        public void init(RakugakiController instance)
        {
            _rakugakiParent = instance;
            _vrPointer = GetComponent<VRPointer>();
            Logger.log?.Debug($"{name}: init()");
            if (_vrPointer == null)
                Logger.log?.Debug($"{name}: vrPointer Null");
        }

        void Update()
        {
            if (_vrPointer != null)
            {
                if (_vrPointer.vrController != null)
                {
                    if (_vrPointer.vrController.triggerValue > 0.9f)
                    {
                        if (_lineObject == null)
                        {
                            _lineObject = new GameObject($"RakugakiObject_{_rakugakiParent.drawCount++}");
                            _lineObject.transform.SetParent(_rakugakiParent.transform);
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
                    else
                    {
                        if (_lineObject != null)
                        {
                            _lineObject = null;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        _rakugakiParent.UndoDraw();
                    }
                    if (Input.GetKeyDown(KeyCode.Delete))
                    {
                        _rakugakiParent.AllEraseDraw();
                    }
                }
            }
        }
    }
}
