using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Image Image;
    public SpriteAtlas sp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("卸载AtlastTexture")]
    public void TestUnloadAtlasTex()
    {
        var tex = Image.sprite.texture;
        print(tex);
        Resources.UnloadAsset(tex);
    }

    [ContextMenu("卸载atlas")]
    public void TestUnloadSprite()
    {
        print(Image.sprite);
        Resources.UnloadAsset(Image.sprite);
    }

    public void TestUnloadAtlas()
    {


    }

}
