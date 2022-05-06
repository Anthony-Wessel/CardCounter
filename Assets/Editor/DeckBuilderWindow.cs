using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DeckBuilderWindow : EditorWindow
{
    DeckOptions currentDeckOptions;
    SerializedObject deckOptions;
    static DeckBuilderWindow thisWindow;
    static Scene rendereringScene;

    [MenuItem("Window/Deck Builder")]
    public static void ShowWindow()
    {
        thisWindow = (DeckBuilderWindow) GetWindow(typeof(DeckBuilderWindow));
        thisWindow.SetupOptions();
        rendereringScene = EditorSceneManager.OpenScene("Assets/Scenes/DeckRenderer.unity", OpenSceneMode.Additive);
    }

    private void OnDestroy()
    {
        EditorSceneManager.CloseScene(rendereringScene, true);
    }

    void SetupOptions()
    {
        currentDeckOptions = Resources.Load<DeckOptions>("ActiveDeckOptions");
        deckOptions = new SerializedObject(currentDeckOptions);
    }

    private void OnGUI()
    {
        if (deckOptions == null) SetupOptions();
        GUILayoutOption[] empty = new GUILayoutOption[]{};
            

        EditorGUILayout.PropertyField(deckOptions.FindProperty("suits"), empty);
        EditorGUILayout.PropertyField(deckOptions.FindProperty("cardTemplates"), empty);

        EditorGUILayout.PropertyField(deckOptions.FindProperty("cardTexture"), empty);
        EditorGUILayout.PropertyField(deckOptions.FindProperty("cardBackTexture"), empty);

        EditorGUILayout.PropertyField(deckOptions.FindProperty("numbers"), empty);

        EditorGUILayout.PropertyField(deckOptions.FindProperty("useCourt"), empty);

        if (GUILayout.Button("Build Deck", empty)) BuildDeck();
        if (GUILayout.Button("Render Deck", empty)) FindObjectOfType<DeckRenderer>().RenderToTexture();

        deckOptions.ApplyModifiedProperties();
    }

    public static void BuildDeck()
    {
        DeckRenderer dr = FindObjectOfType<DeckRenderer>();
        DeckGenerator dg = FindObjectOfType<DeckGenerator>();
        dg.GenerateDeck(thisWindow.currentDeckOptions, dr);
    }
}
