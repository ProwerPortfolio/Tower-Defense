// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Главное меню.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private Button continueButton;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            continueButton.interactable = File.Exists($"{Application.persistentDataPath}/{MapCompletion.fileName}");
        }

        #endregion

        #region Public API

        public void NewGame()
        {
            MapCompletion.ResetSavedData();
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }

        #endregion

        #endregion
    }
}