//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// General API class.
/// </summary>
public class CCDS {

    /// <summary>
    /// Default time scale.
    /// </summary>
    static float defaultTimeScale = -1f;

    /// <summary>
    /// Sets the player name.
    /// </summary>
    /// <param name="playerName"></param>
    public static void SetPlayerName(string playerName) {

        PlayerPrefs.SetString(CCDS_Settings.Instance.playerPrefsPlayerName, playerName);

    }

    /// <summary>
    /// Gets the player name.
    /// </summary>
    /// <returns></returns>
    public static string GetPlayerName() {

        return PlayerPrefs.GetString(CCDS_Settings.Instance.playerPrefsPlayerName, CCDS_Settings.Instance.defaultPlayerName);

    }

    /// <summary>
    /// Gets the money.
    /// </summary>
    /// <returns></returns>
    public static int GetMoney() {

        return PlayerPrefs.GetInt(CCDS_Settings.Instance.playerPrefsPlayerMoney, CCDS_Settings.Instance.defaultMoney);

    }

    /// <summary>
    /// Changes the player money. It can be positive or negative.
    /// </summary>
    /// <param name="amount"></param>
    public static void ChangeMoney(int amount) {

        PlayerPrefs.SetInt(CCDS_Settings.Instance.playerPrefsPlayerMoney, GetMoney() + amount);

    }

    /// <summary>
    /// Gets the latest selected player vehicle.
    /// </summary>
    /// <returns></returns>
    public static int GetVehicle() {

        UnlockVehicle(CCDS_Settings.Instance.defaultSelectedVehicleIndex);
        return PlayerPrefs.GetInt(CCDS_Settings.Instance.playerPrefsPlayerVehicle, CCDS_Settings.Instance.defaultSelectedVehicleIndex);

    }

    /// <summary>
    /// Sets the selected vehicle as player vehicle.
    /// </summary>
    /// <param name="vehicleIndex"></param>
    public static void SetVehicle(int vehicleIndex) {

        PlayerPrefs.SetInt(CCDS_Settings.Instance.playerPrefsPlayerVehicle, vehicleIndex);

    }

    /// <summary>
    ///  Unlocks the target vehicle.
    /// </summary>
    /// <param name="vehicleIndex"></param>
    public static void UnlockVehicle(int vehicleIndex) {

        PlayerPrefs.SetInt(CCDS_PlayerVehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<CCDS_Player>().vehicleSaveName, 1);

    }

    /// <summary>
    /// Deleting save key for the target vehicle.
    /// </summary>
    /// <param name="vehicleIndex"></param>
    public static void LockVehicle(int vehicleIndex) {

        PlayerPrefs.DeleteKey(CCDS_PlayerVehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<CCDS_Player>().vehicleSaveName);

    }

    /// <summary>
    /// Purchases and unlocks all vehicles.
    /// </summary>
    /// <param name="vehicleIndex"></param>
    public static void UnlockAllVehicles() {

        for (int i = 0; i < CCDS_PlayerVehicles.Instance.playerVehicles.Length; i++) {

            if (CCDS_PlayerVehicles.Instance.playerVehicles[i] != null)
                UnlockVehicle(i);

        }

    }

    /// <summary>
    /// Deleting save key for all vehicles.
    /// </summary>
    /// <param name="vehicleIndex"></param>
    public static void LockAllVehicles() {

        for (int i = 0; i < CCDS_PlayerVehicles.Instance.playerVehicles.Length; i++) {

            if (CCDS_PlayerVehicles.Instance.playerVehicles[i] != null)
                LockVehicle(i);

        }

    }

    /// <summary>
    /// Is this vehicle owned by the player?
    /// </summary>
    /// <param name="vehicleIndex"></param>
    /// <returns></returns>
    public static bool IsOwnedVehicle(int vehicleIndex) {

        if (PlayerPrefs.HasKey(CCDS_PlayerVehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<CCDS_Player>().vehicleSaveName))
            return true;
        else
            return false;

    }

    /// <summary>
    /// Sets the target scene to load.
    /// </summary>
    /// <param name="sceneIndex"></param>
    public static void SetScene(int sceneIndex) {

        PlayerPrefs.SetInt(CCDS_Settings.Instance.playerPrefsSelectedScene, sceneIndex);

    }

    /// <summary>
    /// Gets the latest selected scene.
    /// </summary>
    /// <returns></returns>
    public static int GetScene() {

        return PlayerPrefs.GetInt(CCDS_Settings.Instance.playerPrefsSelectedScene, CCDS_Settings.Instance.mainMenuSceneIndex);

    }

    /// <summary>
    /// Loads the latest selected scene.
    /// </summary>
    public static void StartGameplayScene() {

        SceneManager.LoadScene(GetScene());

    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public static void PauseGame() {

        if (defaultTimeScale == -1)
            defaultTimeScale = Time.timeScale;

        Time.timeScale = 0;
        AudioListener.pause = true;
        CCDS_Events.Event_OnPaused();

    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public static void ResumeGame() {

        if (defaultTimeScale == -1)
            defaultTimeScale = Time.timeScale;

        Time.timeScale = defaultTimeScale;

        AudioListener.pause = false;
        AudioListener.volume = GetAudioVolume();

        CCDS_Events.Event_OnResumed();

    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public static void RestartGame() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    /// <summary>
    /// Back to the main menu.
    /// </summary>
    public static void MainMenu() {

        SceneManager.LoadScene(0);
        CCDS_Events.Event_OnMainMenu();

    }

    /// <summary>
    /// Sets the volume of the audiolistener.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetAudioVolume(float volume) {

        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("AudioVolume", volume);

    }

    /// <summary>
    /// Sets the music volume.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetMusicVolume(float volume) {

        PlayerPrefs.SetFloat("MusicVolume", volume);

    }

    /// <summary>
    /// Get volume of the audiolistener.
    /// </summary>
    /// <returns></returns>
    public static float GetAudioVolume() {

        return PlayerPrefs.GetFloat(CCDS_Settings.Instance.playerPrefsAudioVolume, CCDS_Settings.Instance.defaultAudioVolume);

    }

    /// <summary>
    /// Get volume of the music.
    /// </summary>
    /// <returns></returns>
    public static float GetMusicVolume() {

        return PlayerPrefs.GetFloat(CCDS_Settings.Instance.playerPrefsMusicVolume, CCDS_Settings.Instance.defaultMusicVolume);

    }

    /// <summary>
    /// Disables the traffic for a while.
    /// </summary>
    public static void DisableTrafficForAWhile() {

        RTC_SceneManager sceneManager = RTC_SceneManager.Instance;

        if (!sceneManager) {

            Debug.LogError("RTC_SceneManager couldn't found in the scene to enable or disable the traffic.");
            return;

        }

        if (sceneManager.allVehicles != null) {

            for (int i = 0; i < sceneManager.allVehicles.Length; i++) {

                if (sceneManager.allVehicles[i] != null)
                    sceneManager.allVehicles[i].gameObject.SetActive(false);

            }

        }

    }

    /// <summary>
    /// Is this the first gameplay?
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstGameplay() {

        return !PlayerPrefs.HasKey(CCDS_Settings.Instance.playerPrefsPlayerName);

    }

    /// <summary>
    /// Resets the game by deleting the save data and reloading the scene.
    /// </summary>
    public static void ResetGame() {

        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }

}
