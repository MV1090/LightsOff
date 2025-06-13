using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    public static Authentication Instance { get; private set; }
    public string PlayerDisplayName { get; private set; }

    public static event Action<string> OnPlayerNameReady;

    private const string CachedNameKey = "CachedPlayerName";

    private bool wasOffline;

    async void Awake()
    {
        Instance = this;

        try
        {
            await UnityServices.InitializeAsync();
            await SignUpAnonymouslyAsync();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }        
    }

    async Task SignUpAnonymouslyAsync()
    {
        try
        {
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var playerInfo = await AuthenticationService.Instance.GetPlayerInfoAsync();
            string serverName = playerInfo?.Username;
            string localName = PlayerPrefs.GetString(CachedNameKey, null);

            if (string.IsNullOrEmpty(serverName) && string.IsNullOrEmpty(localName))
            {
                string newName = NameGenerator.Instance.RandomName();
                await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
                PlayerPrefs.SetString(CachedNameKey, newName);
                PlayerPrefs.Save();
                PlayerDisplayName = newName;                
            }
            else
            {
                PlayerDisplayName = !string.IsNullOrEmpty(serverName) ? serverName : localName;                
                if (string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(localName))
                    await AuthenticationService.Instance.UpdatePlayerNameAsync(localName);
            }

            OnPlayerNameReady?.Invoke(PlayerDisplayName);

            ScoreCacheManager.Instance.SyncScoresAfterLogin();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void Update()
    {
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        if (isConnected && wasOffline)
        {            
            _= SignUpAnonymouslyAsync();            
        }

        wasOffline = !isConnected;
    }
    
}