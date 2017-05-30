using UnityEngine;
using System.Collections;

public class SpriteHost : MonoBehaviour {

    // Similar to the ImageHost class in TSSE, this contains all the images
    // and returns the requested one when needed
    // Technically this is the correctly named one, cause we used Sprites in TSSE too

    public Sprite tile01;
    public Sprite tile02;
    public Sprite tile03;
    public Sprite tile04;
    public Sprite tile05;

    public Sprite flammenLL;
    public Sprite flammenLR;
    public Sprite flammenUL;
    public Sprite flammenUR;

    public Sprite getTile(int i)
    {
        Sprite retSprite = tile01;
        switch(i)
        {
            case 1:
                retSprite = tile01;
                break;
            case 2:
                retSprite = tile02;
                break;
            case 3:
                retSprite = tile03;
                break;
            case 4:
                retSprite = tile04;
                break;
            case 5:
                retSprite = tile05;
                break;
        }
        return retSprite;
    }

    public Sprite getFlammenGivenDir(Unit.Direction dir)
    {
        Sprite retSprite = flammenLL;
        switch(dir)
        {
            case Unit.Direction.LLeft:
                retSprite = flammenLL;
                break;
            case Unit.Direction.LRight:
                retSprite = flammenLR;
                break;
            case Unit.Direction.ULeft:
                retSprite = flammenUL;
                break;
            case Unit.Direction.URight:
                retSprite = flammenUR;
                break;
        }
        return retSprite;
    }

    public Sprite getUnitSprite(string sprite, Unit.Direction dir)
    {
        Sprite retSprite = flammenLL;
        if(sprite.Equals("flammen"))
        {
            retSprite = getFlammenGivenDir(dir);
        }
        return retSprite;
    }
}
