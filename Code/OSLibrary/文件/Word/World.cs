#region �½�Word�ĵ�
 /// 
/// ��̬����Word�ĵ����������
/// 
/// �ĵ�Ŀ¼
/// �ĵ���
 /// �����Զ�����Ϣ
public static bool CreateWordFile(string dir, string fileName)
 {
 try
 {
 Object oMissing = System.Reflection.Missing.Value;

 if (!Directory.Exists(dir))
 //�����ļ�����Ŀ¼
 Directory.CreateDirectory(dir);
 }
 //����Word�ĵ�(Microsoft.Office.Interop.Word)
 Microsoft.Office.Interop.Word._Application WordApp = new Application();
 WordApp.Visible = true;
 Microsoft.Office.Interop.Word._Document WordDoc = WordApp.Documents.Add(
 ref oMissing, ref oMissing, ref oMissing, ref oMissing);

 //����
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
 #endregion �½�Word�ĵ�
 
 
#region ��word�ĵ����ҳüҳ��
 /// 
/// ��word�ĵ����ҳü
/// 
/// �ļ���
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

 ////���ҳü����һ��
 //WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
 //WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
 //WordApp.ActiveWindow.ActivePane.Selection.InsertAfter( "**��˾" );//ҳü����

 ////���ҳü��������
 if (WordApp.ActiveWindow.ActivePane.View.Type == WdViewType.wdNormalView ||
 WordApp.ActiveWindow.ActivePane.View.Type == WdViewType.wdOutlineView)
 {
 WordApp.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
 }
 WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;
 WordApp.Selection.HeaderFooter.LinkToPrevious = false;
 WordApp.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
 WordApp.Selection.HeaderFooter.Range.Text = "ҳü����";

 WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageFooter;
 WordApp.Selection.HeaderFooter.LinkToPrevious = false;
 WordApp.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
 WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("ҳ������");

 //����ҳüҳ������
 WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;

 //����
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
 #endregion ��word�ĵ����ҳüҳ��
 
 
#region �����ĵ���ʽ������ı����ݡ�������
 /// 
/// �����ĵ���ʽ���������
/// 
/// �ļ���
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

 //���þ���
 WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

 //�����ĵ����м��
 WordApp.Selection.ParagraphFormat.LineSpacing = 15f;
 //�������
 //WordApp.Selection.TypeParagraph();
 Microsoft.Office.Interop.Word.Paragraph para;
 para = WordDoc.Content.Paragraphs.Add(ref oMissing);
 //������ʽ
 para.Range.Text = "This is paragraph 1";
 //para.Range.Font.Bold = 2;
 //para.Range.Font.Color = WdColor.wdColorRed;
 //para.Range.Font.Italic = 2;
 para.Range.InsertParagraphAfter();

 para.Range.Text = "This is paragraph 2";
 para.Range.InsertParagraphAfter();

 //����Hyperlink
 Microsoft.Office.Interop.Word.Selection mySelection = WordApp.ActiveWindow.Selection;
 mySelection.Start = 9999;
 mySelection.End = 9999;
 Microsoft.Office.Interop.Word.Range myRange = mySelection.Range;

 Microsoft.Office.Interop.Word.Hyperlinks myLinks = WordDoc.Hyperlinks;
 object linkAddr = @"http://www.cnblogs.com/lantionzy";
 Microsoft.Office.Interop.Word.Hyperlink myLink = myLinks.Add(myRange, ref linkAddr,
 ref oMissing);
 WordApp.ActiveWindow.Selection.InsertAfter("\n");

 //���
 WordDoc.Paragraphs.Last.Range.Text = "�ĵ�����ʱ�䣺" + DateTime.Now.ToString();
 WordDoc.Paragraphs.Last.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

 //����
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
 #endregion �����ĵ���ʽ������ı����ݡ�������
 
 
#region �ĵ������ͼƬ
 /// 
/// �ĵ������ͼƬ
/// 
/// word�ļ���
/// picture�ļ���
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

 //�ƶ�����ĵ�ĩβ
 object count = WordDoc.Paragraphs.Count;
 object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdParagraph;
 WordApp.Selection.MoveDown(ref WdLine, ref count, ref oMissing);//�ƶ�����
 WordApp.Selection.TypeParagraph();//�������

 object LinkToFile = false;
 object SaveWithDocument = true;
 object Anchor = WordDoc.Application.Selection.Range;
 WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(picPath, ref LinkToFile, ref SaveWithDocument, ref Anchor);

 //����
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
 #endregion �ĵ������ͼƬ
 
 
#region ��������������ø�ʽ��������ݣ�
 /// 
/// �����
/// 
/// word�ļ���
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

 //������
 Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, 12, 3, ref oMissing, ref oMissing);
 //���ñ��
 newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
 newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
 newTable.Columns[1].Width = 100f;
 newTable.Columns[2].Width = 220f;
 newTable.Columns[3].Width = 105f;

 //���������
 newTable.Cell(1, 1).Range.Text = "�ҵļ���";
 //���õ�Ԫ��������Ϊ����
 newTable.Cell(1, 1).Range.Bold = 2;

 //�ϲ���Ԫ��
 newTable.Cell(1, 1).Merge(newTable.Cell(1, 3));

 //��ֱ����
 WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
 //ˮƽ����
 WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

 //���������
 newTable.Cell(2, 1).Range.Text = "��������";
 //���õ�Ԫ����������ɫ
 newTable.Cell(2, 1).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorDarkBlue;
 //�ϲ���Ԫ��
 newTable.Cell(2, 1).Merge(newTable.Cell(2, 3));
 WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

 //���������
 newTable.Cell(3, 1).Range.Text = "������";
 newTable.Cell(3, 2).Range.Text = "����";
 //����ϲ���Ԫ��
 newTable.Cell(3, 3).Select();
 //ѡ��һ��
 object moveUnit = Microsoft.Office.Interop.Word.WdUnits.wdLine;
 object moveCount = 3;
 object moveExtend = Microsoft.Office.Interop.Word.WdMovementType.wdExtend;
 WordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
 WordApp.Selection.Cells.Merge();

 //����в���ͼƬ
 string pictureFileName = System.IO.Directory.GetCurrentDirectory() + @"\picture.jpg";
 object LinkToFile = false;
 object SaveWithDocument = true;
 object Anchor = WordDoc.Application.Selection.Range;
 WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(pictureFileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
 //ͼƬ���
 WordDoc.Application.ActiveDocument.InlineShapes[1].Width = 100f;
 //ͼƬ�߶�
 WordDoc.Application.ActiveDocument.InlineShapes[1].Height = 100f;
 //��ͼƬ����Ϊ���ܻ�����
 Microsoft.Office.Interop.Word.Shape s = WordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
 s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;

 newTable.Cell(12, 1).Range.Text = "��ע��";
 newTable.Cell(12, 1).Merge(newTable.Cell(12, 3));
 //�ڱ����������
 WordDoc.Content.Tables[1].Rows.Add(ref oMissing);

 //����
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
 #endregion #region �����
 
 
#region ��Word�ĵ�ת��ΪHtml�ļ�
 /// 
/// ��Word�ĵ�ת��ΪHtml�ļ�
/// 
/// word�ļ���
/// Ҫ�����html�ļ���
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
 // ���ļ�
 Type docsType = WordApp.Documents.GetType();
 // ת����ʽ�����Ϊ
 Type docType = WordDoc.GetType();
 object saveFileName = htmlFileName;
 docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc,
 new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML });


 #region ������ʽ��
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
 // �˳� Word
 //wordType.InvokeMember( "Quit", System.Reflection.BindingFlags.InvokeMethod,
 // null, WordApp, null );
 #endregion


 //����
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
 #endregion ��Word�ĵ�ת��ΪHtml�ļ�