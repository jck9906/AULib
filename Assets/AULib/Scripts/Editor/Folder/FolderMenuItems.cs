using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace AULib
{
    public class FolderMenuItems
    {
        [MenuItem("AULib/Tools/Folder/OpenDataPath", false, 400)]
        public static void OpenDataPath()
        {
            Process.Start(Application.dataPath);
        }

        [MenuItem("AULib/Tools/Folder/OpenPersistentDataPath", false, 401)]
        public static void OpenPersistentDataPath()
        {
            Process.Start(Application.persistentDataPath);
        }

        [MenuItem("AULib/Tools/Folder/OpenStreamingAssetPath", false, 402)]
        public static void OpenStreamingAssetPath()
        {
            Process.Start(Application.streamingAssetsPath);
        }

        [MenuItem("AULib/Tools/Folder/OpenPatchPath", false, 403)]
        public static void OpenPatchFolder()
        {
            string strPath = Application.persistentDataPath + "/temp";

            if (false == File.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            strPath = strPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            Process.Start("explorer.exe", "/select," + strPath);
            //Debug.LogError(strPath);
        }

    }


}
