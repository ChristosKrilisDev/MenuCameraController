using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    #region Members
    [SerializeField]        //Make private members visible in the editor
    private Transform _startPos , _settingsPos , _playPos;      //Add any Transform for each In-Game Position you need, you can add them from editor
    private Transform _desiredLocation;         //The selected transform on run time

    [Header("Motion Members")]
    [SerializeField][Range(0,1)] 
    private float _smoothTime = 0.25f;          //Smooth Speed Value
    [SerializeField] 
    private float speedMultiplier = 2f;        //Camera movement speed
    private bool canMove  = true;

    #endregion

    private void Start()
    {
        InitCameraController();
    }

    void InitCameraController()
    {
        //Set Camera's position and rotation to => start.transform
        _desiredLocation = _startPos;
        transform.position = _desiredLocation.localPosition;
        transform.rotation = _desiredLocation.rotation;
    }

    private void Update()
    {
        if(canMove)
            Move();
    }

    void Move()
    {
        //transform.localPosition = _desiredPos;
        transform.position = Vector3.Lerp
            (transform.position , _desiredLocation.position , _smoothTime * speedMultiplier * Time.deltaTime);

        transform.rotation = Quaternion.Slerp
            (transform.rotation , _desiredLocation.localRotation , _smoothTime * speedMultiplier * Time.deltaTime);
    }

    //Set the desiredPosition => selectedPos 
    #region Set_Pos_() 
    internal void SetStartPos() => _desiredLocation = _startPos;
    internal void SetSettingsPos() => _desiredLocation = _settingsPos;
    internal void SetPlayPos() => _desiredLocation = _playPos;
    #endregion

    //Validate whenever the camera is close to the desiredPos , if so return false
    internal bool IsMoving()
    {
        if(transform.position.x >= _desiredLocation.transform.position.x - 1f &&
            transform.position.x <= _desiredLocation.transform.position.x + 1f)
        {
            //Camera is not moving or is close to desiredPos
            return false;
        }
        return true;
    }



    #region ExtraPublicGoToMethods
    ///<summary>
    ///The following code , are some fuctions to controll the postion you want to go and to have a better controll
    ///on when the camera is close to the target, so you may open UI, or play a song etc.
    ///
    ///These fuctions are linked to Buttons.Onclick, you can use them from any other class just make sure you have a referece to this Script
    ///
    /// NOTE : The folloing code will not work, need to rework base on what you need
    ///</summary>


    //private delegate void _DelegateTargetView();      
    //private static event _DelegateTargetView _TargetView;     //Set the fuction

    //public void GoToStartView()
    //{
    //    _cameraController.SetStartPos();
    //    if(_TargetView != null)
    //        _TargetView = null;
    //    _TargetView += _uiController.SetStartPanelActive; //Add to the delegate the fuction SetStartPanelActive
    //    StartCoroutine(WaitCameraMotion(_TargetView));
    //}
    //public void GoToSettingsView()
    //{
    //    _cameraController.SetSettingsPos();
    //    if(_TargetView != null)
    //        _TargetView = null;
    //    _TargetView += _uiController.SetSettingsPanelActive;
    //    StartCoroutine(WaitCameraMotion(_TargetView));
    //}
    //public void GoToPlayView()
    //{
    //    _cameraController.SetPlayPos();
    //    if(_TargetView != null)
    //        _TargetView = null;
    //    _TargetView += _uiController.SetPlayPanelActive;
    //    StartCoroutine(WaitCameraMotion(_TargetView));
    //}




    //IEnumerator WaitCameraMotion(Delegate action)
    //{
    //    //Close the current Panel
    //    _uiController.CloseOpenedPanelFadeOut();

    //    while(_cameraController.IsMoving())
    //    {
    //        yield return new WaitForSeconds(1f);
    //    }

          //Exucute the action after the camera movement has end.
    //    action.DynamicInvoke();   

    //    yield return null;
    //}
    #endregion

}
