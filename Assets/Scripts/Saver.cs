// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System;
using System.IO;
using UnityEngine;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Сохраняет данные на жёсткий диск.
    /// </summary>
    [Serializable]
    public class Saver<T>
    {
        #region Parameters

        public T data;

        #endregion

        #region API

        private static string Path(string fileName)
        {
            return $"{Application.persistentDataPath}/{fileName}";
        }

        #region Unity API

        

        #endregion

        #region Public API

        public static void Save(string fileName, T data)
        {
            var wrapper = new Saver<T> { data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            
            File.WriteAllText(Path(fileName), dataString);
        }

        public static void TryLoad(string fileName, ref T data)
        {
            var path = Path(fileName);

            if (File.Exists(path))
            {
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.data;
            }
        }

        public static void Reset(string fileName)
        {
            var path = Path(fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        #endregion

        #endregion
    }
}