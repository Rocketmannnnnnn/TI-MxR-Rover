﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SkyboxController : MonoBehaviour
{
    private float timer = 0;
    private List<Material> images;
    private int imageIndex = 0;
    public float delay = 0.5f;
    public ImageProcessor imageProcessor;
    public Material baseMaterial;
    public SteamVR_Action_Vector2 joystickAction;
    public float threashold = 0.1f;

    private void Awake()
    {
        this.imageProcessor.OnFinishedParsing += OnFinishedParsing;
    }

    /// <summary>
    /// Reads the controller input every frame
    /// </summary>
    void FixedUpdate()
    {
        float value = Input.GetKey(KeyCode.Keypad6) ? 1 : Input.GetKey(KeyCode.Keypad4) ? -1 : joystickAction.GetAxis(SteamVR_Input_Sources.Any).x;
        if(Time.time > timer && value != 0)
        {
            NextImage(value);
            timer = Time.time + delay;
        }
    }

    /// <summary>
    /// Sets the next image in the skybox
    /// </summary>
    /// <param name="value"> The joystick input, positive for next image, negative for previous. </param>
    private void NextImage(float value)
    {
        if(value > this.threashold)
        {
            this.imageIndex = Mathf.Clamp(this.imageIndex + 1, 0, this.images.Count - 1);
            RenderSettings.skybox = this.images[this.imageIndex];
        }
        if (value < -this.threashold)
        {
            this.imageIndex = Mathf.Clamp(this.imageIndex - 1, 0, this.images.Count - 1);
            RenderSettings.skybox = this.images[this.imageIndex];
        }
    }

    /// <summary>
    /// Removes the event's subscription and destroys the imageProcessor.
    /// </summary>
    /// <param name="sender"> The event's sender. </param>
    /// <param name="e"> Empty event args. </param>
    private void OnFinishedParsing(object sender, EventArgs e)
    {
        this.imageProcessor.OnFinishedParsing -= OnFinishedParsing;
        CreateSkyboxMaterials();
    }

    /// <summary>
    /// Creates the materials for the skybox from an image.
    /// Also sets the first image in the skybox.
    /// </summary>
    private void CreateSkyboxMaterials()
    {
        this.images = new List<Material>();
        foreach (Texture2D tex in this.imageProcessor.textureColors.Keys)
        {
            var mat = new Material(baseMaterial);
            mat.mainTexture = tex;
            this.images.Add(mat);
        }
        RenderSettings.skybox = this.images[this.imageIndex];
    }
}
