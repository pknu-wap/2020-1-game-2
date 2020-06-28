using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class Finish : MonoBehaviour
    {
        [Header("게임 화면 씬")] public string PlaySceneName;

        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(PlaySceneName);
        }
    }
}