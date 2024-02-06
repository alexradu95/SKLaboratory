﻿// This requires an addition to the Android Manifest to work on quest:
// <uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true" />
//
// To work on Quest+Link, you may need to enable beta features in the Oculus
// app's settings.

using System.Runtime.InteropServices;
using StereoKit;
using StereoKit.Framework;
using Color = StereoKit.Color;

namespace SKLaboratory.Core.Steppers
{
    public class PassthroughFBExt : IStepper
    {
        bool extAvailable;
        bool enabled;
        bool enableOnInitialize;
        XrPassthroughFB activePassthrough = new XrPassthroughFB();
        XrPassthroughLayerFB activeLayer = new XrPassthroughLayerFB();

        Color oldColor;
        bool oldSky;

        public bool Available => extAvailable;
        public bool Enabled
        {
            get => enabled; set
            {
                if (extAvailable == false || enabled == value) return;
                if (value)
                {
                    enabled = StartPassthrough();
                }
                else
                {
                    PausePassthrough();
                    enabled = false;
                }
            }
        }

        public PassthroughFBExt() : this(true) { }
        public PassthroughFBExt(bool enabled = true)
        {
            if (SK.IsInitialized)
                Log.Err("PassthroughFBExt must be constructed before StereoKit is initialized!");
            Backend.OpenXR.RequestExt("XR_FB_passthrough");
            enableOnInitialize = enabled;
        }

        public bool Initialize()
        {
            extAvailable =
                Backend.XRType == BackendXRType.OpenXR &&
                Backend.OpenXR.ExtEnabled("XR_FB_passthrough") &&
                LoadBindings() &&
                InitPassthrough();

            if (enableOnInitialize)
                Enabled = true;

            return true;
        }

        public void Step()
        {
            if (Enabled == false) return;

            XrCompositionLayerPassthroughFB layer = new XrCompositionLayerPassthroughFB(
                XrCompositionLayerFlags.BLEND_TEXTURE_SOURCE_ALPHA_BIT, activeLayer);
            Backend.OpenXR.AddCompositionLayer(layer, -1);
        }

        public void Shutdown()
        {
            if (!Enabled) return;
            Enabled = false;
            DestroyPassthrough();
        }

        bool InitPassthrough()
        {
            XrResult result = xrCreatePassthroughFB(
                Backend.OpenXR.Session,
                new XrPassthroughCreateInfoFB(XrPassthroughFlagsFB.None),
                out activePassthrough);
            if (result != XrResult.Success)
            {
                Log.Err($"xrCreatePassthroughFB failed: {result}");
                return false;
            }

            result = xrCreatePassthroughLayerFB(
                Backend.OpenXR.Session,
                new XrPassthroughLayerCreateInfoFB(activePassthrough, XrPassthroughFlagsFB.None, XrPassthroughLayerPurposeFB.RECONSTRUCTION_FB),
                out activeLayer);
            if (result != XrResult.Success)
            {
                Log.Err($"xrCreatePassthroughLayerFB failed: {result}");
                return false;
            }
            return true;
        }

        void DestroyPassthrough()
        {
            xrDestroyPassthroughLayerFB(activeLayer);
            xrDestroyPassthroughFB(activePassthrough);
        }

        bool StartPassthrough()
        {
            XrResult result = xrPassthroughStartFB(activePassthrough);
            if (result != XrResult.Success)
            {
                Log.Err($"xrPassthroughStartFB failed: {result}");
                return false;
            }

            result = xrPassthroughLayerResumeFB(activeLayer);
            if (result != XrResult.Success)
            {
                Log.Err($"xrPassthroughLayerResumeFB failed: {result}");
                return false;
            }

            oldColor = Renderer.ClearColor;
            oldSky = Renderer.EnableSky;
            Renderer.ClearColor = Color.BlackTransparent;
            Renderer.EnableSky = false;
            return true;
        }

        void PausePassthrough()
        {
            xrPassthroughPauseFB(activePassthrough);

            Renderer.ClearColor = oldColor;
            Renderer.EnableSky = oldSky;
        }

        #region OpenXR native bindings and types
        enum XrStructureType : UInt64
        {
            XR_TYPE_PASSTHROUGH_CREATE_INFO_FB = 1000118001,
            XR_TYPE_PASSTHROUGH_LAYER_CREATE_INFO_FB = 1000118002,
            XR_TYPE_PASSTHROUGH_STYLE_FB = 1000118020,
            XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_FB = 1000118003,
        }
        enum XrPassthroughFlagsFB : UInt64
        {
            None = 0,
            IS_RUNNING_AT_CREATION_BIT_FB = 0x00000001,
            LAYER_DEPTH_BIT_FB = 0x00000002
        }
        enum XrCompositionLayerFlags : UInt64
        {
            None = 0,
            CORRECT_CHROMATIC_ABERRATION_BIT = 0x00000001,
            BLEND_TEXTURE_SOURCE_ALPHA_BIT = 0x00000002,
            UNPREMULTIPLIED_ALPHA_BIT = 0x00000004,
        }
        enum XrPassthroughLayerPurposeFB : UInt32
        {
            RECONSTRUCTION_FB = 0,
            PROJECTED_FB = 1,
            TRACKED_KEYBOARD_HANDS_FB = 1000203001,
            MAX_ENUM_FB = 0x7FFFFFFF,
        }
        enum XrResult : Int32
        {
            Success = 0,
        }

#pragma warning disable 0169 // handle is not "used", but required for interop
        struct XrPassthroughFB { ulong handle; }
        struct XrPassthroughLayerFB { ulong handle; }
#pragma warning restore 0169

        [StructLayout(LayoutKind.Sequential)]
        struct XrPassthroughCreateInfoFB(PassthroughFBExt.XrPassthroughFlagsFB passthroughFlags)
        {
            private XrStructureType type = XrStructureType.XR_TYPE_PASSTHROUGH_CREATE_INFO_FB;
            public IntPtr next = IntPtr.Zero;
            public XrPassthroughFlagsFB flags = passthroughFlags;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct XrPassthroughLayerCreateInfoFB(PassthroughFBExt.XrPassthroughFB passthrough, PassthroughFBExt.XrPassthroughFlagsFB flags, PassthroughFBExt.XrPassthroughLayerPurposeFB purpose)
        {
            private XrStructureType type = XrStructureType.XR_TYPE_PASSTHROUGH_LAYER_CREATE_INFO_FB;
            public IntPtr next = IntPtr.Zero;
            public XrPassthroughFB passthrough = passthrough;
            public XrPassthroughFlagsFB flags = flags;
            public XrPassthroughLayerPurposeFB purpose = purpose;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct XrPassthroughStyleFB(float textureOpacityFactor, Color edgeColor)
        {
            public XrStructureType type = XrStructureType.XR_TYPE_PASSTHROUGH_STYLE_FB;
            public IntPtr next = IntPtr.Zero;
            public float textureOpacityFactor = textureOpacityFactor;
            public Color edgeColor = edgeColor;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct XrCompositionLayerPassthroughFB(PassthroughFBExt.XrCompositionLayerFlags flags, PassthroughFBExt.XrPassthroughLayerFB layerHandle)
        {
            public XrStructureType type = XrStructureType.XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_FB;
            public IntPtr next = IntPtr.Zero;
            public XrCompositionLayerFlags flags = flags;
            public ulong space = 0;
            public XrPassthroughLayerFB layerHandle = layerHandle;
        }

        delegate XrResult del_xrCreatePassthroughFB(ulong session, [In] XrPassthroughCreateInfoFB createInfo, out XrPassthroughFB outPassthrough);
        delegate XrResult del_xrDestroyPassthroughFB(XrPassthroughFB passthrough);
        delegate XrResult del_xrPassthroughStartFB(XrPassthroughFB passthrough);
        delegate XrResult del_xrPassthroughPauseFB(XrPassthroughFB passthrough);
        delegate XrResult del_xrCreatePassthroughLayerFB(ulong session, [In] XrPassthroughLayerCreateInfoFB createInfo, out XrPassthroughLayerFB outLayer);
        delegate XrResult del_xrDestroyPassthroughLayerFB(XrPassthroughLayerFB layer);
        delegate XrResult del_xrPassthroughLayerPauseFB(XrPassthroughLayerFB layer);
        delegate XrResult del_xrPassthroughLayerResumeFB(XrPassthroughLayerFB layer);
        delegate XrResult del_xrPassthroughLayerSetStyleFB(XrPassthroughLayerFB layer, [In] XrPassthroughStyleFB style);

        del_xrCreatePassthroughFB xrCreatePassthroughFB;
        del_xrDestroyPassthroughFB xrDestroyPassthroughFB;
        del_xrPassthroughStartFB xrPassthroughStartFB;
        del_xrPassthroughPauseFB xrPassthroughPauseFB;
        del_xrCreatePassthroughLayerFB xrCreatePassthroughLayerFB;
        del_xrDestroyPassthroughLayerFB xrDestroyPassthroughLayerFB;
        del_xrPassthroughLayerPauseFB xrPassthroughLayerPauseFB;
        del_xrPassthroughLayerResumeFB xrPassthroughLayerResumeFB;
        del_xrPassthroughLayerSetStyleFB xrPassthroughLayerSetStyleFB;

        bool LoadBindings()
        {
            xrCreatePassthroughFB = Backend.OpenXR.GetFunction<del_xrCreatePassthroughFB>("xrCreatePassthroughFB");
            xrDestroyPassthroughFB = Backend.OpenXR.GetFunction<del_xrDestroyPassthroughFB>("xrDestroyPassthroughFB");
            xrPassthroughStartFB = Backend.OpenXR.GetFunction<del_xrPassthroughStartFB>("xrPassthroughStartFB");
            xrPassthroughPauseFB = Backend.OpenXR.GetFunction<del_xrPassthroughPauseFB>("xrPassthroughPauseFB");
            xrCreatePassthroughLayerFB = Backend.OpenXR.GetFunction<del_xrCreatePassthroughLayerFB>("xrCreatePassthroughLayerFB");
            xrDestroyPassthroughLayerFB = Backend.OpenXR.GetFunction<del_xrDestroyPassthroughLayerFB>("xrDestroyPassthroughLayerFB");
            xrPassthroughLayerPauseFB = Backend.OpenXR.GetFunction<del_xrPassthroughLayerPauseFB>("xrPassthroughLayerPauseFB");
            xrPassthroughLayerResumeFB = Backend.OpenXR.GetFunction<del_xrPassthroughLayerResumeFB>("xrPassthroughLayerResumeFB");
            xrPassthroughLayerSetStyleFB = Backend.OpenXR.GetFunction<del_xrPassthroughLayerSetStyleFB>("xrPassthroughLayerSetStyleFB");

            return
                xrCreatePassthroughFB != null &&
                xrDestroyPassthroughFB != null &&
                xrPassthroughStartFB != null &&
                xrPassthroughPauseFB != null &&
                xrCreatePassthroughLayerFB != null &&
                xrDestroyPassthroughLayerFB != null &&
                xrPassthroughLayerPauseFB != null &&
                xrPassthroughLayerResumeFB != null &&
                xrPassthroughLayerSetStyleFB != null;
        }
        #endregion
    }
}