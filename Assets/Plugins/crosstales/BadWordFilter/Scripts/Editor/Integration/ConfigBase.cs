﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Crosstales.BWF.EditorUtil;
using Crosstales.BWF.EditorTask;
using Crosstales.BWF.Util;

namespace Crosstales.BWF.EditorIntegration
{
   /// <summary>Base class for editor windows.</summary>
   public abstract class ConfigBase : EditorWindow
   {
      #region Variables

      private static string _updateText = UpdateCheck.TEXT_NOT_CHECKED;
      private static UpdateStatus _updateStatus = UpdateStatus.NOT_CHECKED;

      private System.Threading.Thread _worker;

      private Vector2 _scrollPosConfig;
      private Vector2 _scrollPosHelp;
      private Vector2 _scrollPosAboutUpdate;
      private Vector2 _scrollPosAboutReadme;
      private Vector2 _scrollPosAboutVersions;

      private static string _readme;
      private static string _versions;

      private int _aboutTab;

      private static readonly System.Random _rnd = new System.Random();

      private readonly int _adRnd1 = _rnd.Next(0, 3);
      private readonly int _adRnd2 = _rnd.Next(0, 3);
      private readonly int _adRnd3 = _rnd.Next(0, 3);

      #endregion


      #region Protected methods

      protected void showConfiguration()
      {
         GUI.skin.label.wordWrap = true;

         _scrollPosConfig = EditorGUILayout.BeginScrollView(_scrollPosConfig, false, false);
         {
            GUILayout.Label("General Settings", EditorStyles.boldLabel);

            //EditorConfig.PREFAB_AUTOLOAD = EditorGUILayout.Toggle(new GUIContent("Prefab Auto-Load", $"Enable or disable auto-loading of the prefabs to the scene (default: {EditorConstants.DEFAULT_PREFAB_AUTOLOAD})."), EditorConfig.PREFAB_AUTOLOAD);

            Config.DEBUG = EditorGUILayout.Toggle(new GUIContent("Debug", $"Enable or disable debug logs (default: {Constants.DEFAULT_DEBUG})."), Config.DEBUG);
            Config.DEBUG_BADWORDS = EditorGUILayout.Toggle(new GUIContent("Debug BadWords", $"Enable or disable debug logging for BadWords (Attention: slow!, default: {Constants.DEFAULT_DEBUG_BADWORDS})."), Config.DEBUG_BADWORDS);
            Config.DEBUG_DOMAINS = EditorGUILayout.Toggle(new GUIContent("Debug Domains", $"Enable or disable debug logging for Domains (Attention: VERY SLOOOOOOOOWWWW!, default: {Constants.DEFAULT_DEBUG_DOMAINS})."), Config.DEBUG_DOMAINS);

            EditorConfig.UPDATE_CHECK = EditorGUILayout.Toggle(new GUIContent("Update Check", $"Enable or disable the update-checks for the asset (default: {EditorConstants.DEFAULT_UPDATE_CHECK})"), EditorConfig.UPDATE_CHECK);

            //EditorConfig.COMPILE_DEFINES = EditorGUILayout.Toggle(new GUIContent("Compile Defines", $"Enable or disable adding compile define 'CT_BWF' for the asset (default: {EditorConstants.DEFAULT_COMPILE_DEFINES})"), EditorConfig.COMPILE_DEFINES);

            EditorHelper.SeparatorUI();

            GUILayout.Label("BWF Manager", EditorStyles.boldLabel);
            EditorConfig.HIERARCHY_ICON = EditorGUILayout.Toggle(new GUIContent("Show Hierarchy Icon", $"Show hierarchy icon (default: {EditorConstants.DEFAULT_HIERARCHY_ICON})."), EditorConfig.HIERARCHY_ICON);
         }
         EditorGUILayout.EndScrollView();
      }

      protected void showHelp()
      {
         _scrollPosHelp = EditorGUILayout.BeginScrollView(_scrollPosHelp, false, false);
         {
            GUILayout.Label("Resources", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            {
               GUILayout.BeginVertical();
               {
                  if (GUILayout.Button(new GUIContent(" Manual", EditorHelper.Icon_Manual, "Show the manual.")))
                     Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_MANUAL_URL);

                  GUILayout.Space(6);

                  if (GUILayout.Button(new GUIContent(" Forum", EditorHelper.Icon_Forum, "Visit the forum page.")))
                     Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_FORUM_URL);
               }
               GUILayout.EndVertical();

               GUILayout.BeginVertical();
               {
                  if (GUILayout.Button(new GUIContent(" API", EditorHelper.Icon_API, "Show the API.")))
                     Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_API_URL);

                  GUILayout.Space(6);

                  if (GUILayout.Button(new GUIContent(" Product", EditorHelper.Icon_Product, "Visit the product page.")))
                     Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_WEB_URL);
               }
               GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            EditorHelper.SeparatorUI();

            GUILayout.Label("Videos", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            {
               if (GUILayout.Button(new GUIContent(" Promo", EditorHelper.Video_Promo, "View the promotion video on 'Youtube'.")))
                  Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_VIDEO_PROMO);

               if (GUILayout.Button(new GUIContent(" Tutorial", EditorHelper.Video_Tutorial, "View the tutorial video on 'Youtube'.")))
                  Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_VIDEO_TUTORIAL);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(6);

            if (GUILayout.Button(new GUIContent(" All Videos", EditorHelper.Icon_Videos, "Visit our 'Youtube'-channel for more videos.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_SOCIAL_YOUTUBE);

            EditorHelper.SeparatorUI();

            GUILayout.Label("3rd Party Assets", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            {
               if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Asset_PlayMaker, "More information about 'PlayMaker'.")))
                  Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_3P_PLAYMAKER);

               //CT Ads
               switch (_adRnd1)
               {
                  case 0:
                  {
                     //if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_BWF, "More information about 'Bad Word Filter'.")))
                     //   Util.Helper.OpenURL(Constants.ASSET_BWF);

                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_TPB, "More information about 'Turbo Builder'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_TPB);

                     break;
                  }
                  case 1:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_DJ, "More information about 'DJ'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_DJ);

                     break;
                  }
                  default:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_FB, "More information about 'File Browser'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_FB);

                     break;
                  }
               }

               switch (_adRnd2)
               {
                  case 0:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_Radio, "More information about 'Radio'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_RADIO);

                     break;
                  }
                  case 1:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_TB, "More information about 'Turbo Backup'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_TB);

                     break;
                  }
                  default:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_OC, "More information about 'Online Check'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_OC);

                     break;
                  }
               }

               switch (_adRnd3)
               {
                  case 0:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_TPS, "More information about 'Turbo Switch'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_TPS);

                     break;
                  }
                  case 1:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_RTV, "More information about 'RT-Voice'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_RTV);

                     break;
                  }
                  default:
                  {
                     if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset_TR, "More information about 'True Random'.")))
                        Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_TR);

                     break;
                  }
               }
            }
            GUILayout.EndHorizontal();
         }
         EditorGUILayout.EndScrollView();

         GUILayout.Space(6);
      }

      protected void showAbout()
      {
         GUILayout.Space(3);
         GUILayout.Label(Constants.ASSET_NAME, EditorStyles.boldLabel);

         GUILayout.BeginHorizontal();
         {
            GUILayout.BeginVertical(GUILayout.Width(60));
            {
               GUILayout.Label("Version:");

               GUILayout.Space(12);

               GUILayout.Label("Web:");

               GUILayout.Space(2);

               GUILayout.Label("Email:");
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(170));
            {
               GUILayout.Space(0);

               GUILayout.Label(Constants.ASSET_VERSION);

               GUILayout.Space(12);

               EditorGUILayout.SelectableLabel(Constants.ASSET_AUTHOR_URL, GUILayout.Height(16), GUILayout.ExpandHeight(false));

               GUILayout.Space(2);

               EditorGUILayout.SelectableLabel(Constants.ASSET_CONTACT, GUILayout.Height(16), GUILayout.ExpandHeight(false));
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            {
               //GUILayout.Space(0);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(64));
            {
               if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset, "Visit asset website")))
                  Crosstales.Common.Util.NetworkHelper.OpenURL(EditorConstants.ASSET_URL);
            }
            GUILayout.EndVertical();
         }
         GUILayout.EndHorizontal();

         GUILayout.Label($"© 2015-2024 by {Constants.ASSET_AUTHOR}");

         EditorHelper.SeparatorUI();

         GUILayout.BeginHorizontal();
         {
            if (GUILayout.Button(new GUIContent(" AssetStore", EditorHelper.Logo_Unity, "Visit the 'Unity AssetStore' website.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_CT_URL);

            if (GUILayout.Button(new GUIContent($" {Constants.ASSET_AUTHOR}", EditorHelper.Logo_CT, $"Visit the '{Constants.ASSET_AUTHOR}' website.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_AUTHOR_URL);
         }
         GUILayout.EndHorizontal();

         EditorHelper.SeparatorUI();

         _aboutTab = GUILayout.Toolbar(_aboutTab, new[] { "Readme", "Versions", "Update" });

         switch (_aboutTab)
         {
            case 2:
            {
               _scrollPosAboutUpdate = EditorGUILayout.BeginScrollView(_scrollPosAboutUpdate, false, false);
               {
                  Color fgColor = GUI.color;

                  GUI.color = Color.yellow;

                  switch (_updateStatus)
                  {
                     case UpdateStatus.NO_UPDATE:
                        GUI.color = Color.green;
                        GUILayout.Label(_updateText);
                        break;
                     case UpdateStatus.UPDATE:
                     {
                        GUILayout.Label(_updateText);

                        if (GUILayout.Button(new GUIContent(" Download", "Visit the 'Unity AssetStore' to download the latest version.")))
                           UnityEditorInternal.AssetStore.Open($"content/{EditorConstants.ASSET_ID}");

                        break;
                     }
                     case UpdateStatus.UPDATE_VERSION:
                     {
                        GUILayout.Label(_updateText);

                        if (GUILayout.Button(new GUIContent(" Upgrade", "Upgrade to the newer version in the 'Unity AssetStore'")))
                           Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_CT_URL);

                        break;
                     }
                     case UpdateStatus.DEPRECATED:
                     {
                        GUILayout.Label(_updateText);

                        if (GUILayout.Button(new GUIContent(" More Information", "Visit the 'crosstales'-site for more information.")))
                           Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_AUTHOR_URL);

                        break;
                     }
                     default:
                        GUI.color = Color.cyan;
                        GUILayout.Label(_updateText);
                        break;
                  }

                  GUI.color = fgColor;
               }
               EditorGUILayout.EndScrollView();

               if (_updateStatus == UpdateStatus.NOT_CHECKED || _updateStatus == UpdateStatus.NO_UPDATE)
               {
                  bool isChecking = !(_worker == null || _worker?.IsAlive == false);

                  GUI.enabled = Crosstales.Common.Util.NetworkHelper.isInternetAvailable && !isChecking;

                  if (GUILayout.Button(new GUIContent(isChecking ? "Checking... Please wait." : " Check For Update", EditorHelper.Icon_Check, $"Checks for available updates of {Constants.ASSET_NAME}")))
                  {
                     _worker = new System.Threading.Thread(() => UpdateCheck.UpdateCheckForEditor(out _updateText, out _updateStatus));
                     _worker.Start();
                  }

                  GUI.enabled = true;
               }

               break;
            }
            case 0:
            {
               if (_readme == null)
               {
                  string path = $"{Application.dataPath}{EditorConfig.ASSET_PATH}README.txt";

                  try
                  {
                     _readme = Crosstales.Common.Util.FileHelper.ReadAllText(path);
                  }
                  catch (System.Exception)
                  {
                     _readme = $"README not found: {path}";
                  }
               }

               _scrollPosAboutReadme = EditorGUILayout.BeginScrollView(_scrollPosAboutReadme, false, false);
               {
                  GUILayout.Label(_readme);
               }
               EditorGUILayout.EndScrollView();
               break;
            }
            default:
            {
               if (_versions == null)
               {
                  string path = $"{Application.dataPath}{EditorConfig.ASSET_PATH}Documentation/VERSIONS.txt";

                  try
                  {
                     _versions = Crosstales.Common.Util.FileHelper.ReadAllText(path);
                  }
                  catch (System.Exception)
                  {
                     _versions = $"VERSIONS not found: {path}";
                  }
               }

               _scrollPosAboutVersions = EditorGUILayout.BeginScrollView(_scrollPosAboutVersions, false, false);
               {
                  GUILayout.Label(_versions);
               }

               EditorGUILayout.EndScrollView();
               break;
            }
         }

         EditorHelper.SeparatorUI(6);

         GUILayout.BeginHorizontal();
         {
            if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Social_Discord, "Communicate with us via 'Discord'.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_SOCIAL_DISCORD);

            if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Social_Facebook, "Follow us on 'Facebook'.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_SOCIAL_FACEBOOK);

            if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Social_Twitter, "Follow us on 'Twitter'.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_SOCIAL_TWITTER);

            if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Social_Linkedin, "Follow us on 'LinkedIn'.")))
               Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_SOCIAL_LINKEDIN);
         }
         GUILayout.EndHorizontal();

         GUILayout.Space(6);
      }

      protected static void save()
      {
         Config.Save();
         EditorConfig.Save();

         if (Config.DEBUG)
            Debug.Log("Config data saved");
      }

      #endregion
   }
}
#endif
// © 2016-2021 crosstales LLC (https://www.crosstales.com)