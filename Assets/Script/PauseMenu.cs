using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("시작 화면 씬")] public string StartSceneName;
        [Header("일시정지 캔버스")] public GameObject PauseMenuCanvas;
        public static bool isPause = false; //메뉴 호출 시 true

        public void BackToMenu()
        {
            SceneManager.LoadScene(StartSceneName);
        }

        void Start()
        {

        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause)
                    CallMenu();
                else
                    CloseMenu();
            }
        }

        private void CallMenu()
        {
            isPause = true;
            PauseMenuCanvas.SetActive(true);
        }

        private void CloseMenu()
        {
            isPause = false;
            PauseMenuCanvas.SetActive(false);
        }
    }
}
