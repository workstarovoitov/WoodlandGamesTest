using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace MenuManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }

        private void Start()
        {
            actions.Menu.Pause.performed += _ => OnResumePressed();
            actions.Menu.Back.performed += _ => OnResumePressed();
        }

        public override void ShowMenu()
        {
            if (selectedButton) selectedButton.Select();
        }

        public void OnResumePressed()
        {
            OnBackPressed();
        }
        public void OnRestartPressed()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameMenu.Initialize();
            base.OnBackPressed();
        }

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public override void OnBackPressed()
        {
            Time.timeScale = 1f;
            base.OnBackPressed();
        }
    }
}