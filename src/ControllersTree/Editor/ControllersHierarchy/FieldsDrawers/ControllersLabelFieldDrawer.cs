using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Playtika.Controllers.Editor
{
    public class ControllersLabelFieldDrawer : ControllersBaseFieldDrawer
    {
        public ControllersLabelFieldDrawer(
            FieldInfo fieldInfo,
            object target)
            : base(fieldInfo, target)
        {
        }

        protected override void OnDraw()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label(FieldInfo.Name);
                GUILayout.FlexibleSpace();
                GUILayout.Label(FieldInfo.GetValue(Target)?.ToString() ?? NULL_VALUE);
            }
        }
    }
}
