'
' Código convertido de C# a Visual Basic con CSharpToVB de Paul1956
'

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Namespace Reloj_Windows.Properties


    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")>
    Friend NotInheritable Partial Class Settings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        Private Shared defaultInstance As Settings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New Settings), Settings)
        Public Shared ReadOnly Property [Default] As Settings
            Get
                Return defaultInstance
            End Get
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property RecordarPos As Boolean
            Get
                Return (CBool(Me("RecordarPos")))
            End Get

            Set(Value As Boolean)
                Me("RecordarPos") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("-1")>
        Public Property AcoplarDonde As Integer
            Get
                Return (CInt(Fix(Me("AcoplarDonde"))))
            End Get

            Set(Value As Integer)
                Me("AcoplarDonde") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("100")>
        Public Property Opacidad As Double
            Get
                Return (CDbl(Me("Opacidad")))
            End Get

            Set(Value As Double)
                Me("Opacidad") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("-1")>
        Public Property vLeft As Integer
            Get
                Return (CInt(Fix(Me("vLeft"))))
            End Get

            Set(Value As Integer)
                Me("vLeft") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("-1")>
        Public Property vTop As Integer
            Get
                Return (CInt(Fix(Me("vTop"))))
            End Get

            Set(Value As Integer)
                Me("vTop") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("-1")>
        Public Property vWidth As Integer
            Get
                Return (CInt(Fix(Me("vWidth"))))
            End Get

            Set(Value As Integer)
                Me("vWidth") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("-1")>
        Public Property vHeight As Integer
            Get
                Return (CInt(Fix(Me("vHeight"))))
            End Get

            Set(Value As Integer)
                Me("vHeight") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("False")>
        Public Property SiempreEncima As Boolean
            Get
                Return (CBool(Me("SiempreEncima")))
            End Get

            Set(Value As Boolean)
                Me("SiempreEncima") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property AcoplarMinimo As Boolean
            Get
                Return (CBool(Me("AcoplarMinimo")))
            End Get

            Set(Value As Boolean)
                Me("AcoplarMinimo") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("False")>
        Public Property UsarComoSalvaPantalla As Boolean
            Get
                Return (CBool(Me("UsarComoSalvaPantalla")))
            End Get

            Set(Value As Boolean)
                Me("UsarComoSalvaPantalla") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("False")>
        Public Property AcoplarTransparente As Boolean
            Get
                Return (CBool(Me("AcoplarTransparente")))
            End Get

            Set(Value As Boolean)
                Me("AcoplarTransparente") = value
            End Set
        End Property
        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("0.75")>
        Public Property OpacidadAcopleySalvaP As Double
            Get
                Return (CDbl(Me("OpacidadAcopleySalvaP")))
            End Get

            Set(Value As Double)
                Me("OpacidadAcopleySalvaP") = value
            End Set
        End Property
    End Class
End Namespace
