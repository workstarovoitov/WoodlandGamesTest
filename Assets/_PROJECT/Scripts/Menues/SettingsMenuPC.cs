using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuManagement
{
    public class SettingsMenuPC : SettingsMenu
    {
        [SerializeField] private GameObject[] pages;
        private int currentPageNum = 0;

        public void Start()
        {
            actions.Menu.Back.performed += _ => OnBackPressed();

        }
        public void ChangePage(int num)
        {
            currentPageNum += num;
            currentPageNum = (currentPageNum + pages.Length) % pages.Length;
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            pages[currentPageNum].SetActive(true);
        }

        public override void ShowMenu()
        {
            currentPageNum = 0;
            ChangePage(0);
            if (selectedButton) selectedButton.Select();

        }

    }
}