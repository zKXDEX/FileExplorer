﻿using FileExplorerRedesign.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace FileExplorerRedesign.Views.Clase
{
    class WindowBlureffect
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        private uint _blurOpacity;
        public double BlurOpacity
        {
            get { return _blurOpacity; }
            set { _blurOpacity = (uint)value; EnableBlur(); }
        }

        private uint _blurBackgroundColor = 0x990000;
        private ViewModel viewModel;
        private object value;

        private Window window { get; set; }
        private AccentState accentState { get; set; }
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(window);
            var accent = new AccentPolicy();


            //To  enable blur the image behind the window
            accent.AccentState = accentState;
            accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF); /*(White mask 0xFFFFFF)*/


            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        //to call blur in our desired window
        internal WindowBlureffect(Window window, AccentState accentState)
        {
            this.window = window;
            this.accentState = accentState;
            EnableBlur();
        }

        public WindowBlureffect(ViewModel viewModel, object value)
        {
            this.viewModel = viewModel;
            this.value = value;
        }
    }
}