using System.Runtime.CompilerServices;
using UnityEngine;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Collections.Generic;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace Rakugaki.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        //public virtual int IntValue { get; set; } = 42; // Must be 'virtual' if you want BSIPA to detect a value change and save the config automatically.

        [NonNullable]
        public virtual bool SaveDrawState { get; set; } = false;
        [NonNullable]
        public virtual float DelayToStartDrawing { get; set; } = 0.01f;

        [UseConverter(typeof(ListConverter<DrawDataElements>))]
        public virtual List<DrawDataElements> DrawData { get; set; } = new List<DrawDataElements>();

        public class DrawDataElements
        {
            public virtual string DrawColor { get; set; } = $"#{ColorUtility.ToHtmlStringRGB(Color.white)}";
            public virtual float PenSize { get; set; } = 0.01f;
            [UseConverter(typeof(ListConverter<Vector3>))]
            public virtual List<Vector3> DrawElements { get; set; } = new List<Vector3>();

        }
        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}
