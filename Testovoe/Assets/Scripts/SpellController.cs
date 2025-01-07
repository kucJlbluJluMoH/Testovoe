using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpellController : MonoBehaviour
{
    [SerializeField] private ActionBarContoller _actionBarContoller;
    [SerializeField] private List<AbilityInfo> _abilities; 
    [SerializeField] private float _spellSpeed = 20f;
    private List<KeyCode> _keyCodes = new(); 
    private void Start()
    {
        for (int i = 0; i < _abilities.Count; i++)
        {
            _actionBarContoller.SetLabel(_abilities[i].KeyCode.ToString(), i);
            _keyCodes.Add(_abilities[i].KeyCode);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !IsPointerOverUI() && !_actionBarContoller.IsCurrentSpellInCD()) 
        {
            Shoot();
        }

        foreach (KeyCode keyCode in _keyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                _actionBarContoller.SwitchAbility(_keyCodes.IndexOf(keyCode));
            }
        }
        
    }
    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            _actionBarContoller.Shoot(_abilities[_actionBarContoller.selectedAbilityNumber-1].Cooldown);
            
            switch (_actionBarContoller.selectedAbilityNumber)
            {
                case 1:
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    GameObject bullet = Instantiate(_abilities[_actionBarContoller.selectedAbilityNumber-1].Prefab, transform.position, Quaternion.identity);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = direction * _spellSpeed;
                    }
                    break;
                case 2:
                    Instantiate(_abilities[_actionBarContoller.selectedAbilityNumber-1].Prefab, targetPosition, Quaternion.identity);
                    break;
            }
        }
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    [Serializable]
    private class AbilityInfo
    {
        [SerializeField] private GameObject _spellPrefab;
        [SerializeField] private float _abilityCooldown;
        [SerializeField] private KeyCode _keyCode;

        public GameObject Prefab => _spellPrefab;
        public float Cooldown => _abilityCooldown;
        public KeyCode KeyCode => _keyCode;
    }
}
