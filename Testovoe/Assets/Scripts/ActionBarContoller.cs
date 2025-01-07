using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ActionBarContoller : MonoBehaviour
{
    public int selectedAbilityNumber = 1;

    public List<GameObject> abilities;
    
    private List<float> _abilitiesCDLeft;

    public List<float> abilitiesCDLeft
    {
        get => _abilitiesCDLeft;
        set => _abilitiesCDLeft = value;
    }

    private List<Vector3> _originalScales = new();
    
    private float _refreshingRate = 0.01f;
    private float _scaleFactor = 1.3f;
    void Start()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            _originalScales.Add(abilities[i].transform.localScale);
        }

        if (_abilitiesCDLeft.Count == 0)
        {
            for (int i = 0; i < abilities.Count; i++)
            {
               _abilitiesCDLeft.Add(0);
            }
        }
        ScaleAbility(selectedAbilityNumber-1);
        StartCoroutine(CooldownAbility());
    }

    private void ScaleAbility(int selectedAbilityIndex)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (i == selectedAbilityIndex)
            {
                abilities[i].transform.localScale = _originalScales[i] * _scaleFactor;
            }
            else
            {
                abilities[i].transform.localScale = _originalScales[i];
            }
        }
    }
    
    public IEnumerator CooldownAbility()
    {
        var prevTime = Time.time;
        while (true)
        {
            for (int i = 0; i < _abilitiesCDLeft.Count; i++)
            {
                if (_abilitiesCDLeft[i] > 0)
                {
                    GameObject cooldownBG = abilities[i].transform.Find("CDBG").gameObject;
                    cooldownBG.SetActive(true);
                    TextMeshProUGUI cooldownTimeLeft =
                        cooldownBG.transform.Find("TimerText").gameObject.GetComponent<TextMeshProUGUI>();
                    cooldownTimeLeft.text = _abilitiesCDLeft[i].ToString("F1");

                    var dt = Time.time - prevTime;
                    _abilitiesCDLeft[i] -= dt;
                }
                else
                {
                    GameObject cooldownBG = abilities[i].transform.Find("CDBG").gameObject;
                    cooldownBG.SetActive(false);
                    _abilitiesCDLeft[i] = 0;
                }
            }

            prevTime = Time.time;
            yield return new WaitForSeconds(_refreshingRate);
        }
    }
    
    public bool IsCurrentSpellInCD()
    {
        return _abilitiesCDLeft[selectedAbilityNumber - 1] > 0;
    }

    public void Shoot(float delay)
    {
        _abilitiesCDLeft[selectedAbilityNumber - 1] = delay;
    }

    public void SwitchAbility(int index)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].transform.localScale = _originalScales[i];
        }
        abilities[index].transform.localScale = _originalScales[index]*_scaleFactor;
        selectedAbilityNumber = index + 1;
    }

    public void SetLabel(string label, int index)
    {
        abilities[index].transform.Find("HotKey").gameObject.GetComponent<TextMeshProUGUI>().text =
          label;
    }
}
