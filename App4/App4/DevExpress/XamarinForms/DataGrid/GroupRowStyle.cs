// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.XamarinForms.Core.Themes;
    using System;
    
    public class GroupRowStyle : StyleBase
    {
        public GroupRowStyle()
        {
            ThemeManager.Initialize(this);
        }
        
        public GroupRowStyle(IStyledElement styledElement) : base(styledElement)
        {
            ThemeManager.Initialize(this);
        }
        
        protected override StyleBase CreateCloneInstance() => 
            new GroupRowStyle();
        
        protected override void OnStylePropertyChanged()
        {
            IStyledElement styledElement = base.StyledElement;
            if (styledElement == null)
            {
                IStyledElement local1 = styledElement;
            }
            else
            {
                styledElement.OnGroupRowStyleChanged();
            }
        }
    }
}