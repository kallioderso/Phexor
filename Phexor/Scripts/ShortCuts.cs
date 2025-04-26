using System;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Phexor.Scripts;
// C. = Create
// G. = Generate
// V. = Variable
// L. = List
// M. = Method
// Xc. = XAML code
// C#c. = C# code

public static class ShortCuts
{
    //-----Public Methods-----\\
    public static void KeyPress(KeyEventArgs e, Explorer explorer) => Selector(e, explorer); //Public Method to call the Selector method
    
    //-----Private Methods-----\\
    private static void Selector(KeyEventArgs e, Explorer explorer) //Selector method to check which key was pressed
    {
        if (Keyboard.FocusedElement is TextBox) //Check if the focused element is a TextBox
        {
            switch (e.Key) //Check which key was pressed
            {
                case Key.Enter: PathInputEnter(explorer); break; //Enter key pressed
                case Key.Escape: EscapeInput(explorer); break; //Escape key pressed
            }
        }
        else
        {
            switch (e.Key) //Check which key was pressed
            {
                case Key.E: explorer.Clear(); break; //E key pressed
                case Key.F: explorer.Clear(); break; //F key pressed
                case Key.O: explorer.Clear(); break; //O key pressed
                case Key.C: explorer.Clear(); break; //C key pressed
                case Key.X: explorer.Clear(); break; //X key pressed
                case Key.Z: explorer.Clear(); break; //Z key pressed
            }
        }
    }
    
    //-----ShortCut Methods-----\\
    private static void Undo(Explorer e) => e.UndoPath(); //call the UndoPath method from the Explorer class
    private static void Redo(Explorer e) => e.RedoPath(); //call the RedoPath method from the Explorer class
    private static void Input(Explorer e) => Keyboard.Focus(e.PathInput); //Set focus to the PathInput TextBox
    private static void PathInputEnter(Explorer e) { Keyboard.ClearFocus(); e.PathInput.Focusable = false; e.Focus(); e.PathInput.Focusable = true; e.InputFieldPath(); } //call the PathInputEnter method from the Explorer class
    private static void EscapeInput(Explorer e) { Keyboard.ClearFocus(); e.PathInput.Focusable = false; e.Focus(); e.PathInput.Focusable = true;} //Set focus to the PathInput TextBox
    private static void Settings(Explorer e) => e.Settings(); //call the Settings method from the Explorer class
}