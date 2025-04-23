using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        SetupEvents();

        await SignUpAnonymouslyAsync();
    }
     

    /// <summary>
    /// To be used if we want to run events on sign in and sign out.
    /// </summary>
    /// <returns></returns>
    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
           
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
        };

        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

    async Task SignUpAnonymouslyAsync()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");

                string currentName = AuthenticationService.Instance.PlayerName;

                while(currentName.Length>15)
                {
                    string newName = NameGenerator.Instance.RandomName();
                    try
                    {
                        await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
                        Debug.Log($"Assigned new player name: {newName}");
                    }
                    catch (RequestFailedException ex)
                    {
                        Debug.LogError("Failed to update player name.");
                        Debug.LogException(ex);
                    }
                    currentName = newName;
                }

                Debug.Log($"Player name: {AuthenticationService.Instance.PlayerName}");
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            }

            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }
        
    }
}
