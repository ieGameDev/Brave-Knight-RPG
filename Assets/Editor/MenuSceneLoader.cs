using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public class MenuSceneLoader : MonoBehaviour
    {
        private const string MenuItemName = "\ud83c\udfae ieGameDev/Scenes";

        [MenuItem(MenuItemName + "/Initial", priority = 1000)]
        public static void LoadInitia() =>
            TryLoadScene(0);

        [MenuItem(MenuItemName + "/TestLevel", priority = 1001)]
        public static void LoadMain() =>
            TryLoadScene(1);

        private static void TryLoadScene(int index)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[index].path);
        }
    }
}