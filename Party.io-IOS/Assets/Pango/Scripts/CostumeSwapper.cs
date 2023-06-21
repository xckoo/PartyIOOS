﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class CostumeSwapper
{
    public static void SwapValues<T>(this T[] source, int index1, int index2)
    {
        T temp = source[index1];
        source[index1] = source[index2];
        source[index2] = temp;
    }


}