using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace MenuManagement
{
    public class GameMenuPC : GameMenu
    {
        public static event Action OnGameStarted;
        public static event Action OnGamePaused;

        [SerializeField] private Text toothesAmount;
        private int toothesOnLevel;
        private int toothesCollected;


        [SerializeField] private Text time;
        
        [SerializeField] private GameObject[] hearts;

        [Header("WinPopup")]
        [SerializeField] private GameObject winPopup;
        [SerializeField] private Button nextButton;
        [SerializeField] private Text levelWinLabel;

        [Header("FailPopup")]
        [SerializeField] private GameObject failPopup;
        [SerializeField] private Text levelFailLabel;


        private GameObject activeHero;
        private int healthMax = 0;
        private int healthCurrent = 0;
        private float startTime;
        private float addTime;
        private float currentTime;

        private bool popupOpened = false;

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
            Tooth.OnCollectTooth += ToothCounterUpdate;
            HealthController.OnHealthChanged += HealthUpdate;
            actions.Menu.Pause.performed += _ => OnPausePressed();
            nextButton.interactable = true;

            failPopup.SetActive(false);
            winPopup.SetActive(false);
        }

        public override void ShowMenu() 
        {
            Time.timeScale = 1;

            OnGameStarted?.Invoke();
            popupOpened = false;
            winPopup.SetActive(false);
            failPopup.SetActive(false);
        }
 
        public override void InitializeMenuData()
        {
            activeHero = GameObject.FindGameObjectWithTag("Player");
            if (!activeHero) StartCoroutine(DelayOnStartScene());
            startTime = Time.time;
            toothesCollected = 0;
            toothesOnLevel = GameObject.FindGameObjectsWithTag("Tooth").Length;
            toothesAmount.text = toothesCollected.ToString() + "/" + toothesOnLevel.ToString();
            HealthUpdate();
        }

        public void ShowPlayerHealth()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].transform.GetChild(0).gameObject.SetActive(false);
                hearts[i].SetActive(false);
                
                if (i < healthMax)
                {
                    hearts[i].SetActive(true);
                }
                if (i < healthCurrent)
                {
                    hearts[i].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }


        private void HealthUpdate()
        {
            healthCurrent = activeHero.GetComponent<HealthController>().HealthCurrent;
            healthMax = activeHero.GetComponent<HealthController>().HealthMax;
            if (healthCurrent >= 6) healthCurrent = 6;
            if (healthMax >= 6) healthMax = 6; 
            ShowPlayerHealth();

            if (healthCurrent <= 0)
            {
                LevelFail();
            }
        }

        private void Update()
        {
            if (!activeHero) return;

            currentTime = Time.time - startTime + addTime;

            int minutes = (int)(currentTime / 60f);
            int seconds = (int)(currentTime % 60f);
            int milliseconds = (int)(currentTime * 1000f) % 1000;
            time.text = minutes.ToString() + ":" + seconds.ToString("D2") + "." + milliseconds.ToString("D3");
        }
        
        private void ToothCounterUpdate()
        {
            toothesCollected ++;
            toothesAmount.text = toothesCollected.ToString() + "/" + toothesOnLevel.ToString();


            if (toothesCollected >= toothesOnLevel)
            {
                LevelWin();
            }
        }

        public void OnPausePressed()
        {
            if (popupOpened) return;
            OnGamePaused?.Invoke();
            Time.timeScale = 0;
            PauseMenu.Open();
        }
        
        public void LevelWin()
        {
            popupOpened = true;
            winPopup.SetActive(true);
            levelWinLabel.text = "Level " + (GetComponent<LevelController>().LevelCurrent + 1).ToString();

            OnGamePaused?.Invoke();
            Time.timeScale = 0;

            if (GetComponent<LevelController>().LevelCurrent >= GetComponent<LevelController>().LevelsMax - 1) nextButton.interactable = false;
            if (selectedButton && selectedButton.isActiveAndEnabled) selectedButton.Select();
        }

        public void LevelFail()
        {
            popupOpened = true;
            failPopup.SetActive(true);
            levelFailLabel.text = "Level " + (GetComponent<LevelController>().LevelCurrent + 1).ToString();

            OnGamePaused?.Invoke();
            Time.timeScale = 0;
        }

        public void OnNextPressed()
        {
            GetComponent<LevelController>().LevelCurrent++;
            popupOpened = false;
            winPopup.SetActive(false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            StartCoroutine(DelayOnStartScene());
        }

        public void OnRestartPressed()
        {
            popupOpened = false;
            winPopup.SetActive(false);
            failPopup.SetActive(false);


            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            StartCoroutine(DelayOnStartScene());
        }

        public void OnMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }

        IEnumerator DelayOnStartScene()
        {
            yield return new WaitForSeconds(0.5f);
            Initialize();
        }
    }
}
