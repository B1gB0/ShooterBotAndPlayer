using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : AbstractHealth
{
    internal override void Die()
    {
        Destroy(gameObject);
    }
}
