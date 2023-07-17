using AULib;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// 스프라이트 아틀라스 빌더
/// </summary>
public class SpriteAtlasBuilder : Editor
{

    /// <summary>
    /// 스프라이트 아틀라스 생성
    /// </summary>
    public static void BuildSpriteAtlas()
    {
        AULibSetting.Init();
        
        var atlasData = AssetDatabase.LoadAssetAtPath<SpriteAtlasData>(AULibSetting.ATLAS_LIST_DATA_PATH);
        atlasData.Clear();//

        BuildSpriteAtlasForBuild(atlasData);
        BuildSpriteAtlasForBundle(atlasData);

        SpriteAtlasUtility.PackAllAtlases(BuildTarget.StandaloneWindows64);

        //EditorUtility.SetDirty(atlasData);
        //AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void BuildSpriteAtlasForBuild(SpriteAtlasData atlasData)
    {
        var directories = Directory.GetDirectories(AULibSetting.RAW_SPRITE_BUILD_PATH);//
        BuildSpriteAtlasForHelper(atlasData, directories, AULibSetting.ATLAS_CREATE_BUILD_PATH, false);

    }

    private static void BuildSpriteAtlasForBundle(SpriteAtlasData atlasData)
    {
        var directories = Directory.GetDirectories(AULibSetting.RAW_SPRITE_BUNDLE_PATH);
        BuildSpriteAtlasForHelper(atlasData, directories, AULibSetting.ATLAS_CREATE_BUNDLE_PATH, true);
    }

    private static void BuildSpriteAtlasForHelper(SpriteAtlasData atlasData, string[] directories, string buildPath, bool isBundle)
    {

        foreach (var directory in directories)
        {
            var directoriyName = directory.Substring(directory.LastIndexOf('\\') + 1);


            SpriteAtlas sa = GetSpriteAtlas(isBundle);
            AssetDatabase.CreateAsset(sa, $"{buildPath}/{directoriyName}.spriteatlas");


            var files = Directory.GetFiles(directory, "*.png", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Sprite assetFile = AssetDatabase.LoadAssetAtPath<Sprite>(file.Replace('\\', '/'));
                if (assetFile != null)
                {
                    sa.Add(new Object[] { assetFile });
                }
            }

            atlasData.AddSpriteAtlas(directoriyName, isBundle);//

            //SpriteAtlasUtility.PackAllAtlases(BuildTarget.StandaloneWindows64);

            AssetDatabase.SaveAssets();
        }
        EditorUtility.SetDirty(atlasData);
        AssetDatabase.SaveAssets();
    }

    private static SpriteAtlasPackingSettings GetAtlasPackingSetting()
    {        
        SpriteAtlasPackingSettings settings = new();
        settings.enableRotation = false;
        settings.enableTightPacking = false;
        settings.padding = 4;
        return settings;
    }

    private static SpriteAtlasTextureSettings GetAtlasTextureSetting()
    {
        SpriteAtlasTextureSettings settings = new();
        settings.readable = false;
        settings.generateMipMaps = false;
        settings.sRGB = true;
        settings.filterMode = FilterMode.Bilinear;
        return settings;
    }

    private static TextureImporterPlatformSettings GetAtlasPlatformSetting()
    {
        TextureImporterPlatformSettings settings = new();
        settings.maxTextureSize = AULibSetting.ATLAS_MAX_SIZE;
        return settings;
    }

    private static SpriteAtlas GetSpriteAtlas(bool isBundle)
    {
        SpriteAtlas sa = new SpriteAtlas();
        sa.SetIncludeInBuild(!isBundle);
        sa.SetPackingSettings(GetAtlasPackingSetting());
        sa.SetTextureSettings(GetAtlasTextureSetting());
        sa.SetPlatformSettings(GetAtlasPlatformSetting());
        return sa;
    }
}
