using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class StartScene : MonoBehaviour
    {
        [Header("게임 화면 씬")] public string PlaySceneName;

        public void StartGame()
        {
            SceneManager.LoadScene(PlaySceneName);
            Time.timeScale = 1f;
            PauseMenu.isPause = false;
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("게임종료");
        }

        void Start()
        {

        }


        void Update()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}