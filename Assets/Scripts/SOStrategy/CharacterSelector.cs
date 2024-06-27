using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public StrategySO[] currentStrategy = new StrategySO[3];
    public Image[] SpriteSelectors = new Image[6];
    public Image[] SpriteSelections = new Image[3];
    public Sprite DefaultProfile;
    public Button playButton;
    public bool isGame = false;
    public CharacterInfoSO characterInfo;
    public Test ControllerInput;
    public int count = 0;
    private void Start()
    {
        if (!isGame) return;

        for (int i = 0; i < SpriteSelections.Length; i++)
        {
            int localIndex = i;
            SpriteSelections[localIndex].sprite = characterInfo.selectedWarriors[localIndex].GetProfilePicture();
            SpriteSelections[localIndex].gameObject.GetComponent<Button>().onClick.AddListener(() => UpdateChracterSO(characterInfo.selectedWarriors[localIndex]));
        }

        characterInfo.LoadInformation();
        ControllerInput.SetWarriorPrefab(characterInfo.StrategySODefault);
    }
    public void SetWarrior(StrategySO strategySO)
    {
        if (IsHeroAlreadySelected(strategySO))
            return;

        for (int i = 0; i < currentStrategy.Length; i++)
        {
            if (currentStrategy[i] == null)
            {
                currentStrategy[i] = strategySO;
                SpriteSelections[i].sprite = strategySO.GetProfilePicture();

                count++;
                if (count == 3)
                {
                    playButton.interactable = true;
                    characterInfo.selectedWarriors = currentStrategy;
                }
                break;
            }
        }
    }
    public void DeselectWarrior(int index)
    {
        if (currentStrategy[index] != null)
        {
            currentStrategy[index] = null;
            SpriteSelections[index].sprite = DefaultProfile;
            count--;
            if (count < 3)
            {
                playButton.interactable = false;
            }
        }
    }
    // Metodo auxiliar para verificar si el heroe ya esta seleccionado
    private bool IsHeroAlreadySelected(StrategySO strategySO)
    {
        for (int i = 0; i < currentStrategy.Length; i++)
        {
            if (currentStrategy[i] == strategySO)
            {
                return true;
            }
        }
        return false;
    }
    // Metodo para actualizar el objetivo a spawnear
    public void UpdateChracterSO(StrategySO current)
    {
        ControllerInput.SetWarriorPrefab(current);
    }
}
