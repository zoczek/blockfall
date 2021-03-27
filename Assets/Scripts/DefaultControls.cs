using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DefaultControls
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>
    {
        {"left", KeyCode.LeftArrow },
        {"right", KeyCode.RightArrow},
        {"rotate", KeyCode.Space }
    };

}
