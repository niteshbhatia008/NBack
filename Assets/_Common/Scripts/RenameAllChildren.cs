#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class RenameAllChildren : EditorWindow
{
    static EditorWindow window;
    string textField = "";
    
    [MenuItem("MyTools/RenameAllChildren")]
    static void ShowWindow()
    {
        window = EditorWindow.GetWindow(typeof(RenameAllChildren));
        window.position =new Rect(Screen.width*2, Screen.height/2, 300,100);
    }

    void OnGUI()
    {
        GUILayout.Space(25);
        
        EditorGUILayout.BeginHorizontal();
        
        GUILayout.Label( "ObjectName" );
        textField = GUILayout.TextField( textField , GUILayout.Width(window.position.width/1.5f));
       
        GUILayout.EndHorizontal();
        GUILayout.Space(25);
        
        if( GUILayout.Button( "Change all names of ChildrenObject" ) )
        {
            int num = 1;
            
            GameObject selectionObj = Selection.activeGameObject;

            for (int i = 0; i < selectionObj.transform.childCount;i++)
            {
                Transform childTransform = selectionObj.transform.GetChild(i);

                childTransform.name = textField + num;
                num++;
            }
        }
    }
}
#endif
