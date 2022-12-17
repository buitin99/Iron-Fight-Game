using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    // private float healthPlayer;
    // public int moneyCollected {get; private set;}
    // private PlayerData playerData;
    // public SettingData settingData;
    // private bool isWin, isEnd, bossFight;
    // private Coroutine coroutine;
    // public UnityEvent<float> OnUpdateHealthPlayer =  new UnityEvent<float>();
    // public UnityEvent<int, int> OnUpdateMoney =  new UnityEvent<int, int>();
    // public UnityEvent<Vector3> OnEnemyAlert =  new UnityEvent<Vector3>();
    // public UnityEvent OnEnemyAlertOff =  new UnityEvent();
    // public UnityEvent<bool> OnStart =  new UnityEvent<bool>();
    // public UnityEvent OnEndCutScene = new UnityEvent();
    // public UnityEvent OnPause =  new UnityEvent();
    // public UnityEvent OnResume =  new UnityEvent();
    // public UnityEvent<int> OnSelectItem =  new UnityEvent<int>();
    // public UnityEvent<int> OnBuyItem =  new UnityEvent<int>();
    // public UnityEvent<bool> OnEndGame = new UnityEvent<bool>();
    // // Start is called before the first frame update
    // protected override void Awake() {
    //     base.Awake();
    //     playerData = PlayerData.Load();
    //     settingData = SettingData.Load();
    // }
    // void Start()
    // {
    //     SetGraphics();
    //     InitGame();
    // }
    
    // public void UpdatePlayerHealth(float hp) {
    //     healthPlayer = hp;
    //     OnUpdateHealthPlayer?.Invoke(healthPlayer);
    // }

    // public void UpdateCurrency(int point, bool save = false) {
    //     if(isEnd) return;
    //     moneyCollected += point;
    //     playerData.money += point;
    //     OnUpdateMoney?.Invoke(playerData.money, moneyCollected);
    //     if(save) {
    //         playerData.Save();
    //     }
    // }

    // public void EnemyTriggerAlert(Vector3 pos, float time) {
    //     if(coroutine != null) {
    //         StopCoroutine(coroutine);
    //     }
    //     OnEnemyAlert?.Invoke(pos);
    //     coroutine = StartCoroutine(StartAlert(time));
    // }

    // public void SetBossFight() {
    //     bossFight = true;
    // }

    // public void EndCutScene() {
    //     OnEndCutScene?.Invoke();
    // }

    // public void StartGame() {
    //     OnStart?.Invoke(bossFight);
    // }
    // public void PauseGame() {
    //     OnPause?.Invoke();
    //     Time.timeScale = 0;
    // }

    // public void ResumeGame() {
    //     OnResume?.Invoke();
    //     Time.timeScale = 1;
    // }

    // public void UnlockNewLevel(int indexLevel) {
    //     List<int> list = playerData.levels;
    //     int index = list.IndexOf(indexLevel);
    //     if(index == -1) {
    //         playerData.levels.Add(indexLevel);
    //         playerData.Save();
    //     } else {
    //         playerData.levels.Remove(index);
    //         playerData.levels.Add(indexLevel);
    //         playerData.Save();
    //     }
    // }

    // public void InitGame() {
    //     bossFight = false;
    //     isEnd = false;
    //     moneyCollected = 0;
    //     playerData = PlayerData.Load();
    //     OnUpdateHealthPlayer?.Invoke(healthPlayer);
    //     OnUpdateMoney?.Invoke(playerData.money, moneyCollected);
    // }

    // public void EndGame(bool win) {
    //     isWin = win;
    //     OnEndGame?.Invoke(isWin);
    //     int bonus = isWin?100:0;
    //     UpdateCurrency(bonus);
    //     playerData.Save();
    // }

    // public bool BuyItem(int id, int price, TypeItem typeItem) {
    //     bool canBuy = playerData.money >= price;
    //     OnBuyItem?.Invoke(id);
    //     if(canBuy) {
    //         if(typeItem == TypeItem.Character) {
    //             if(playerData.characters.IndexOf(id) == -1) {
    //                 UpdateCurrency(-price);
    //                 playerData.characters.Add(id);
    //                 playerData.Save();
    //                 return true;
    //             }
    //         } else {
    //             if(playerData.weapons.IndexOf(id) == -1) {
    //                 UpdateCurrency(-price);
    //                 playerData.weapons.Add(id);
    //                 playerData.Save();
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    // }

    // public bool SelectItem(int id, TypeItem typeItem) {
    //     if(typeItem == TypeItem.Character) {
    //         if(playerData.characters.IndexOf(id) != -1) {
    //             playerData.selectedCharacter = id;
    //             playerData.Save();
    //             OnSelectItem?.Invoke(id);
    //             return true;
    //         }
    //     } else {
    //         if(playerData.weapons.IndexOf(id) != -1) {
    //             playerData.selectedWeapon = id;
    //             playerData.Save();
    //             OnSelectItem?.Invoke(id);
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // IEnumerator StartAlert(float time) {
    //     yield return new WaitForSeconds(time);
    //     OnEnemyAlertOff?.Invoke();
    // }

    // private void SetGraphics() {
    //     float resolutionScale = settingData.resolutionScale;
    //     float xx = Screen.width;
    //     float yy = Screen.height;
    //     int w = (int)(xx * resolutionScale);
    //     int h = (int)(yy * resolutionScale);
    //     Screen.SetResolution(w, h, true);
    //     Camera.main.aspect = xx / yy;
    //     Application.targetFrameRate = settingData.fps;
    // }

}
