﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Crosstales.BWF.EditorUtil;

namespace Crosstales.BWF.EditorExtension
{
   /// <summary>Base-class for custom editors of children of the 'BaseProvider'-class.</summary>
   public abstract class BaseProviderEditor : Editor
   {
      #region Variables

      private Crosstales.BWF.Provider.BaseProvider _script;

      #endregion


      #region Editor methods

      protected virtual void OnEnable()
      {
         _script = (Crosstales.BWF.Provider.BaseProvider)target;
      }

      public override void OnInspectorGUI()
      {
         DrawDefaultInspector();

         if (_script.isActiveAndEnabled)
         {
            if (_script.Sources?.Count > 0)
            {
               //do nothing
               EditorHelper.SeparatorUI();

               GUILayout.Label("Stats", EditorStyles.boldLabel);
               GUILayout.Label($"Regex Count:\t{_script.RegexCount}");
            }
            else
            {
               EditorHelper.SeparatorUI();
               EditorGUILayout.HelpBox("Please add an entry to 'Sources'!", MessageType.Warning);
            }
         }
         else
         {
            EditorHelper.SeparatorUI();

            EditorGUILayout.HelpBox("Script is disabled!", MessageType.Info);
         }
      }

      #endregion
   }
}
#endif
// © 2016-2024 crosstales LLC (https://www.crosstales.com)