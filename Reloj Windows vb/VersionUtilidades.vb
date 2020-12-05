'------------------------------------------------------------------------------
' Versiones utilidades                                              (04/Dic/20)
' Para comprobar las versiones de mis utilidades y programas
'
' Agrego la función CompararVersionWeb con dos sobrecargas          (05/Dic/20)
' 
' Esa información está en 
'   www.elguille.info/NET/dotnet/versiones-utilidades.aspx
' Y el formato es: 
'   <meta name="NombreAplicacion" content="1.0.0.0" />
' Por ejemplo:
'   <meta name="Reloj Windows (C#)" content="1.0.0.4" />
'   <meta name="gsPanelClip" content="4.0.1.7" />
' Para comprobar directamente las versiones usar: EsMayorLaVersionWeb
'
' (c) Guillermo (elGuille) Som, 2020
'------------------------------------------------------------------------------
Option Strict On
Option Infer On

Imports System
'Imports System.Data
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq
Imports Microsoft.VisualBasic
'Imports vb = Microsoft.VisualBasic
Imports System.Text.RegularExpressions

Public Class VersionUtilidades

    Private Const laUrl As String = "http://www.elguille.info/NET/dotnet/versiones-utilidades.aspx"

    ''' <summary>
    ''' Devuelve una cadena en formato 0.0.0.0 con la versión del programa indicado.
    ''' </summary>
    ''' <param name="aplicacion">Nombre de la aplicación 
    ''' en .NET Framework lo que se pone en AssemblyName (Product en la ventana de Assembly Information) o en 
    ''' lo que se pone en Product en .NET 5.0, ese el valor devuelto por Application.ProductName.</param>
    ''' <returns>Devuelve una cadena con la versión leída en elGuille.info 
    ''' o "" si no se ha hallado el nombre de la aplciación</returns>
    Public Shared Function VersionWeb(aplicacion As String) As String

        Try
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(laUrl)
            Dim response As System.Net.WebResponse
            Dim reader As System.IO.StreamReader
            ' Obtener la respuesta.
            response = request.GetResponse()
            ' Abrir el stream de la respuesta recibida.
            reader = New System.IO.StreamReader(response.GetResponseStream())
            ' Leer el contenido.
            Dim s As String = reader.ReadToEnd()
            ' Cerrar los streams abiertos.
            reader.Close()
            response.Close()

            ' Código anterior al 04/Dic/20
            '' Comprobar el valor de <meta name="version" 
            '' Usar esta expresión regular: <meta name="version" content="(\d.\d.\d.\d)" />
            '' En Groups(1) estará la versión
            '' Comprobar que haya más de una cifra                   (14/Abr/07)
            '' Tener en cuenta que se pueda usar en el formato > y /> (con o sin espacio)
            'Dim r As New Regex("<meta name=""version"" content=""(\d{1,}.\d{1,}.\d{1,}.\d{1,})""\s?/?>")

            ' Comprobar el valor de <meta name="ProductName" 
            ' Usar esta expresión regular: <meta name="version" content="(\d.\d.\d.\d)" />
            ' En Groups(1) estará la versión
            ' Comprobar que haya más de una cifra                   (14/Abr/07)
            ' Tener en cuenta que se pueda usar en el formato > y /> (con o sin espacio)
            Dim elMeta = $"<meta name=""{aplicacion}"""
            Dim r As New Regex(elMeta & " content=""(\d{1,}.\d{1,}.\d{1,}.\d{1,})""\s?/?>")

            For Each m As Match In r.Matches(s)
                If m.Groups.Count > 1 Then
                    Return m.Groups(1).Value
                End If
            Next
        Catch 'ex As Exception
            'Return ""
            'System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Comprueba la versión de la aplicación indicada y la compara con la indicada en versionActual.
    ''' Si la version en la web es mayor, devuelve True, en otro caso devuelve False.
    ''' </summary>
    ''' <param name="aplicacion">Nombre de la aplicación a comprobar. Debería ser el valore devuelto por Application.ProductName</param>
    ''' <param name="versionActual">La versión actual de la aplicación, debe ser el valor de FileVersion.</param>
    ''' <param name="laVersionWeb">Aquí devolverá la versión que hay en la Web</param>
    ''' <returns>Devuelve True si la versión en la web es mayor que la actual.</returns>
    Public Shared Function EsMayorLaVersionWeb(aplicacion As String, versionActual As String,
                                               ByRef laVersionWeb As String) As Boolean
        ' Para comprobar las versiones

        Dim vWeb = VersionWeb(aplicacion)
        laVersionWeb = vWeb

        ' Por si no lee bien la versión
        If String.IsNullOrEmpty(vWeb) Then
            vWeb = "0.0.0.0"
        End If

        ' Para comprobar mejor las versiones de la Web (del AcercaDe usado en colorear código)
        ' Solo funcionará bien con valores de 1 cifra
        ' ya que 1.0.3.11 será menor que 1.0.3.9 aunque no sea así...
        ' Convertirlo en cadena de números de dos cifras

        Dim aWeb() = vWeb.Split("."c)

        Dim aFic() = versionActual.Split("."c)

        vWeb = ""
        Dim vApp = ""

        For i = 0 To aWeb.Length - 1
            vWeb &= CInt(aWeb(i)).ToString("00") & "."
        Next
        For i = 0 To aFic.Length - 1
            vApp &= CInt(aFic(i)).ToString("00") & "."
        Next

        If vWeb > vApp Then
            ' Hay una nueva versión en la web
            Return True
        Else
            ' Esta es la versión más reciente
            Return False
        End If

    End Function

    ''' <summary>
    ''' Comprueba la versión de la aplicación indicada y la compara con la indicada en versionActual.
    ''' Si la version en la web es mayor, devuelve 1, -1 si es menor, 0 si son iguales.
    ''' </summary>
    ''' <param name="aplicacion">Nombre de la aplicación a comprobar. 
    ''' Debería ser el valor devuelto por Application.ProductName.</param>
    ''' <param name="versionActual">La versión actual de la aplicación.
    ''' Debería ser la versión devuelta por FileVersion.</param>
    ''' <param name="laVersionWeb">Aquí asignará la versión que hay en la Web.</param>
    ''' <returns>Si la version en la web es mayor, devuelve 1, -1 si es menor, 0 si son iguales.</returns>
    Public Shared Function CompararVersionWeb(aplicacion As String, versionActual As String,
                                              ByRef laVersionWeb As String) As Integer
        ' Para comprobar las versiones

        Dim vWeb = VersionWeb(aplicacion)
        laVersionWeb = vWeb

        ' Por si no lee bien la versión
        If String.IsNullOrEmpty(vWeb) Then
            vWeb = "0.0.0.0"
        End If

        ' Para comprobar mejor las versiones de la Web (del AcercaDe usado en colorear código)
        ' Solo funcionará bien con valores de 1 cifra
        ' ya que 1.0.3.11 será menor que 1.0.3.9 aunque no sea así...
        ' Convertirlo en cadena de números de dos cifras

        Dim aWeb() = vWeb.Split("."c)

        Dim aFic() = versionActual.Split("."c)

        vWeb = ""
        Dim vApp = ""

        For i = 0 To aWeb.Length - 1
            vWeb &= CInt(aWeb(i)).ToString("00") & "."
        Next
        For i = 0 To aFic.Length - 1
            vApp &= CInt(aFic(i)).ToString("00") & "."
        Next

        ' Devolver 0 si son iguales, -1 si la de la web es menor o 1 si la de la web es mayor
        Return vWeb.CompareTo(vApp)

        'If vWeb > vApp Then
        '    ' Hay una nueva versión en la web
        '    Return 1
        'ElseIf vWeb < vApp Then
        '    ' Esta es la versión más reciente
        '    Return -1
        'Else
        '    ' Son iguales
        '    Return 0
        'End If

    End Function

    ''' <summary>
    ''' Comprueba la versión de la aplicación indicada y la compara con la indicada en versionActual.
    ''' Si la version en la web es mayor, devuelve 1, -1 si es menor, 0 si son iguales.
    ''' </summary>
    ''' <param name="ensamblado">Referencia al ensamblado a comprobar.
    ''' Usar: System.Reflection.Assembly.GetExecutingAssembly()</param>
    ''' <param name="laVersionWeb">Aquí asignará la versión que hay en la Web.</param>
    ''' <returns>Si la version en la web es mayor, devuelve 1, -1 si es menor, 0 si son iguales.</returns>
    Public Shared Function CompararVersionWeb(ensamblado As System.Reflection.Assembly,
                                              ByRef laVersionWeb As String) As Integer
        ' Para comprobar las versiones usando el ensamblado de llamada
        Dim fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ensamblado.Location)
        Return CompararVersionWeb(fvi.ProductName, fvi.FileVersion, laVersionWeb)
    End Function

End Class
