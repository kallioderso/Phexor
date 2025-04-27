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
    public static void Clicked(Explorer explorer) => EscapeInput(explorer); //Public Method to call the EscapeInput method
    
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
                case Key.U: Undo(explorer); break; //U key pressed
                case Key.R: Redo(explorer); break; //R key pressed
                case Key.I: e.Handled = true; Input(explorer); break; //I key pressed
                case Key.S: Settings(explorer); break; //S key pressed
            }
        }
    }
    
    //-----ShortCut Methods-----\\
    private static void Undo(Explorer e) => e.UndoPath(); //Undo the last action
    private static void Redo(Explorer e) => e.RedoPath(); //Redo the last action
    private static void Input(Explorer e)
    {
        Keyboard.Focus(e.PathInput); //Focus on the PathInput TextBox
        if (e.PathInput.Text != PathSearcher.Path) e.PathInput.Text = PathSearcher.Path; //Set the PathInput TextBox to the current path
        e.PathInput.CaretIndex = e.PathInput.Text.Length; //Set the caret index to the end of the text
    }

    private static void PathInputEnter(Explorer e) { Keyboard.ClearFocus(); e.PathInput.Focusable = false; e.Focus(); e.PathInput.Focusable = true; e.InputFieldPath();} //Call the PathInputEnter method
    private static void EscapeInput(Explorer e) { Keyboard.ClearFocus(); e.PathInput.Focusable = false; e.Focus(); e.PathInput.Focusable = true;} //Clear the focus from the PathInput TextBox
    private static void Settings(Explorer e) => e.Settings(); //Open the Settings window
}