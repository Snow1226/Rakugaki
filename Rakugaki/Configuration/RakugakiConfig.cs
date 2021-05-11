using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rakugaki.Configuration
{
    internal class RakugakiConfig
    {

        public bool SaveDrawState { get; set; } = false;
        public float DelayToStartDrawing { get; set; } = 0.01f;

        public List<DrawDataElements> DrawData { get; set; } = new List<DrawDataElements>();
    }
    internal class DrawDataElements
    {
        public string DrawColor { get; set; } = $"#{ColorUtility.ToHtmlStringRGB(Color.white)}";
        public float PenSize { get; set; } = 0.01f;
        public List<Vector3> DrawElements { get; set; }

    }

    internal class Position
    {
        float x { get; set; } = 0.0f;
        float y { get; set; } = 0.0f;
        float z { get; set; } = 0.0f;

    }
}
