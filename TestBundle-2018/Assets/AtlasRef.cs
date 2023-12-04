using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasRef : MonoBehaviour
{
    public SpriteAtlas atlas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_EDITOR

    [UnityEditor.MenuItem("Tools/ClearCache")]
    static void ClearCache()
    {
        Caching.ClearCache();
    }
#  endif
}
