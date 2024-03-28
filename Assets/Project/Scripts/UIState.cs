using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    [SerializeField] private List<UIState> nearStates = new();
    public IReadOnlyCollection<UIState> NearStates => nearStates;
    
    public Image image1;
    public Image image2;

    public AssetReferenceT<Sprite> test1;
    public AssetReferenceT<Sprite> test2;


    public event Action<UIState> Activated;

    public async void Activate()
    {
        image1.sprite = null;
        image2.sprite = null;

        if (!test1.IsValid())
            image1.sprite = await test1.LoadAssetAsync<Sprite>().Task;
        else
        {
            image1.sprite = test1.Asset as Sprite;
        }

        image2.sprite = await test2.LoadAssetAsync<Sprite>().Task;

        nearStates.ForEach(a => a.HalfActivate());

        Activated?.Invoke(this);
    }

    public async void HalfActivate()
    {
        image1.sprite = null;
        image2.sprite = null;

        if (!test1.IsValid())
            image1.sprite = await test1.LoadAssetAsync<Sprite>().Task;
        else
        {
            image1.sprite = test1.Asset as Sprite;
        }

        image2.sprite = null;
        test2.ReleaseAsset();
    }

    public void Deactivate()
    {
        image1.sprite = null;
        image2.sprite = null;
        test1.ReleaseAsset();
        test2.ReleaseAsset();
    }
}