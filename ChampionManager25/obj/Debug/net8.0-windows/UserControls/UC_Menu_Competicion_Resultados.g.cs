﻿#pragma checksum "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7E1F75FDDE5216A75413BCB2C0CB5DAD5AF5EDAA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ChampionManager25.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ChampionManager25.UserControls {
    
    
    /// <summary>
    /// UC_Menu_Competicion_Resultados
    /// </summary>
    public partial class UC_Menu_Competicion_Resultados : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ChampionManager25.UserControls.UC_Menu_Competicion_Resultados resultados;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel panelNavegadorCalendario;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnJornadaAnterior;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtJornadaActual;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnJornadaSiguiente;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridPartidos;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChampionManager25;component/usercontrols/uc_menu_competicion_resultados.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.resultados = ((ChampionManager25.UserControls.UC_Menu_Competicion_Resultados)(target));
            
            #line 7 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
            this.resultados.Loaded += new System.Windows.RoutedEventHandler(this.resultados_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.panelNavegadorCalendario = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 3:
            this.btnJornadaAnterior = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
            this.btnJornadaAnterior.Click += new System.Windows.RoutedEventHandler(this.btnJornadaAnterior_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtJornadaActual = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.btnJornadaSiguiente = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\UserControls\UC_Menu_Competicion_Resultados.xaml"
            this.btnJornadaSiguiente.Click += new System.Windows.RoutedEventHandler(this.btnJornadaSiguiente_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.gridPartidos = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

