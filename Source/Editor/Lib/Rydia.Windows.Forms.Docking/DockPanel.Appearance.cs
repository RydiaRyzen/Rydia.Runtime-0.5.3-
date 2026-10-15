using WeifenLuo.WinFormsUI.Docking.Skins;
using System.ComponentModel;

namespace WeifenLuo.WinFormsUI.Docking
{
    public partial class DockPanel
    {
        private DockPanelSkin m_dockPanelSkin = DockPanelSkinBuilder.Create(Style.VisualStudio2005);
        [LocalizedCategory("Category_Docking")]
        [LocalizedDescription("DockPanel_DockPanelSkin")]
        public DockPanelSkin Skin
        {
            get { return this.m_dockPanelSkin;  }
            set { this.m_dockPanelSkin = value; }
        }
        
        private Style m_dockPanelSkinStyle = Style.VisualStudio2005;
        [LocalizedCategory("Category_Docking")]
        [LocalizedDescription("DockPanel_DockPanelSkinStyle")]
        [DefaultValue(Style.VisualStudio2005)]
        public Style SkinStyle
        {
            get { return this.m_dockPanelSkinStyle; }
            set
            {
                if (this.m_dockPanelSkinStyle == value)
                    return;

				this.m_dockPanelSkinStyle = value;

				Skin = DockPanelSkinBuilder.Create(this.m_dockPanelSkinStyle);
            }
        }
    }
}
