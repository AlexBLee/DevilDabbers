using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetUIAnchors : EditorWindow
{
    private GameObject specified = null;
    

    [MenuItem("UI/Set Selected Anchors")]
    static void Do()
    {
        SetUIAnchors window = GetWindow<SetUIAnchors>();
    }

    private void OnGUI()
    {
        specified = EditorGUILayout.ObjectField("Object to set anchors:", specified, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();

        if(GUILayout.Button("Set only this object's Anchors") && specified != null)
        {
            SetSpecifiedUIAnchors(specified);
            specified = null;
        }
        EditorGUILayout.Space();


        if (GUILayout.Button("Set this and all Children's anchors") && specified != null)
        {
            SetUIAnchorsWithChildren(specified);
        }
        EditorGUILayout.Space();

    }

    public static void SetUIAnchorsWithChildren(GameObject obj)
    {
        SetSpecifiedUIAnchors(obj);

        for(int ii = 0; ii < obj.transform.childCount; ii++)
        {
            SetUIAnchorsWithChildren(obj.transform.GetChild(ii).gameObject);
        }
    }

    public static void SetSpecifiedUIAnchors(GameObject obj)
    {
        //GameObject obj = Selection.activeGameObject; // selected object
        RectTransform rt = null;
        RectTransform parent = null;

        if(obj != null)
        {
            rt = obj.GetComponent<RectTransform>();
            parent = obj.transform.parent.GetComponent<RectTransform>();
        }

        if(rt != null && parent != null)
        {
            Vector2 bottomLeft = new Vector2(
                ((parent.rect.width * 0.5f) - (rt.rect.width * 0.5f) + (rt.anchoredPosition.x)) / parent.rect.width,
                ((parent.rect.height * 0.5f) - (rt.rect.height * 0.5f) + (rt.anchoredPosition.y)) / parent.rect.height);

            Vector2 topRight = new Vector2(
                ((parent.rect.width * 0.5f) + (rt.rect.width * 0.5f) + (rt.anchoredPosition.x)) / parent.rect.width,
                ((parent.rect.height * 0.5f) + (rt.rect.height * 0.5f) + (rt.anchoredPosition.y)) / parent.rect.height);

            rt.anchorMin = bottomLeft;
            rt.anchorMax = topRight;

            rt.anchoredPosition = Vector2.zero;
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rt.sizeDelta.x);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rt.sizeDelta.y);

        }
        else
        {
            Debug.LogError("No object selected or selected object is not UI");
        }
    }
}
