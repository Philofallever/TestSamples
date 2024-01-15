using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TestBundle : MonoBehaviour
{
    public string ABRoot = "yoo/default/";
    public string atlasBundleName = "default_brid2atlas.bundle";
    public string prefabBundleName = "default_birdprefab2.bundle";

    public Image img;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button("LoadAtlasAndChangeImg")]
    public void LoadAtlasAndChangeImg()
    {
        AssetBundle ab = null;
        var abs = AssetBundle.GetAllLoadedAssetBundles();
        foreach (var bundle in abs)
        {
            if (bundle.name.Contains(atlasBundleName))
            {
                ab = bundle;
                print("AB exist");
            }
        }
        if (ab == null)
        {
            var pngBundlePath = Application.streamingAssetsPath + "/" + ABRoot + "default_brid2png.bundle";
            if (File.Exists(pngBundlePath))
            {
                var p1 = Application.streamingAssetsPath + "/" + ABRoot + "default_brid2png.bundle";
                AssetBundle.LoadFromFile(p1);
            }

            var p = Application.streamingAssetsPath + "/" + ABRoot + atlasBundleName;
            ab = AssetBundle.LoadFromFile(p);
        }
        var atlas = ab.LoadAsset<SpriteAtlas>("Assets/bird2x/bird2.spriteatlas");
        //var atlas = ab.LoadAsset<SpriteAtlas>("bird2");
        var index = UnityEngine.Random.Range(0, atlas.spriteCount);
        var spName = "0000" + index.ToString("d2");
        var sp = atlas.GetSprite(spName);
        print($"{spName} {sp}");
        img.sprite = sp;
    }

    public void SetImageSPNull()
    {
        img.sprite = null;
    }

    [Button("UnloadUnused")]
    public void UnloadUnused()
    {
        Resources.UnloadUnusedAssets().completed += (op) => print("unload unused completed!");
    }

    [Button("DumpBundle")]
    public void DumpBundle()
    {
        var abs = AssetBundle.GetAllLoadedAssetBundles();
        print("all bundles:\n" + string.Join('\n', abs.Select(x => x.ToString())));
    }

    [Button("DumpTexture")]
    public void DumpTexture()
    {
        var texs = Resources.FindObjectsOfTypeAll<Texture2D>();
        print("all textures:\n" + string.Join('\n', texs.Where(x => x.name.StartsWith("sactx")).Select(x => x.ToString())));
    }

    [Button("DumpAtlas")]
    public void DumpAtlas()
    {
        var atlases = Resources.FindObjectsOfTypeAll<SpriteAtlas>();
        print("all atlas:\n" + string.Join('\n', atlases.Select(x => x.ToString())));
    }

    [Button("DumpSprite")]
    public void DumpSprite()
    {
        var sps = Resources.FindObjectsOfTypeAll<Sprite>();
        print("all sprites:\n" + string.Join('\n', sps.Where(x => x.ToString().StartsWith("0000")).Select(x => x.ToString())));
    }

    [Title("unload"), Button("unloadAllBundle")]
    public void UnloadAllBundle(bool unloadAsset)
    {
        AssetBundle.UnloadAllAssetBundles(unloadAsset);
    }

    [Button]
    public void UnloadAllBundleFalse()
    {
        AssetBundle.UnloadAllAssetBundles(false);
    }

    [Button("UnloadAtlasBundle")]
    public void UnloadAtlasBundle()
    {
        var abs = AssetBundle.GetAllLoadedAssetBundles();
        foreach (var ab in abs)
        {
            if (ab.name.Contains(atlasBundleName, System.StringComparison.OrdinalIgnoreCase))
            {
                var atlas = ab.LoadAsset<SpriteAtlas>("Assets/bird2x/bird2.spriteatlas");
                Resources.UnloadAsset(atlas);
                ab.Unload(false);

            }
            else if (ab.name.Contains("default_brid2png", StringComparison.OrdinalIgnoreCase))
            {

                ab.Unload(false);
            }
        }
    }

    [Button("UnloadAtlasRes")]
    public void UnloadAtlasRes()
    {
        var atlases = Resources.FindObjectsOfTypeAll<SpriteAtlas>();
        foreach (var atlas in atlases)
        {
            print("Unload atlas: " + atlas);
            Resources.UnloadAsset(atlas);
        }
    }

    [Button("UnloadTexRes")]
    public void UnloadTexRes()
    {
        var texs = Resources.FindObjectsOfTypeAll<Texture2D>();
        foreach (var tex in texs)
        {
            print("Unload tex: " + tex);
            Resources.UnloadAsset(tex);
        }
    }

    [Button("UnloadSpriteRes")]
    public void UnloadSpriteRes()
    {
        var sps = Resources.FindObjectsOfTypeAll<Sprite>();
        foreach (var sp in sps)
        {
            print("unload sp: " + sp);
            DestroyImmediate(sp);
        }
    }
}
