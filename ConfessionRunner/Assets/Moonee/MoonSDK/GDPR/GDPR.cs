using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Moonee.MoonSDK.Internal.GDPR
{
    public class GDPR : MonoBehaviour
    {
        public event Action OnCompleted;

        [SerializeField] private TextMeshProUGUI appName;
        [SerializeField] private Image appIcon;
        [SerializeField] private Button agreeButton;
        [SerializeField] private GameObject intro;
        public static bool IsAlreadyAsked => PlayerPrefs.GetInt(SaveName, 0) > 0;

        private const string SaveName = "GDPR";

        private void Start()
        {
       

            agreeButton.onClick.AddListener(OnAgreeButtonClicked);

            var text = String.Format(appName.text, Application.productName);
            appName.text = text;


            Ask();
        }
        public void Ask()
        {
            MoonSDKSettings settings = MoonSDKSettings.Load();

            if (IsAlreadyAsked || !CheckGDPRCountry.CheckCountryForGDPR())
            {
                gameObject.SetActive(false);
                intro.gameObject.SetActive(true);
                Destroy(this);
                return;
            }
            gameObject.SetActive(true);
        }
        public void OnAgreeButtonClicked()
        {
            OnCompleted?.Invoke();
            PlayerPrefs.SetInt(SaveName, 1);
            intro.gameObject.SetActive(true);
            Close();
        }
        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}