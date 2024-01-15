using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [MenuItem("Test/build")]
    static void Test()
    {
        if (Directory.Exists("build-asset"))
        {
            Directory.Delete("build-asset", true);
        }
        Directory.CreateDirectory("build-asset");
        BuildPipeline.BuildAssetBundles("build-asset", BuildAssetBundleOptions.ForceRebuildAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        Debug.Log("打包资源完成");
    }

    [MenuItem("Test/RemoveUnusedNames")]
    static void RemoveUnusedNames()
    {
        var unused = AssetDatabase.GetUnusedAssetBundleNames();
        Debug.Log("无用名字: " + string.Join('\n', unused));
        AssetDatabase.RemoveUnusedAssetBundleNames();
        Debug.Log("remove unusednames success!");
    }

    [MenuItem("Test/RemoveAllNames")]
    static void RemoveAllNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            AssetDatabase.RemoveAssetBundleName(name, true);
        Debug.Log("remove all names success!");
    }

    [MenuItem("Test/PackAllAtlas")]
    static void PackAllAtlas()
    {
        SpriteAtlasUtility.PackAllAtlases(EditorUserBuildSettings.activeBuildTarget);
    }

    [MenuItem("TestAtlas/DumpBirdVarAtlasPacks")]
    static void DumpBirdV()
    {
        var guids = AssetDatabase.FindAssets("t:spriteatlas", new string[] { "Assets/BirdV" });
        foreach (var guid in guids)
        {
            var p = AssetDatabase.GUIDToAssetPath(guid);
            var b = new StringBuilder(p + "\n");
            var atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(p);
            var sps = new Sprite[atlas.spriteCount];
            var c = atlas.GetSprites(sps);
            b.AppendLine($"精灵个数{c}");
            b.AppendJoin(' ', sps as IEnumerable<Sprite>);
            b.Append('\n');
            if (atlas.isVariant)
            {
                atlas = atlas.GetMasterAtlas();
                b.AppendLine($"is v of {atlas}");
            }

            var packs = atlas.GetPackables();
            b.AppendJoin('\n', packs as IEnumerable<Object>);
            Debug.Log(b);
        }
    }

    static string bundles = "Bundles/WebGL/default/2024-01-04-944/default_assets_birdv_";
    static void LoadSpriteAtlas(string name)
    {
        AssetBundle.UnloadAllAssetBundles(true);
        var p = bundles + name + ".bundle";
        //bird - var1
        var bundle = AssetBundle.LoadFromFile(p);
        var assets =  bundle.LoadAllAssets();
       
        var b = new StringBuilder();
        b.AppendJoin('\n', assets as IEnumerable<Object>);        
        Debug.Log(b);

        Debug.Log("222" + bundle.LoadAsset("assets/birdv/bird-var1.spriteatlas"));

        var atlas = assets[0] as SpriteAtlas;
        var sps = new Sprite[atlas.spriteCount];
        var c = atlas.GetSprites(sps);
        var image = GameObject.FindFirstObjectByType<Image>();
        image.sprite = sps[Random.Range(0 ,sps.Length)];
    }
    
    [MenuItem("TestAtlas/LoadBird1")]     
    static void LoadBird1()
    {
        LoadSpriteAtlas("bird-var1");
    }

    [MenuItem("TestAtlas/LoadBird25")]
    static void LoadBird25()
    {
        LoadSpriteAtlas("bird-var25");
    }

}
