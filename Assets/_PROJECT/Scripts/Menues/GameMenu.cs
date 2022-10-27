using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MenuManagement
{
    public abstract class GameMenu : Menu<GameMenu>
    {
        public override void ShowMenu() {}
    }
}
