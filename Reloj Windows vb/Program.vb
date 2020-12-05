'------------------------------------------------------------------------------
' Punto de entrada del programa                                     (04/Dic/20)
'
' Código convertido de C# a Visual Basic con CSharpToVB de Paul1956
'
' He tenido que añadir manualmente los ficheros de recursos:
' Form1.resx
' y adaptar los de Resources y Settings de C# a VB
' Pero... ¡¡¡ya funciona!!!
'
' (c) Guillermo (elGuille) Som, 2020
'------------------------------------------------------------------------------
Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq
Imports Microsoft.VisualBasic
'Imports vb = Microsoft.VisualBasic

Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace Reloj_Windows
    Module Program
        ''' <summary>
        '''  The main entry point for the application.
        ''' </summary>
        <STAThread>
        Public Sub Main()
            Application.SetHighDpiMode(HighDpiMode.SystemAware)
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New FormReloj)
        End Sub
    End Module
End Namespace
