using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine.UI;

public class EditorExWindow : EditorWindow
{
    [MenuItem("Window/Editor")]
    static void Open()
    {
        EditorWindow.GetWindow<EditorExWindow>("Editor");
    }

    int leftSize = 0;
    Vector2 leftScrollPos = Vector2.zero;
    int rightSize = 0;
    Vector2 rightScrollPos = Vector2.zero;

    Dictionary<int, Sprite> SpriteDictonary = new Dictionary<int, Sprite>();
    string row = "";
    string col = "";
    void OnGUI()
    {
        EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！");
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(300));
        EditorGUILayout.LabelField("MapChip");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Import"))
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("MapChip/tileb");
            for (int i = 0; i < sprites.Length; i++)
            {
                SpriteDictonary.Add(i, sprites[i]);
            }
        }
        if (GUILayout.Button("Delete"))
        {

        }
        EditorGUILayout.EndHorizontal();
        leftScrollPos = EditorGUILayout.BeginScrollView(leftScrollPos, GUI.skin.box);
        {
            // スクロール範囲
            for (int i = 0; i < leftSize; i++)
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                EditorGUILayout.LabelField("Index " + i);
                GUILayout.Button(SpriteDictonary[i].texture, GUILayout.Width(30), GUILayout.Height(30));
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        /***********************************************************************************************/

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("フィールド");
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Size", GUILayout.Width(60));
            row = GUILayout.TextField(row, GUILayout.Width(30));
            GUILayout.Label("x", GUILayout.Width(10));
            col = GUILayout.TextField(col, GUILayout.Width(30));
            if (GUILayout.Button("セット", GUILayout.Width(60)))
            {
                rightSize = int.Parse(col);
            }
        }
        EditorGUILayout.EndHorizontal();
        rightScrollPos = EditorGUILayout.BeginScrollView(rightScrollPos, GUI.skin.box);
        {
            for (int y = 0; y < int.Parse(row); y++)
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    // ここの範囲は横並び
                    EditorGUILayout.PrefixLabel("Index " + y);
                    // 下に行くほどボタン数増やす
                    for (int i = 0; i < int.Parse(col); i++)
                    {
                        // ボタン(横幅100px)
                        if (GUILayout.Button(i.ToString(), GUILayout.Width(30), GUILayout.Height(30)))
                        {
                            Debug.Log("Button" + i + "押したよ");
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            // こんな感じで横幅固定しなくても、範囲からはみ出すときにスクロールバー出してくれる。
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
}