using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTab;
using DevExpress.XtraBars.Alerter;

namespace MomoFrameWork
{
    public partial class Momo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Momo()
        {
            InitializeComponent();
            LoadMenuTree();
        }

        #region 临时
        private int _num = 0;

        public int Num
        {
            get
            {
                _num++;
                return _num;
            }
        }
        private string xTitle
        {
            get
            {
                return "页面" + Num.ToString();
            }
        }

        #endregion

        #region TabControl控制区

        /// <summary>
        /// 添加页
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="form">窗体</param>
        public void AddPage(string Title, Form form)
        {
            try
            {
                //取消顶级属性
                form.TopLevel = false;
                //显示窗体
                form.Visible = true;
                //无边框最大化
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                //全屏
                form.Dock = DockStyle.Fill;
                XtraTabPage Xtb = new XtraTabPage();
                Xtb.Text = Title;
                Xtb.Controls.Add(form);
                MomoTabPanel.TabPages.Add(Xtb);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }
        /// <summary>
        /// 关闭选中页
        /// </summary>
        public void RemovePage()
        {
            MomoTabPanel.TabPages.Remove(MomoTabPanel.SelectedTabPage);
        }
        /// <summary>
        /// 关闭除选中Tab页
        /// </summary>
        public void RemoveOtherPage()
        {

            List<XtraTabPage> ls = new List<XtraTabPage>();

            foreach (XtraTabPage item in MomoTabPanel.TabPages)
            {
                if (MomoTabPanel.SelectedTabPage == item)
                {
                    continue;
                }
                ls.Add(item);
            }

            foreach (XtraTabPage item in ls)
            {
                MomoTabPanel.TabPages.Remove(item);
            }
        }
        /// <summary>
        /// 关闭所有Tab页
        /// </summary>
        public void RemoveAll()
        {
            MomoTabPanel.TabPages.Clear();
        }
        
        #endregion

        #region Ribbon控制区

        /// <summary>
        /// 添加Ribbon组页
        /// </summary>
        /// <param name="Text">标题</param>
        private void AddRibbonPageGroup(string Text)
        {
            RibbonPageCategory rpc = new RibbonPageCategory(Text, Color.White);
            rpc.Visible = true;
            MomoRibbon.PageCategories.Add(rpc);
        }
        /// <summary>
        /// 添加Ribbon页
        /// </summary>
        /// <param name="Text"></param>
        private void AddRibbonPage(string Text)
        {
            RibbonPage rp = new RibbonPage(Text);
            MomoRibbon.Pages.Add(rp);
        }
        /// <summary>
        /// 添加Ribbon组
        /// </summary>
        /// <param name="rp"></param>
        /// <param name="Text"></param>
        private void AddRibbonGroup(RibbonPage rp, string Text)
        {
            RibbonPageGroup rpg = new RibbonPageGroup(Text);
            rp.Groups.Add(rpg);
        }

        #endregion

        #region DockTool控制区
        //更改布局
        #endregion

        DataTable Dt = new DataTable("Menu");
        #region 菜单控制区
        MenuService ms = new MenuService();
        private void LoadMenuTree()
        {
            MomoMenuTree.ParentFieldName = "ParentID";
            MomoMenuTree.KeyFieldName = "ID";
          
            
            MomoMenuTree.DataSource = ms.GetMenus();
            MomoMenuTree.ExportToXml(@"C:\Data\sp.xml");
        }
            
        //菜单加载
        //菜单刷新
        //菜单点击
        #endregion

        #region 全局信息提示部分
        private void ErrorMessage(string message)
        {
            ShowMessage(message, MessageStyle.Error);
        }
        private void ErrorMessage(Exception exception)
        {

        }
        private void ShowMessage(string message, MessageStyle style)
        {
            AlertInfo ai = new AlertInfo("显示", message);
            switch (style)
            {
                case MessageStyle.Error:
                    MomoAlert.Show(this, "错误", message);
                    break;
                case MessageStyle.Option:
                    MomoAlert.Show(this, "选择", message);
                    break;
                case MessageStyle.Message:
                    MomoAlert.Show(this, "信息", message);
                    break;
                default:
                    break;
            }

        } 
        #endregion

       
    }
 
    public class MenuModel
    {
        #region 字段属性

        private int id;
        /// <summary>
        /// Gets or sets the menu ID.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int parentID;
        /// <summary>
        /// Gets or sets the parent ID.
        /// </summary>
        /// <value>The parent ID.</value>
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private int orderID;
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        /// <value>The order ID.</value>
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        private string menuName;
        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        /// <value>The name of the menu.</value>
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }

        #endregion

        public MenuModel() { }

        protected MenuModel(MenuModel model)
        {
            this.id = model.id;
            this.menuName = model.menuName;
            this.orderID = model.orderID;
            this.parentID = model.parentID;
        }
       
    }
    public class MenuService
    {
        public List<MenuModel> GetMenus()
        {
            //模拟在BLL获取数据
            List<MenuModel> list = new List<MenuModel>();
            MenuModel model1 = new MenuModel();
            model1.ID = 1;
            model1.MenuName = "首页";
            model1.OrderID = 1;
            model1.ParentID = 0;
            list.Add(model1);

            MenuModel model2 = new MenuModel();
            model2.ID = 2;
            model2.MenuName = "首页资讯";
            model2.OrderID = 1;
            model2.ParentID = 1;
            list.Add(model2);

            MenuModel model3 = new MenuModel();
            model3.ID = 3;
            model3.MenuName = "首页图片";
            model3.OrderID = 1;
            model3.ParentID = 0;
            list.Add(model3);

            MenuModel model4 = new MenuModel();
            model4.ID = 4;
            model4.MenuName = "首页置顶图片";
            model4.OrderID = 1;
            model4.ParentID = 3;
            list.Add(model4);

            MenuModel model5 = new MenuModel();
            model5.ID = 5;
            model5.MenuName = "分类管理";
            model5.OrderID = 2;
            model5.ParentID = 0;
            list.Add(model5);

            MenuModel model6 = new MenuModel();
            model6.ID = 6;
            model6.MenuName = "产品分类";
            model6.OrderID = 1;
            model6.ParentID = 5;
            list.Add(model6);

            MenuModel model7 = new MenuModel();
            model7.ID = 7;
            model7.MenuName = "品牌分类";
            model7.OrderID = 2;
            model7.ParentID = 5;
            list.Add(model7);

            MenuModel model8 = new MenuModel();
            model8.ID = 8;
            model8.MenuName = "关于我们";
            model8.OrderID = 3;
            model8.ParentID = 0;
            list.Add(model8);

            MenuModel model9 = new MenuModel();
            model9.ID = 9;
            model9.MenuName = "简要介绍";
            model9.OrderID = 1;
            model9.ParentID = 8;
            list.Add(model9);

            MenuModel model10 = new MenuModel();
            model10.ID = 10;
            model10.MenuName = "联系我们";
            model10.OrderID = 2;
            model10.ParentID = 8;
            list.Add(model10);

            MenuModel model11 = new MenuModel();
            model11.ID = 11;
            model11.MenuName = "加入我们";
            model11.OrderID = 3;
            model11.ParentID = 8;
            list.Add(model11);

            MenuModel model12 = new MenuModel();
            model12.ID = 12;
            model12.MenuName = "加盟";
            model12.OrderID = 1;
            model12.ParentID = 11;
            list.Add(model12);

            MenuModel model13 = new MenuModel();
            model13.ID = 13;
            model13.MenuName = "参加我们的团队";
            model13.OrderID = 2;
            model13.ParentID = 11;
            list.Add(model13);

            MenuModel model14 = new MenuModel();
            model14.ID = 14;
            model14.MenuName = "加盟条款";
            model14.OrderID = 1;
            model14.ParentID = 12;
            list.Add(model14);

            return list;
        }
    }
}