using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWaveManager : MonoBehaviour
{
    [SerializeField] Text _waveText = default;
    [SerializeField] Text _nextWaveText = default;

    [Header("Visual settings")]
    [SerializeField] Color[] _textColor = default;
    [SerializeField] Sprite[] _enemyImage = default;

    [Header("Gameobjects to change")]
    [SerializeField] Image[] _enemyImageConponenet = default;
    [SerializeField] Text[] _enemyAmountText = default;

    public void NextWaveCountdown(float timer)
    {
        _nextWaveText.text = timer.ToString($"Next Wave {0.0f}");
    }

    public void ApplyArtOnScreen(int wave, float enemy1Amount,
        float enemy2Amount, float enemy3Amount, float enemy4Amount, float enemy5Amount)
    {
        _waveText.text = $"Wave {wave++}";

        float[] _enemyAmount = new float[] {enemy1Amount, enemy2Amount,
            enemy3Amount, enemy4Amount, enemy5Amount };

        for (int i = 0; i < _enemyAmount.Length; i++)
        {
            if (_enemyAmount[i] != 0)
            {
                _enemyAmountText[i].text = $"{_enemyAmount[i]}X";
                _enemyAmountText[i].color = _textColor[i];
                _enemyImageConponenet[i].sprite = _enemyImage[i];
                _enemyImageConponenet[i].color = Color.white;
            }

            else
            {
                _enemyAmountText[i].text = "0X";
                _enemyAmountText[i].color = _textColor[5];
                _enemyImageConponenet[i].sprite = _enemyImage[5];
                _enemyImageConponenet[i].color = _textColor[5];
            }
        }
    }
}