using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    public FlashColor _flashColor;

    [Header("Guns")]
    public List<GunBase> guns;

    protected override void Init()
    {
        base.Init();

        CreateGun(0);

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CreateGun(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            CreateGun(1);
    }

    private void CreateGun(int i)
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject);

        _currentGun = Instantiate(guns[i], gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        ShakeCamera.Instance.Shake();
        _currentGun.StartShoot();

        _flashColor?.Flash();
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
    }
}
