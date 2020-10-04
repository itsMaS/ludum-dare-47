using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void MoveDinamic(this Transform from, Vector2 to, float speed, Space space = Space.World)
    {
        if(space == Space.World)
            from.position = Vector2.MoveTowards(from.position, to,
                ((Vector2.Distance(from.position, to)*speed)+0.001f)*Time.deltaTime);
        else
            from.localPosition = Vector2.MoveTowards(from.position, to,
                ((Vector2.Distance(from.position, to) * speed) + 0.001f) * Time.deltaTime);
    }
    public static Vector2 PerpendicularClockwise(this Vector2 vector2)
    {
        return new Vector2(vector2.x, -vector2.x);
    }
}
