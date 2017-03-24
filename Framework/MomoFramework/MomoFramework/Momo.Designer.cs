namespace MomoFrameWork
{
    partial class Momo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MomoRibbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.MomoAppMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.nima = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.bsMessage = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.MomoTabPanel = new DevExpress.XtraTab.XtraTabControl();
            this.MomoDockMgr = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.MomoDockMgr_Menu = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.MomoMenuTree = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.MomoAlert = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MomoRibbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomoAppMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MomoTabPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomoDockMgr)).BeginInit();
            this.MomoDockMgr_Menu.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MomoMenuTree)).BeginInit();
            this.SuspendLayout();
            // 
            // MomoRibbon
            // 
            this.MomoRibbon.ApplicationButtonDropDownControl = this.MomoAppMenu;
            this.MomoRibbon.ApplicationButtonText = null;
            this.MomoRibbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.nima,
            this.barStaticItem1,
            this.bsMessage,
            this.barEditItem1});
            this.MomoRibbon.Location = new System.Drawing.Point(0, 0);
            this.MomoRibbon.MaxItemId = 24;
            this.MomoRibbon.Name = "MomoRibbon";
            this.MomoRibbon.PageHeaderItemLinks.Add(this.barEditItem1);
            this.MomoRibbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.MomoRibbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.MomoRibbon.SelectedPage = this.ribbonPage1;
            this.MomoRibbon.Size = new System.Drawing.Size(796, 147);
            this.MomoRibbon.StatusBar = this.ribbonStatusBar;
            // 
            // MomoAppMenu
            // 
            this.MomoAppMenu.BottomPaneControlContainer = null;
            this.MomoAppMenu.Name = "MomoAppMenu";
            this.MomoAppMenu.Ribbon = this.MomoRibbon;
            this.MomoAppMenu.RightPaneControlContainer = null;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 9;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // nima
            // 
            this.nima.Caption = "barButtonItem2";
            this.nima.Id = 10;
            this.nima.Name = "nima";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "状态 :";
            this.barStaticItem1.Id = 18;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsMessage
            // 
            this.bsMessage.Caption = "[信息]";
            this.bsMessage.Id = 19;
            this.bsMessage.Name = "bsMessage";
            this.bsMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem1
            // 
            this.barEditItem1.Edit = this.repositoryItemButtonEdit1;
            this.barEditItem1.Id = 21;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.Width = 130;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "标准";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.ItemLinks.Add(this.bsMessage);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 441);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.MomoRibbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(796, 26);
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.Controls.Add(this.MomoTabPanel);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(168, 147);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(628, 294);
            this.clientPanel.TabIndex = 2;
            // 
            // MomoTabPanel
            // 
            this.MomoTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MomoTabPanel.Location = new System.Drawing.Point(0, 0);
            this.MomoTabPanel.Name = "MomoTabPanel";
            this.MomoTabPanel.Size = new System.Drawing.Size(628, 294);
            this.MomoTabPanel.TabIndex = 0;
            // 
            // MomoDockMgr
            // 
            this.MomoDockMgr.Form = this;
            this.MomoDockMgr.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.MomoDockMgr_Menu});
            this.MomoDockMgr.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // MomoDockMgr_Menu
            // 
            this.MomoDockMgr_Menu.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.MomoDockMgr_Menu.Appearance.Options.UseBackColor = true;
            this.MomoDockMgr_Menu.Controls.Add(this.dockPanel1_Container);
            this.MomoDockMgr_Menu.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.MomoDockMgr_Menu.DockVertical = DevExpress.Utils.DefaultBoolean.True;
            this.MomoDockMgr_Menu.FloatVertical = true;
            this.MomoDockMgr_Menu.ID = new System.Guid("3706dee0-e337-40c0-80ac-9218d5acc32b");
            this.MomoDockMgr_Menu.Location = new System.Drawing.Point(0, 147);
            this.MomoDockMgr_Menu.Name = "MomoDockMgr_Menu";
            this.MomoDockMgr_Menu.Options.AllowDockBottom = false;
            this.MomoDockMgr_Menu.Options.AllowDockFill = false;
            this.MomoDockMgr_Menu.Options.AllowDockTop = false;
            this.MomoDockMgr_Menu.Options.AllowFloating = false;
            this.MomoDockMgr_Menu.Options.FloatOnDblClick = false;
            this.MomoDockMgr_Menu.Options.ShowCloseButton = false;
            this.MomoDockMgr_Menu.Options.ShowMaximizeButton = false;
            this.MomoDockMgr_Menu.OriginalSize = new System.Drawing.Size(168, 168);
            this.MomoDockMgr_Menu.Size = new System.Drawing.Size(168, 294);
            this.MomoDockMgr_Menu.Text = "菜单";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.MomoMenuTree);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(162, 266);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // MomoMenuTree
            // 
            this.MomoMenuTree.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MomoMenuTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.MomoMenuTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MomoMenuTree.DragExpandDelay = 5;
            this.MomoMenuTree.DragNodesMode = DevExpress.XtraTreeList.TreeListDragNodesMode.Standard;
            this.MomoMenuTree.Location = new System.Drawing.Point(0, 0);
            this.MomoMenuTree.Name = "MomoMenuTree";
            this.MomoMenuTree.BeginUnboundLoad();
            this.MomoMenuTree.AppendNode(new object[] {
            "213"}, -1);
            this.MomoMenuTree.AppendNode(new object[] {
            "房东"}, 0);
            this.MomoMenuTree.AppendNode(new object[] {
            "房东a"}, 1);
            this.MomoMenuTree.AppendNode(new object[] {
            "房东 "}, 2);
            this.MomoMenuTree.AppendNode(new object[] {
            "房东3"}, 0);
            this.MomoMenuTree.AppendNode(new object[] {
            "房东"}, 4);
            this.MomoMenuTree.AppendNode(new object[] {
            "房东1"}, 0);
            this.MomoMenuTree.EndUnboundLoad();
            this.MomoMenuTree.OptionsBehavior.Editable = false;
            this.MomoMenuTree.OptionsLayout.AddNewColumns = false;
            this.MomoMenuTree.OptionsView.ShowColumns = false;
            this.MomoMenuTree.OptionsView.ShowHorzLines = false;
            this.MomoMenuTree.OptionsView.ShowVertLines = false;
            this.MomoMenuTree.Size = new System.Drawing.Size(162, 266);
            this.MomoMenuTree.TabIndex = 0;
            this.MomoMenuTree.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "321";
            this.treeListColumn1.FieldName = "MenuName";
            this.treeListColumn1.MinWidth = 74;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 147;
            // 
            // MomoAlert
            // 
            this.MomoAlert.AutoFormDelay = 3000;
            this.MomoAlert.FormDisplaySpeed = DevExpress.XtraBars.Alerter.AlertFormDisplaySpeed.Slow;
            this.MomoAlert.ShowPinButton = false;
            this.MomoAlert.ShowToolTips = false;
            // 
            // Momo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 467);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.MomoDockMgr_Menu);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.MomoRibbon);
            this.Name = "Momo";
            this.Ribbon = this.MomoRibbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Momo";
            ((System.ComponentModel.ISupportInitialize)(this.MomoRibbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomoAppMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MomoTabPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomoDockMgr)).EndInit();
            this.MomoDockMgr_Menu.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MomoMenuTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl MomoRibbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu MomoAppMenu;
        private DevExpress.XtraBars.Docking.DockManager MomoDockMgr;
        private DevExpress.XtraTab.XtraTabControl MomoTabPanel;
        private DevExpress.XtraBars.Docking.DockPanel MomoDockMgr_Menu;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem nima;
        private DevExpress.XtraTreeList.TreeList MomoMenuTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraBars.Alerter.AlertControl MomoAlert;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem bsMessage;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}