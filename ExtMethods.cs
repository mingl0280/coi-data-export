using Mafi;
using Mafi.Base;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Mods;
using Mafi.Localization;
using Mafi.Unity;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Entities.Static;
using UnityEngine;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Population;
using Mafi.Core.Research;
using Mafi.Core.UnlockingTree;

namespace COIDataExport
{
    public static class ExtMethods
    {
        public static HashSet<string> CreatedImgs = new HashSet<string>();
        public static LocStr GetEnUsStrFromLocStr(this LocStr str)
        {
            return LocalizationManager.LoadLocalizedString0(str.Id);
        }

        public static void SaveGameObjectToPng(this GameObject obj, string outputPath, int width = 256, int height = 256)
        {
            // 1. Create a temporary Camera and RenderTexture to capture the GameObject
            Camera temp_camera = new GameObject("TempCamera").AddComponent<Camera>();
            temp_camera.clearFlags = CameraClearFlags.SolidColor;
            temp_camera.backgroundColor = Color.clear;
            temp_camera.transform.position = obj.transform.position + new Vector3(0, 0, -2); // Adjust this as needed
            RenderTexture render_texture = new RenderTexture(width, height, 24);
            temp_camera.targetTexture = render_texture;

            // Render the GameObject using the Camera
            temp_camera.Render();

            // 2. Convert the RenderTexture to Texture2D
            RenderTexture.active = render_texture;
            Texture2D texture_2d = new Texture2D(render_texture.width, render_texture.height, TextureFormat.RGBA32, false);
            texture_2d.ReadPixels(new Rect(0, 0, render_texture.width, render_texture.height), 0, 0);
            texture_2d.Apply();

            // Clean up temporary GameObject and RenderTexture
            RenderTexture.active = null;
            Object.DestroyImmediate(temp_camera.gameObject);

            // 3. Save Texture2D to PNG
            byte[] byte_array = texture_2d.EncodeToPNG();
            File.WriteAllBytes(outputPath, byte_array);
        }

        public static Main GetMainInstance()
        {
            var game_instance = UnityEngine.Object.FindObjectOfType<GameMainMb>();
            FieldInfo main_field =
                typeof(GameMainMb).GetField("m_main", BindingFlags.NonPublic | BindingFlags.Instance);
            if (main_field == null)
            {
                Log.Info("Cannot find main field!");
                return null;
            }
            var main_opt = (Option<Main>)main_field.GetValue(game_instance);
            var main_obj = main_opt.Value;
            return main_obj;
        }


        public static void SaveTextureToPng(this Texture2D texture, string filePath)
        {
            // 1. Encode Texture2D to PNG
            byte[] png_data = texture.EncodeToPNG();

            // 2. Save to File
            if (png_data != null)
            {
                File.WriteAllBytes(filePath, png_data);
                Debug.Log("Saved to " + filePath);
            }
            else
            {
                Debug.LogError("Failed to convert Texture2D to PNG.");
            }
        }
    }
}