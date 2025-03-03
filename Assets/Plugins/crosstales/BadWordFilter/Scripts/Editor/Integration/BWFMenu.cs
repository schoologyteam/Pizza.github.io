﻿#if UNITY_EDITOR
using UnityEditor;
using Crosstales.BWF.EditorUtil;
using Crosstales.BWF.Util;

namespace Crosstales.BWF.EditorIntegration
{
   /// <summary>Editor component for the "Tools"-menu.</summary>
   public static class BWFMenu
   {
      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Prefabs/" + Constants.MANAGER_SCENE_OBJECT_NAME, false, EditorHelper.MENU_ID + 20)]
      private static void AddBWF()
      {
         EditorHelper.InstantiatePrefab(Constants.MANAGER_SCENE_OBJECT_NAME);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Prefabs/" + Constants.MANAGER_SCENE_OBJECT_NAME, true)]
      private static bool AddBWFValidator()
      {
         return !EditorHelper.isBWFInScene;
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Create/Source", false, EditorHelper.MENU_ID + 300)]
      public static void CreateSource()
      {
         Helper.CreateSource();
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/Manual", false, EditorHelper.MENU_ID + 600)]
      private static void ShowManual()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_MANUAL_URL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/API", false, EditorHelper.MENU_ID + 610)]
      private static void ShowAPI()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_API_URL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/Forum", false, EditorHelper.MENU_ID + 620)]
      private static void ShowForum()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_FORUM_URL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/Product", false, EditorHelper.MENU_ID + 630)]
      private static void ShowProduct()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_WEB_URL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/Videos/Promo", false, EditorHelper.MENU_ID + 650)]
      private static void ShowVideoPromo()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_VIDEO_PROMO);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/Videos/Tutorial", false, EditorHelper.MENU_ID + 660)]
      private static void ShowVideoTutorial()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_VIDEO_TUTORIAL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/Videos/All Videos", false, EditorHelper.MENU_ID + 680)]
      private static void ShowAllVideos()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_SOCIAL_YOUTUBE);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Help/3rd Party Assets", false, EditorHelper.MENU_ID + 700)]
      private static void Show3rdPartyAV()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_3P_PLAYMAKER);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/Check for Update...", false, EditorHelper.MENU_ID + 750)]
      private static void ShowUpdateCheck()
      {
         Crosstales.BWF.EditorTask.UpdateCheck.UpdateCheckWithDialog();
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/About/Unity AssetStore", false, EditorHelper.MENU_ID + 800)]
      private static void ShowUAS()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_CT_URL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/About/" + Constants.ASSET_AUTHOR, false, EditorHelper.MENU_ID + 820)]
      private static void ShowCT()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_AUTHOR_URL);
      }

      [MenuItem("Tools/" + Constants.ASSET_NAME + "/About/Info", false, EditorHelper.MENU_ID + 840)]
      private static void ShowInfo()
      {
         EditorUtility.DisplayDialog($"{Constants.ASSET_NAME} - About",
            $"Version: {Constants.ASSET_VERSION}{System.Environment.NewLine}{System.Environment.NewLine}© 2015-2024 by {Util.Constants.ASSET_AUTHOR}{System.Environment.NewLine}{System.Environment.NewLine}{Util.Constants.ASSET_AUTHOR_URL}{System.Environment.NewLine}", "Ok");
      }
   }
}
#endif
// © 2015-2024 crosstales LLC (https://www.crosstales.com)