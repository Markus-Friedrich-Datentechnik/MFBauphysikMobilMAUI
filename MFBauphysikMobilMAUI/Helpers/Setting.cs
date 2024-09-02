using System;
using System.Collections.Generic;
using System.Text;

namespace MFBauphysikMobilMAUI.Helpers
{
    public static class Setting
    {
        const int theme = 0;
        const double size_default = 16;
        const double size_large = 24;
        const double size_medium = 19.2;
        const double size_micro = 11.2;
        const double size_title = 27.2;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
        public static double Size_Default
        {
            get => Preferences.Get(nameof(Size_Default), size_default);
            set => Preferences.Set(nameof(Size_Default), value);
            
        }
        public static double Size_Large
        {
            get => Preferences.Get(nameof(Size_Large), size_large);
            set => Preferences.Set(nameof(Size_Large), value);
        }
        public static double Size_Medium
        {
            get => Preferences.Get(nameof(Size_Medium), size_medium);
            set => Preferences.Set(nameof(Size_Medium), value);
        }
        public static double Size_Micro
        {
            get => Preferences.Get(nameof(Size_Micro), size_micro);
            set => Preferences.Set(nameof(Size_Micro), value);
        }
        public static double Size_Title
        {
            get => Preferences.Get(nameof(Size_Title), size_title);
            set => Preferences.Set(nameof(Size_Title), value);
        }

    }
}
