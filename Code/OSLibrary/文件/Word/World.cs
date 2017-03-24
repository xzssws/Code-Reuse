#region 新建Word文档
 /// 
/// 动态生成Word文档并填充内容
/// 
/// 文档目录
/// 文档名
 /// 返回自定义信息
public static bool CreateWordFile(string dir, string fileName)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;

 if (!Directory.Exists(dir))
 //创建文件所在目录
 Directory.CreateDirectory(dir);
 }
 //创建Word文档(Microsoft.Office.Interop.Word)
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Add(
 ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 //保存
 object filename = dir + fileName;
 WordDoc.SaveAs(ref filename, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
 WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
 WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
 return true;
 }
 catch (Exception e)
 {
 Console.WriteLine(e.Message);
 Console.WriteLine(e.StackTrace);
 return false;
 }
 }
 #endregion 新建Word文档
 
 
#region 给word文档添加页眉页脚
 /// 
/// 给word文档添加页眉
/// 
/// 文件名
 /// 
public static bool AddPageHeaderFooter(string filePath)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 object filename = filePath;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Open(ref filename, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 ////添加页眉方法一：
 //WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
 //WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
 //WordApp.ActiveWindow.ActivePane.Selection.InsertAfter( "**公司" );//页眉内容

 ////添加页眉方法二：
 if (WordApp.ActiveWindow.ActivePane.View.Type == WdViewType.wdNormalView ||
 WordApp.ActiveWindow.ActivePane.View.Type == WdViewType.wdOutlineView)
 {
 WordApp.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
 }
 WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;
 WordApp.Selection.HeaderFooter.LinkToPrevious = false;
 WordApp.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
 WordApp.Selection.HeaderFooter.Range.Text = "页眉内容";

 WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageFooter;
 WordApp.Selection.HeaderFooter.LinkToPrevious = false;
 WordApp.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
 WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("页脚内容");

 //跳出页眉页脚设置
 WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;

 //保存
 WordDoc.Save();
 WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
 WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
 return true;
 }
 catch (Exception e)
 {
 Console.WriteLine(e.Message);
 Console.WriteLine(e.StackTrace);
 return false;
 }
 }
 #endregion 给word文档添加页眉页脚
 
 
#region 设置文档格式并添加文本内容、超链接
 /// 
/// 设置文档格式并添加内容
/// 
/// 文件名
 /// 
public static bool AddContent(string filePath)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 object filename = filePath;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Open(ref filename, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 //设置居左
 WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

 //设置文档的行间距
 WordApp.Selection.ParagraphFormat.LineSpacing = 15f;
 //插入段落
 //WordApp.Selection.TypeParagraph();
 Microsoft.Office.Interop.Word.Paragraph para;
 para = WordDoc.Content.Paragraphs.Add(ref oMissing);
 //正常格式
 para.Range.Text = "This is paragraph 1";
 //para.Range.Font.Bold = 2;
 //para.Range.Font.Color = WdColor.wdColorRed;
 //para.Range.Font.Italic = 2;
 para.Range.InsertParagraphAfter();

 para.Range.Text = "This is paragraph 2";
 para.Range.InsertParagraphAfter();

 //插入Hyperlink
 Microsoft.Office.Interop.Word.Selection mySelection = WordApp.ActiveWindow.Selection;
 mySelection.Start = 9999;
 mySelection.End = 9999;
 Microsoft.Office.Interop.Word.Range myRange = mySelection.Range;

 Microsoft.Office.Interop.Word.Hyperlinks myLinks = WordDoc.Hyperlinks;
 object linkAddr = @"http://www.cnblogs.com/lantionzy";
 Microsoft.Office.Interop.Word.Hyperlink myLink = myLinks.Add(myRange, ref linkAddr,
 ref oMissing);
 WordApp.ActiveWindow.Selection.InsertAfter("\n");

 //落款
 WordDoc.Paragraphs.Last.Range.Text = "文档创建时间：" + DateTime.Now.ToString();
 WordDoc.Paragraphs.Last.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

 //保存
 WordDoc.Save();
 WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
 WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
 return true;
 }
 catch (Exception e)
 {
 Console.WriteLine(e.Message);
 Console.WriteLine(e.StackTrace);
 return false;
 }
 }
 #endregion 设置文档格式并添加文本内容、超链接
 
 
#region 文档中添加图片
 /// 
/// 文档中添加图片
/// 
/// word文件名
/// picture文件名
 /// 
public static bool AddPicture(string filePath, string picPath)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 object filename = filePath;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Open(ref filename, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 //移动光标文档末尾
 object count = WordDoc.Paragraphs.Count;
 object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdParagraph;
 WordApp.Selection.MoveDown(ref WdLine, ref count, ref oMissing);//移动焦点
 WordApp.Selection.TypeParagraph();//插入段落

 object LinkToFile = false;
 object SaveWithDocument = true;
 object Anchor = WordDoc.Application.Selection.Range;
 WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(picPath, ref LinkToFile, ref SaveWithDocument, ref Anchor);

 //保存
 WordDoc.Save();
 WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
 WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
 return true;
 }
 catch (Exception e)
 {
 Console.WriteLine(e.Message);
 Console.WriteLine(e.StackTrace);
 return false;
 }
 }
 #endregion 文档中添加图片
 
 
#region 表格处理（插入表格、设置格式、填充内容）
 /// 
/// 表格处理
/// 
/// word文件名
 /// 
public static bool AddTable(string filePath)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 object filename = filePath;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Open(ref filename, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 //插入表格
 Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, 12, 3, ref oMissing, ref oMissing);
 //设置表格
 newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
 newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
 newTable.Columns[1].Width = 100f;
 newTable.Columns[2].Width = 220f;
 newTable.Columns[3].Width = 105f;

 //填充表格内容
 newTable.Cell(1, 1).Range.Text = "我的简历";
 //设置单元格中字体为粗体
 newTable.Cell(1, 1).Range.Bold = 2;

 //合并单元格
 newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));

 //垂直居中
 WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
 //水平居中
 WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

 //填充表格内容
 newTable.Cell(2, 1).Range.Text = "座右铭：";
 //设置单元格内字体颜色
 newTable.Cell(2, 1).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorDarkBlue;
 //合并单元格
 newTable.Cell(2, 1).Merge(newTable.Cell(2, 3));
 WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

 //填充表格内容
 newTable.Cell(3, 1).Range.Text = "姓名：";
 newTable.Cell(3, 2).Range.Text = "雷鑫";
 //纵向合并单元格
 newTable.Cell(3, 3).Select();
 //选中一行
 object moveUnit = Microsoft.Office.Interop.Word.WdUnits.wdLine;
 object moveCount = 3;
 object moveExtend = Microsoft.Office.Interop.Word.WdMovementType.wdExtend;
 WordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
 WordApp.Selection.Cells.Merge();

 //表格中插入图片
 string pictureFileName = System.IO.Directory.GetCurrentDirectory() + @"\picture.jpg";
 object LinkToFile = false;
 object SaveWithDocument = true;
 object Anchor = WordDoc.Application.Selection.Range;
 WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(pictureFileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
 //图片宽度
 WordDoc.Application.ActiveDocument.InlineShapes[1].Width = 100f;
 //图片高度
 WordDoc.Application.ActiveDocument.InlineShapes[1].Height = 100f;
 //将图片设置为四周环绕型
 Microsoft.Office.Interop.Word.Shape s = WordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
 s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;

 newTable.Cell(12, 1).Range.Text = "备注：";
 newTable.Cell(12, 1).Merge(newTable.Cell(12, 3));
 //在表格中增加行
 WordDoc.Content.Tables[1].Rows.Add(ref oMissing);

 //保存
 WordDoc.Save();
 WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
 WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
 return true;
 }
 catch (Exception e)
 {
 Console.WriteLine(e.Message);
 Console.WriteLine(e.StackTrace);
 return false;
 }
 }
 #endregion #region 表格处理
 
 
#region 把Word文档转化为Html文件
 /// 
/// 把Word文档转化为Html文件
/// 
/// word文件名
/// 要保存的html文件名
 /// 
public static bool WordToHtml(string wordFileName, string htmlFileName)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 object filename = wordFileName;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Open(ref filename, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 // Type wordType = WordApp.GetType();
 // 打开文件
 Type docsType = WordApp.Documents.GetType();
 // 转换格式，另存为
 Type docType = WordDoc.GetType();
 object saveFileName = htmlFileName;
 docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc,
 new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML });


 #region 其它格式：
 ///wdFormatHTML
 ///wdFormatDocument
 ///wdFormatDOSText
 ///wdFormatDOSTextLineBreaks
 ///wdFormatEncodedText
 ///wdFormatRTF
 ///wdFormatTemplate
 ///wdFormatText
 ///wdFormatTextLineBreaks
 ///wdFormatUnicodeText
 // 退出 Word
 //wordType.InvokeMember( "Quit", System.Reflection.BindingFlags.InvokeMethod,
 // null, WordApp, null );
 #endregion


 //保存
 WordDoc.Save();
 WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
 WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
 return true;
 }
 catch (Exception e)
 {
 Console.WriteLine(e.Message);
 Console.WriteLine(e.StackTrace);
 return false;
 }
 }
 #endregion 把Word文档转化为Html文件