// This requires an addition to the Android Manifest to work on quest:
// <uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true" />
//
// To work on Quest+Link, you may need to enable beta features in the Oculus
// app's settings.

using System.Runtime.InteropServices;
using StereoKit;
using StereoKit.Framework;
using Color = StereoKit.Color;

namespace SKLaboratory.Core.Steppers;

public class PassthroughFbExt : IStepper
{
	private XrPassthroughLayerFb _activeLayer;
	private XrPassthroughFb _activePassthrough;
	private bool _enabled;
	private readonly bool _enableOnInitialize;

	private Color _oldColor;
	private bool _oldSky;

	public PassthroughFbExt() : this(true)
	{
	}

	public PassthroughFbExt(bool enabled = true)
	{
		if (SK.IsInitialized)
			Log.Err("PassthroughFBExt must be constructed before StereoKit is initialized!");
		Backend.OpenXR.RequestExt("XR_FB_passthrough");
		_enableOnInitialize = enabled;
	}

	public bool Available { get; private set; }

	public bool Enabled
	{
		get => _enabled;
		set
		{
			if (Available == false || _enabled == value) return;
			if (value)
			{
				_enabled = StartPassthrough();
			}
			else
			{
				PausePassthrough();
				_enabled = false;
			}
		}
	}

	public bool Initialize()
	{
		Available =
			Backend.XRType == BackendXRType.OpenXR &&
			Backend.OpenXR.ExtEnabled("XR_FB_passthrough") &&
			LoadBindings() &&
			InitPassthrough();

		if (_enableOnInitialize)
			Enabled = true;

		return true;
	}

	public void Step()
	{
		if (Enabled == false) return;

		var layer = new XrCompositionLayerPassthroughFb(
			XrCompositionLayerFlags.BlendTextureSourceAlphaBit, _activeLayer);
		Backend.OpenXR.AddCompositionLayer(layer, -1);
	}

	public void Shutdown()
	{
		if (!Enabled) return;
		Enabled = false;
		DestroyPassthrough();
	}

	private bool InitPassthrough()
	{
		var result = _xrCreatePassthroughFb(
			Backend.OpenXR.Session,
			new XrPassthroughCreateInfoFb(XrPassthroughFlagsFb.None),
			out _activePassthrough);
		if (result != XrResult.Success)
		{
			Log.Err($"xrCreatePassthroughFB failed: {result}");
			return false;
		}

		result = _xrCreatePassthroughLayerFb(
			Backend.OpenXR.Session,
			new XrPassthroughLayerCreateInfoFb(_activePassthrough, XrPassthroughFlagsFb.None,
				XrPassthroughLayerPurposeFb.ReconstructionFb),
			out _activeLayer);
		if (result != XrResult.Success)
		{
			Log.Err($"xrCreatePassthroughLayerFB failed: {result}");
			return false;
		}

		return true;
	}

	private void DestroyPassthrough()
	{
		_xrDestroyPassthroughLayerFb(_activeLayer);
		_xrDestroyPassthroughFb(_activePassthrough);
	}

	private bool StartPassthrough()
	{
		var result = _xrPassthroughStartFb(_activePassthrough);
		if (result != XrResult.Success)
		{
			Log.Err($"xrPassthroughStartFB failed: {result}");
			return false;
		}

		result = _xrPassthroughLayerResumeFb(_activeLayer);
		if (result != XrResult.Success)
		{
			Log.Err($"xrPassthroughLayerResumeFB failed: {result}");
			return false;
		}

		_oldColor = Renderer.ClearColor;
		_oldSky = Renderer.EnableSky;
		Renderer.ClearColor = Color.BlackTransparent;
		Renderer.EnableSky = false;
		return true;
	}

	private void PausePassthrough()
	{
		_xrPassthroughPauseFb(_activePassthrough);

		Renderer.ClearColor = _oldColor;
		Renderer.EnableSky = _oldSky;
	}

	#region OpenXR native bindings and types

	private enum XrStructureType : ulong
	{
		XrTypePassthroughCreateInfoFb = 1000118001,
		XrTypePassthroughLayerCreateInfoFb = 1000118002,
		XrTypePassthroughStyleFb = 1000118020,
		XrTypeCompositionLayerPassthroughFb = 1000118003
	}

	private enum XrPassthroughFlagsFb : ulong
	{
		None = 0,
		IsRunningAtCreationBitFb = 0x00000001,
		LayerDepthBitFb = 0x00000002
	}

	private enum XrCompositionLayerFlags : ulong
	{
		None = 0,
		CorrectChromaticAberrationBit = 0x00000001,
		BlendTextureSourceAlphaBit = 0x00000002,
		UnpremultipliedAlphaBit = 0x00000004
	}

	private enum XrPassthroughLayerPurposeFb : uint
	{
		ReconstructionFb = 0,
		ProjectedFb = 1,
		TrackedKeyboardHandsFb = 1000203001,
		MaxEnumFb = 0x7FFFFFFF
	}

	private enum XrResult
	{
		Success = 0
	}

#pragma warning disable 0169 // handle is not "used", but required for interop
	private struct XrPassthroughFb
	{
		private ulong _handle;
	}

	private struct XrPassthroughLayerFb
	{
		private ulong _handle;
	}
#pragma warning restore 0169

	[StructLayout(LayoutKind.Sequential)]
	private struct XrPassthroughCreateInfoFb(XrPassthroughFlagsFb passthroughFlags)
	{
		private XrStructureType type = XrStructureType.XrTypePassthroughCreateInfoFb;
		public IntPtr next = IntPtr.Zero;
		public XrPassthroughFlagsFb flags = passthroughFlags;
	}

	[StructLayout(LayoutKind.Sequential)]
	private struct XrPassthroughLayerCreateInfoFb(
		XrPassthroughFb passthrough,
		XrPassthroughFlagsFb flags,
		XrPassthroughLayerPurposeFb purpose)
	{
		private XrStructureType type = XrStructureType.XrTypePassthroughLayerCreateInfoFb;
		public IntPtr next = IntPtr.Zero;
		public XrPassthroughFb passthrough = passthrough;
		public XrPassthroughFlagsFb flags = flags;
		public XrPassthroughLayerPurposeFb purpose = purpose;
	}

	[StructLayout(LayoutKind.Sequential)]
	private struct XrPassthroughStyleFb(float textureOpacityFactor, Color edgeColor)
	{
		public XrStructureType type = XrStructureType.XrTypePassthroughStyleFb;
		public IntPtr next = IntPtr.Zero;
		public float textureOpacityFactor = textureOpacityFactor;
		public Color edgeColor = edgeColor;
	}

	[StructLayout(LayoutKind.Sequential)]
	private struct XrCompositionLayerPassthroughFb(XrCompositionLayerFlags flags, XrPassthroughLayerFb layerHandle)
	{
		public XrStructureType type = XrStructureType.XrTypeCompositionLayerPassthroughFb;
		public IntPtr next = IntPtr.Zero;
		public XrCompositionLayerFlags flags = flags;
		public ulong space = 0;
		public XrPassthroughLayerFb layerHandle = layerHandle;
	}

	private delegate XrResult DelXrCreatePassthroughFb(ulong session, [In] XrPassthroughCreateInfoFb createInfo,
		out XrPassthroughFb outPassthrough);

	private delegate XrResult DelXrDestroyPassthroughFb(XrPassthroughFb passthrough);

	private delegate XrResult DelXrPassthroughStartFb(XrPassthroughFb passthrough);

	private delegate XrResult DelXrPassthroughPauseFb(XrPassthroughFb passthrough);

	private delegate XrResult DelXrCreatePassthroughLayerFb(ulong session,
		[In] XrPassthroughLayerCreateInfoFb createInfo, out XrPassthroughLayerFb outLayer);

	private delegate XrResult DelXrDestroyPassthroughLayerFb(XrPassthroughLayerFb layer);

	private delegate XrResult DelXrPassthroughLayerPauseFb(XrPassthroughLayerFb layer);

	private delegate XrResult DelXrPassthroughLayerResumeFb(XrPassthroughLayerFb layer);

	private delegate XrResult DelXrPassthroughLayerSetStyleFb(XrPassthroughLayerFb layer,
		[In] XrPassthroughStyleFb style);

	private DelXrCreatePassthroughFb _xrCreatePassthroughFb;
	private DelXrDestroyPassthroughFb _xrDestroyPassthroughFb;
	private DelXrPassthroughStartFb _xrPassthroughStartFb;
	private DelXrPassthroughPauseFb _xrPassthroughPauseFb;
	private DelXrCreatePassthroughLayerFb _xrCreatePassthroughLayerFb;
	private DelXrDestroyPassthroughLayerFb _xrDestroyPassthroughLayerFb;
	private DelXrPassthroughLayerPauseFb _xrPassthroughLayerPauseFb;
	private DelXrPassthroughLayerResumeFb _xrPassthroughLayerResumeFb;
	private DelXrPassthroughLayerSetStyleFb _xrPassthroughLayerSetStyleFb;

	private bool LoadBindings()
	{
		_xrCreatePassthroughFb = Backend.OpenXR.GetFunction<DelXrCreatePassthroughFb>("xrCreatePassthroughFB");
		_xrDestroyPassthroughFb = Backend.OpenXR.GetFunction<DelXrDestroyPassthroughFb>("xrDestroyPassthroughFB");
		_xrPassthroughStartFb = Backend.OpenXR.GetFunction<DelXrPassthroughStartFb>("xrPassthroughStartFB");
		_xrPassthroughPauseFb = Backend.OpenXR.GetFunction<DelXrPassthroughPauseFb>("xrPassthroughPauseFB");
		_xrCreatePassthroughLayerFb =
			Backend.OpenXR.GetFunction<DelXrCreatePassthroughLayerFb>("xrCreatePassthroughLayerFB");
		_xrDestroyPassthroughLayerFb =
			Backend.OpenXR.GetFunction<DelXrDestroyPassthroughLayerFb>("xrDestroyPassthroughLayerFB");
		_xrPassthroughLayerPauseFb =
			Backend.OpenXR.GetFunction<DelXrPassthroughLayerPauseFb>("xrPassthroughLayerPauseFB");
		_xrPassthroughLayerResumeFb =
			Backend.OpenXR.GetFunction<DelXrPassthroughLayerResumeFb>("xrPassthroughLayerResumeFB");
		_xrPassthroughLayerSetStyleFb =
			Backend.OpenXR.GetFunction<DelXrPassthroughLayerSetStyleFb>("xrPassthroughLayerSetStyleFB");

		return
			_xrCreatePassthroughFb != null &&
			_xrDestroyPassthroughFb != null &&
			_xrPassthroughStartFb != null &&
			_xrPassthroughPauseFb != null &&
			_xrCreatePassthroughLayerFb != null &&
			_xrDestroyPassthroughLayerFb != null &&
			_xrPassthroughLayerPauseFb != null &&
			_xrPassthroughLayerResumeFb != null &&
			_xrPassthroughLayerSetStyleFb != null;
	}

	#endregion
}