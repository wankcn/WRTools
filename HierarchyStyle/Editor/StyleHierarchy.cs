using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public class HierarchyStyle
{
    // 拿到资源中的所有调色板ColorPalette
    private static string[] dataArray;

    // 获得调色盘里的SO路径
    private static string path;
    private static ColorPalette colorPalette;

    /// <summary>
    /// 初始化
    /// </summary>
    [MenuItem("WRTools/应用HierarchyStyle")]
    private static void InitHierarchyStyle()
    {
        // 点击ok返回true，点击cancel返回false；
        bool res = EditorUtility.DisplayDialog("一个没得感情的HierarchyStyle",
            "点击确认后将会应用HierarchyStyle,仅只需要在第一次进行初始化，之后的任何样式修改将会在Hierarchy实时自动生效。", "好的", "哒咩");
        if (res) DrawGUI();
    }


    /// <summary>
    /// 实时绘制
    /// </summary>
    static HierarchyStyle()
    {
        DrawGUI();
    }


    private static void DrawGUI()
    {
        dataArray = AssetDatabase.FindAssets("t:ColorPalette");

        if (dataArray.Length >= 1)
        {
            // 只有一个调色板，使用dataArray[0]获取文件的路径
            path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            colorPalette = AssetDatabase.LoadAssetAtPath<ColorPalette>(path);
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
        }
    }

    private static void OnHierarchyWindow(int instanceID, Rect selectionRect)
    {
        // 需要确保第一次导入时不会出错 
        if (dataArray.Length == 0) return;
        Object instance = EditorUtility.InstanceIDToObject(instanceID);

        if (instance != null)
        {
            for (int i = 0; i < colorPalette.colorDesigns.Count; i++)
            {
                var design = colorPalette.colorDesigns[i];

                // 检查每个游戏对象的名称是否在colorDesigns列表中并且以keyChar开头。
                if (instance.name.StartsWith(design.keyChar))
                {
                    // 从名称中删除符号
                    string newName = instance.name.Substring(design.keyChar.Length);
                    // 绘制一个矩形背景并添加颜色
                    EditorGUI.DrawRect(selectionRect, design.backgroundColor);
                    // 新建一个GUIStyle
                    GUIStyle newStyle = new GUIStyle
                    {
                        alignment = design.textAlignment,
                        fontStyle = design.fontStyle,
                        normal = new GUIStyleState()
                        {
                            textColor = design.textColor,
                        }
                    };

                    var showName = design.isToUpper ? newName.ToUpper() : newName;
                    EditorGUI.LabelField(selectionRect, showName, newStyle);
                }
            }
        }
    }
}