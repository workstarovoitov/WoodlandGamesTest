using UnityEngine;
using UnityEngine.UI;
namespace MenuManagement
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;
        public static T Instance { get { return _instance; } }

        public StarterAssetsControls actions;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
                actions = new StarterAssetsControls();
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
        
        public static void Initialize()
        {
            if (MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.InitializeMenu(Instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] public Button selectedButton;

        public bool IsMobileDevice()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                return true;
            }
            return false;
        }

        public virtual void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
        public virtual void InitializeMenuData() { }

        public abstract void ShowMenu();

    }
}