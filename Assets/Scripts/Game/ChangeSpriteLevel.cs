using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteLevel : MonoBehaviour
{
    public Image   sprite1, sprite2, sprite3, sprite4, sprite5;
    public ScriptableSpriteLevelObject  scriptableSpriteLevelObject;
    private GameDatas   gameDatas;
    private GameManager gameManager;

    private void Awake() 
    {
        gameDatas = GameDatas.LoadData();
        gameManager = GameManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.OnEndGame.RemoveListener(EndGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameDatas = GameDatas.LoadData();
        if (gameDatas.LastestLevel == 1)
        {
            sprite1.sprite = scriptableSpriteLevelObject.spriteLevels[0].levelSprite;
            sprite2.sprite = scriptableSpriteLevelObject.spriteLevels[1].levelSprite;
            sprite3.sprite = scriptableSpriteLevelObject.spriteLevels[2].levelSprite;
            sprite4.sprite = scriptableSpriteLevelObject.spriteLevels[3].levelSprite;
            sprite5.sprite = scriptableSpriteLevelObject.spriteLevels[4].levelSprite;

        }
        if (gameDatas.LastestLevel > 1)
        {
            sprite1.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel-1].levelSprite;
            sprite2.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel].levelSprite;
            sprite3.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+1].levelSprite;
            sprite4.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+2].levelSprite;
            sprite5.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+3].levelSprite;
        }
        Debug.Log(gameDatas.LastestLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndGame(bool isWin)
    {
        gameDatas = GameDatas.LoadData();
        sprite1.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel].levelSprite;
        sprite2.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+1].levelSprite;
        sprite3.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+2].levelSprite;
        sprite4.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+3].levelSprite;
        sprite5.sprite = scriptableSpriteLevelObject.spriteLevels[gameDatas.LastestLevel+4].levelSprite;
    }

    private void OnDisable() 
    {
        gameManager.OnEndGame.AddListener(EndGame);
    }
}
