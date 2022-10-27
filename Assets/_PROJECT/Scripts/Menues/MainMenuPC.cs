using System.Collections;
using UnityEngine;

namespace MenuManagement
{
    public class MainMenuPC : MainMenu
    {
        public override void ShowMenu()
        {
            Time.timeScale = 0;
            if (selectedButton) selectedButton.Select();
        }

        public void OnPlayPressed()
        {
            GameMenu.Open();
            GameMenu.Initialize();
        }
        
        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }
        
        public void OnCreditsPressed()
        {
            Application.OpenURL("https://www.woodland.games/about");
        }


        public override void OnBackPressed()
        {
            Application.Quit();
        }

    }
}