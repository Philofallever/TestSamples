﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TestLoad : MonoBehaviour
{
    public string folderName = "StandaloneWindows-noinclude";
    public Transform p;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AssetBundle[] bundles = new AssetBundle[10];


    public SpriteAtlas atlas;

    public void LoadAtals()
    {
        var ab = LoadAB("atlas");
        print(ab);
        atlas = ab.LoadAsset<SpriteAtlas>("atlas");
        print(atlas);
    }

    public Image img;
    public Sprite[] sps = new Sprite[10];

    public void SetImg()
    {
        print(atlas.spriteCount);
        sps = new Sprite[atlas.spriteCount];
        var x=  atlas.GetSprites(sps);
        print(x);
        img.sprite = sps[2];
    }

    public void UnloadAtlasAB()
    {
        UnloadAB("atlas");
    }


    public AssetBundle LoadAB(string abName)
    {
        var path = "AssetBundles/" + folderName + "/" + abName;

        var ab = AssetBundle.LoadFromFile(path);
        if (ab != null)
        {
            var list = bundles.ToList();
            list.Add(ab);
            bundles = list.ToArray();
        }
        return ab;
    }

    public void UnloadAB(string name)
    {
        foreach (var ab in bundles)
        {
            if (ab!=null && ab.name == name)
                ab.Unload(true);
        }
    }


    public void InstGO()
    {
        var ab = LoadAB("prefab");
        var pref = ab.LoadAsset<GameObject>("uiprefab");
        var o = Instantiate(pref,p) ;
    }

    public void UnloadPrefabAB()
    {
        UnloadAB("prefab");
    }
}
