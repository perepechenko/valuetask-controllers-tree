using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Playtika.Controllers.Editor
{
    public class ControllersTabBarDrawer
    {
        private const float TabWidth = 128f;
        private const float TabHeight = 24f;
        private const float CloseButtonSize = 16f;
        private const float CloseButtonPadding = 4f;

        private readonly ControllersTreeViewModel _model;

        private List<ControllerTabData> _tabData;
        private readonly List<ControllerTabData> _pendingAddData = new List<ControllerTabData>();
        private int _index = 0;
        private int? _removeIndex;
        private Vector2 _scrollPosition;

        public ControllersTabBarDrawer(ControllersTreeViewModel model, ControllerTabData rootTabData)
        {
            _model = model;
            SetData(rootTabData);
        }

        private void SetData(ControllerTabData rootTabData)
        {
            _tabData = new List<ControllerTabData>();
            _tabData.Add(rootTabData);
            Select(_index);
        }

        public void AddData(ControllerTabData tabData)
        {
            _pendingAddData.Add(tabData);
        }

        private void RemoveData(int index)
        {
            if (_index >= index)
            {
                --_index;
            }

            _removeIndex = index;
        }

        private void Select(int index)
        {
            _index = Mathf.Clamp(index, 0, _tabData?.Count - 1 ?? 0);
        }

        public void Draw()
        {
            if (_tabData == null || _tabData.Count == 0)
            {
                return;
            }

            ApplyPendingChanges();

            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                var index = _index;

                DrawTabBar();

                if (changeScope.changed)
                {
                    _tabData[index].TabOffCallback?.Invoke();
                    _tabData[_index].TabOnCallback?.Invoke();
                }

                _tabData[_index].TabDrawCallback?.Invoke();
            }
        }

        public void Reload()
        {
            if (_tabData == null || _tabData.Count == 0 || _removeIndex.HasValue || _pendingAddData.Count > 0)
            {
                return;
            }

            _tabData[_index].TabReloadCallback();
        }

        private void ApplyPendingChanges()
        {
            if (Event.current == null || Event.current.type != EventType.Layout)
            {
                return;
            }

            if (_removeIndex.HasValue)
            {
                _tabData.RemoveAt(_removeIndex.Value);
                _removeIndex = null;
            }

            if (_pendingAddData.Count > 0)
            {
                _tabData.AddRange(_pendingAddData);
                _pendingAddData.Clear();
            }

            Select(_index);
        }

        private void DrawTabBar()
        {
            using var scrollScope = new EditorGUILayout.ScrollViewScope(_scrollPosition, GUIStyle.none, GUIStyle.none, GUILayout.ExpandHeight(false));
            _scrollPosition = scrollScope.scrollPosition;

            using var scope = new EditorGUILayout.HorizontalScope();
            for (int i = 0; i < _tabData.Count; ++i)
            {
                DrawTabButton(i);
            }
        }

        private void DrawTabButton(int index)
        {
            var tabData = _tabData[index];
            var tabStyle = GUI.skin.box ?? EditorStyles.helpBox;
            var rect = GUILayoutUtility.GetRect(
                TabWidth,
                TabHeight,
                tabStyle,
                GUILayout.Width(TabWidth),
                GUILayout.Height(TabHeight));

            Rect? closeButtonRect = null;
            if (tabData.IsClosable)
            {
                closeButtonRect = new Rect(
                    rect.xMax - CloseButtonSize - CloseButtonPadding,
                    rect.y + (rect.height - CloseButtonSize) * 0.5f,
                    CloseButtonSize,
                    CloseButtonSize);
            }

            var currentEvent = Event.current;
            if (currentEvent.type == EventType.MouseDown &&
                currentEvent.button == 0 &&
                rect.Contains(currentEvent.mousePosition) &&
                (closeButtonRect == null || !closeButtonRect.Value.Contains(currentEvent.mousePosition)))
            {
                _index = index;
                GUI.changed = true;
                currentEvent.Use();
            }

            var color = GUI.backgroundColor;
            GUI.backgroundColor = Color.clear;
            GUI.Box(rect, tabData.TabContent, tabStyle);
            GUI.backgroundColor = color;

            if (closeButtonRect.HasValue)
            {
                var closeButtonStyle = _model.CloseButtonStyle ?? EditorStyles.miniButton;
                if (GUI.Button(closeButtonRect.Value, GUIContent.none, closeButtonStyle))
                {
                    RemoveData(index);
                }
            }

            var colorRect = _index == index
                                ? ControllersTreeHelper.SelectedColor
                                : ControllersTreeHelper.UnselectedColor;
            var lineRect = rect;
            lineRect.height = 2;
            var center = lineRect.center;
            center.y += rect.height;
            lineRect.center = center;

            if (rect.Contains(Event.current.mousePosition))
            {
                colorRect = ControllersTreeHelper.FocusedColor;
            }

            EditorGUI.DrawRect(lineRect, colorRect);
        }
    }
}
