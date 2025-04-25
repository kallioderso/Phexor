using System;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Phexor.Scripts;

public static class ShortCuts
{
    public static void KeyPress(KeyEventArgs e, Explorer explorer) => Selector(e, explorer);
    
    private static void Selector(KeyEventArgs e, Explorer explorer)
    {
        if (Keyboard.FocusedElement is TextBox)
        {
            switch (e.Key)
            {
                case Key.Enter: PathInputEnter(explorer); break;
                case Key.Escape: EscapeInput(explorer); break;
            }
        }
        else
        {
            switch (e.Key)
            {
                case Key.U: Undo(explorer); break;
                case Key.R: Redo(explorer); break;
                case Key.I: Input(explorer); break;
                case Key.S: Settings(explorer); break;
            }
        }
    }
    
    private static void Undo(Explorer e) => e.UndoPath();
    private static void Redo(Explorer e) => e.RedoPath();
    private static void Input(Explorer e) => Keyboard.Focus(e.PathInput);
    private static void PathInputEnter(Explorer e) { Keyboard.ClearFocus(); e.PathInput.Focusable = false; e.Focus(); e.PathInput.Focusable = true; e.InputFieldPath();}
    private static void EscapeInput(Explorer e) { Keyboard.ClearFocus(); e.PathInput.Focusable = false; e.Focus(); e.PathInput.Focusable = true;}
    private static void Settings(Explorer e) => e.Settings();
}