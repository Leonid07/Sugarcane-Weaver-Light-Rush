using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_IOS
using UnityEngine.iOS;
using UnityEngine.UI;
#endif
public class Setting : MonoBehaviour
{
    public Button policyButton;
    public Button termsButton;
    public Button shareApp;

    [SerializeField] string _policyString;
    [SerializeField] string _termsString;
    private void Start()
    {
        policyButton.onClick.AddListener(() => Application.OpenURL(_policyString));
        termsButton.onClick.AddListener(() => Application.OpenURL(_termsString));

        shareApp.onClick.AddListener(ShareApp);
    }
    void ShareApp()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }
}
