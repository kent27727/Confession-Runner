using GameAnalyticsSDK;
using UnityEngine;

namespace Moonee.MoonSDK.Internal.Analytics
{
    public class GameAnalyticsManager : MonoBehaviour, IGameAnalyticsATTListener
    {
        [SerializeField] private GameObject gameAnalyticsGameObject;
        void Start()
        {
            MoonSDKSettings settings = MoonSDKSettings.Load();

            if (settings.GameAnalytics == false)
            {
                Destroy(gameAnalyticsGameObject);
                Destroy(this);
            }
            //var gameAnalyticsComponent = Object.FindObjectOfType<GameAnalytics>();
            //if (gameAnalyticsComponent == null)
            //{
            //    var gameAnalyticsGameObject = new GameObject("GameAnalytics");
            //    gameAnalyticsGameObject.AddComponent<GameAnalytics>();
            //    gameAnalyticsGameObject.SetActive(true);
            //}
            //else
            //{
            //    gameAnalyticsComponent.gameObject.name = "GameAnalytics";
            //}

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                GameAnalytics.RequestTrackingAuthorization(this);
            }
            else
            {
                GameAnalytics.Initialize();
            }
        }
        public void GameAnalyticsATTListenerNotDetermined()
        {
            GameAnalytics.Initialize();
        }
        public void GameAnalyticsATTListenerRestricted()
        {
            GameAnalytics.Initialize();
        }
        public void GameAnalyticsATTListenerDenied()
        {
            GameAnalytics.Initialize();
        }
        public void GameAnalyticsATTListenerAuthorized()
        {
            GameAnalytics.Initialize();
        }
    }
}