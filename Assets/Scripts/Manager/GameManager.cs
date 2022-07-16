using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ByteLoop.Manager
{
    public class GameManager : PersistentMonoSingleton<GameManager>
    {
        public int HighScore = 0;
        public bool IsPaused = false;
        public bool InputAllowed = true;
        public GameObject TimerGO;
        private void Start() {
            // SceneController.Instance.LoadScene("L1",LoadSceneMode.Additive);
        }
    }

}