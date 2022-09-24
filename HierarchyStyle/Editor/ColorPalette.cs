using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ColorDesign
{
    [Tooltip("在名称前添加唯一字符")] public string keyChar;

    [Tooltip("文字颜色，Alpha为255")] public Color textColor;

    [Tooltip("背景颜色，Alpha为255")] public Color backgroundColor;

    [Tooltip("设置文字对齐")] public TextAnchor textAlignment;

    [Tooltip("设置文字样式")] public FontStyle fontStyle;

    [Tooltip("设置名称显示是否大写")] public bool isToUpper;
}

public class ColorPalette : ScriptableObject
{
    public List<ColorDesign> colorDesigns = new List<ColorDesign>();
}