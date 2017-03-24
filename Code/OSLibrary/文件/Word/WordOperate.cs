using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Web;
using System.Data;
using System.Reflection;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Net;
namespace OfficeOperate
{
    public class WordOperate
    {
        #region ��̬����Word�ĵ����������
        ///
        /// ��̬����Word�ĵ���������� 
        ///
        /// �����Զ�����Ϣ
        public static string CreateWordFile()
        {
            string message = "";
            try
            {
                Object oMissing = System.Reflection.Missing.Value;
                string dir = System.Web.HttpContext.Current.Server.MapPath( "" );//������������using System.web������
                if( !Directory.Exists( dir + "\\file" ) )
                {
                    Directory.CreateDirectory( dir + "\\file" );  //�����ļ�����Ŀ¼
                }
                string name = DateTime.Now.ToLongDateString() + ".doc";
                object filename = dir + "\\file" + name;  //�ļ�����·��
                //����Word�ĵ�
                Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
                Microsoft.Office.Interop.Word.Document WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
                ////���ҳü����һ��
                //WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
                //WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
                //WordApp.ActiveWindow.ActivePane.Selection.InsertAfter( "Something here" );//ҳü����
                //���ҳü��������
                if( WordApp.ActiveWindow.ActivePane.View.Type == Microsoft.Office.Interop.Word.WdViewType.wdNormalView || WordApp.ActiveWindow.ActivePane.View.Type == Microsoft.Office.Interop.Word.WdViewType.wdOutlineView )
                {
                    WordApp.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;
                }
                WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageHeader;
                string sHeader = "ҳü����";
                WordApp.Selection.HeaderFooter.LinkToPrevious = false;
                WordApp.Selection.HeaderFooter.Range.Text = sHeader;
                WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;

                //WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;//�����Ҷ���
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;//���������   
                WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;//����ҳü����
                WordApp.Selection.ParagraphFormat.LineSpacing = 15f;//�����ĵ����м��
                //�ƶ����㲢����
                object count = 14;
                object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdLine;//��һ��;
                WordApp.Selection.MoveDown( ref WdLine, ref count, ref oMissing );//�ƶ�����
                WordApp.Selection.TypeParagraph();//�������
                //�ĵ��д������
                Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables.Add( WordApp.Selection.Range, 12, 3, ref oMissing, ref oMissing );
                //���ñ����ʽ
                newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                newTable.Columns[1].Width = 100f;
                newTable.Columns[2].Width = 220f;
                newTable.Columns[3].Width = 105f;
                //���������
                newTable.Cell( 1, 1 ).Range.Text = "��Ʒ��ϸ��Ϣ��";
                newTable.Cell( 1, 1 ).Range.Bold = 2;//���õ�Ԫ��������Ϊ����
                //�ϲ���Ԫ��
                newTable.Cell( 1, 1 ).Merge( newTable.Cell( 1, 3 ) );
                WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//��ֱ����
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//ˮƽ����
                //���������
                newTable.Cell( 2, 1 ).Range.Text = "��Ʒ������Ϣ";
                newTable.Cell( 2, 1 ).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorDarkBlue;//���õ�Ԫ����������ɫ
                //�ϲ���Ԫ��
                newTable.Cell( 2, 1 ).Merge( newTable.Cell( 2, 3 ) );
                WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                //���������
                newTable.Cell( 3, 1 ).Range.Text = "Ʒ�����ƣ�";
                newTable.Cell( 3, 2 ).Range.Text = "BrandName";
                //����ϲ���Ԫ��
                newTable.Cell( 3, 3 ).Select();//ѡ��һ��
                object moveUnit = Microsoft.Office.Interop.Word.WdUnits.wdLine;
                object moveCount = 5;
                object moveExtend = Microsoft.Office.Interop.Word.WdMovementType.wdExtend;
                WordApp.Selection.MoveDown( ref moveUnit, ref moveCount, ref moveExtend );
                WordApp.Selection.Cells.Merge();
                //����ͼƬ
                if( File.Exists( System.Web.HttpContext.Current.Server.MapPath( "images//picture.jpg" ) ) )
                {
                    string FileName = System.Web.HttpContext.Current.Server.MapPath( "images//picture.jpg" );//ͼƬ����·��
                    object LinkToFile = false;
                    object SaveWithDocument = true;
                    object Anchor = WordDoc.Application.Selection.Range;
                    WordDoc.Application.ActiveDocument.InlineShapes.AddPicture( FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor );
                    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = 100f;//ͼƬ���
                    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = 100f;//ͼƬ�߶�
                }
                //��ͼƬ����Ϊ���ܻ�����
                Microsoft.Office.Interop.Word.Shape s = WordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
                s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;
                newTable.Cell( 12, 1 ).Range.Text = "��Ʒ��������";
                newTable.Cell( 12, 1 ).Merge( newTable.Cell( 12, 3 ) );
                //�ڱ����������
                WordDoc.Content.Tables[1].Rows.Add( ref oMissing );
                WordDoc.Paragraphs.Last.Range.Text = "�ĵ�����ʱ�䣺" + DateTime.Now.ToString();//����
                WordDoc.Paragraphs.Last.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                //�ļ�����
                WordDoc.SaveAs( ref filename, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing );
                WordDoc.Close( ref oMissing, ref oMissing, ref oMissing );
                WordApp.Quit( ref oMissing, ref oMissing, ref oMissing );
                message = name + "�ĵ����ɳɹ�";
            }
            catch
            {
                message = "�ļ������쳣��";
            }
            return message;
        }
        #endregion       
        #region ��������һ���յ�word�ĵ����б༭
        ///
        /// ��������һ���յ�word�ĵ����б༭
        ///
        public static void OpenNewWordFileToEdit()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
        }
        #endregion
        #region ����word�ĵ�
        ///
        /// ����word�ĵ�
        ///
        ///
        public static string createWord()
        {
            Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            Document WordDoc;
            string strContent = "";
            object strFileName = System.Web.HttpContext.Current.Server.MapPath( "test.doc " );
            if( System.IO.File.Exists( (string)strFileName ) )
                System.IO.File.Delete( (string)strFileName );
            Object oMissing = System.Reflection.Missing.Value;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            #region   �����ݿ��ж�ȡ������д�뵽word�ļ���
            strContent = "���\n\n\r ";
            WordDoc.Paragraphs.Last.Range.Text = strContent;
            strContent = "���ǲ��Գ��� ";
            WordDoc.Paragraphs.Last.Range.Text = strContent;
            #endregion
            //��WordDoc�ĵ���������ݱ���ΪDOC�ĵ�   
            WordDoc.SaveAs( ref strFileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref   oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            //�ر�WordDoc�ĵ�����   
            WordDoc.Close( ref oMissing, ref oMissing, ref oMissing );
            //�ر�WordApp�������   
            WordApp.Quit( ref oMissing, ref oMissing, ref oMissing );
            string message = strFileName + "\r\n " + "�����ɹ� ";
            return message;
        }
        #endregion
        #region ��Word�ĵ�װ��ΪHtml�ļ�
        ///
        /// ��Word�ĵ�װ��ΪHtml�ļ�
        ///
        /// Ҫת����Word�ĵ�
        public static void WordToHtml( string strFileName )
        {
            string saveFileName = strFileName + DateTime.Now.ToString( "yyyy-MM-dd-HH-mm-ss" ) + ".html";
            WordToHtml( strFileName, saveFileName );
        }
        ///
        /// ��Word�ĵ�װ��ΪHtml�ļ�
        ///
        /// Ҫת����Word�ĵ�
        /// Ҫ���ɵľ����Htmlҳ��
        public static void WordToHtml( string strFileName, string strSaveFileName )
        {
            Microsoft.Office.Interop.Word.ApplicationClass WordApp; 
            Microsoft.Office.Interop.Word.Document WordDoc;
            Object oMissing = System.Reflection.Missing.Value;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            object fileName = strFileName;
            
            WordDoc = WordApp.Documents.Open( ref fileName,
               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            Type wordType = WordApp.GetType();
            // ���ļ�
            Type docsType = WordApp.Documents.GetType();
            // ת����ʽ�����Ϊ
            Type docType = WordDoc.GetType();
            object saveFileName = strSaveFileName;
            docType.InvokeMember( "SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML } );
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
            //-----------------------------------------------------------------------------------
            //            docType.InvokeMember( "SaveAs", System.Reflection.BindingFlags.InvokeMethod,
            //                null, WordDoc, new object[]{saveFileName, Word.WdSaveFormat.wdFormatHTML} );
            // �˳� Word
            //wordType.InvokeMember( "Quit", System.Reflection.BindingFlags.InvokeMethod,
            //    null, WordApp, null );
            #endregion
            WordDoc.Close( ref oMissing, ref oMissing, ref oMissing );
            WordApp.Quit( ref oMissing, ref oMissing, ref oMissing );
        }
        #endregion
        #region ����ģ��
        ///
        /// ����ģ��
        ///
        /// ģ���ĵ�·��
        public static void ImportTemplate( string filePath )
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            object fileName = filePath;
            WordDoc = WordApp.Documents.Add( ref fileName, ref oMissing, ref oMissing, ref oMissing );
        }
        #endregion
        #region word������±�
        ///
        /// word������±�
        ///
        public static void AddTable()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            object start = 0;
            object end = 0;
            Microsoft.Office.Interop.Word.Range tableLocation = WordDoc.Range( ref start, ref end );
            WordDoc.Tables.Add( tableLocation, 3, 4, ref oMissing, ref oMissing );//3��4�еı�
        }
        #endregion
        #region �ڱ��в�������
        ///
        /// �ڱ��в����µ�1��
        ///
        public static void AddRow()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            object start = 0;
            object end = 0;
            Microsoft.Office.Interop.Word.Range tableLocation = WordDoc.Range( ref start, ref end );
            WordDoc.Tables.Add( tableLocation, 3, 4, ref oMissing, ref oMissing );
            Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables[1];
            object beforeRow = newTable.Rows[1];
            newTable.Rows.Add( ref beforeRow );
        }
        #endregion
        #region ���뵥Ԫ��
        ///
        /// �ϲ���Ԫ��
        ///
        public static void CombinationCell()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            object start = 0;
            object end = 0;
            Microsoft.Office.Interop.Word.Range tableLocation = WordDoc.Range( ref start, ref end );
            WordDoc.Tables.Add( tableLocation, 3, 4, ref oMissing, ref oMissing );
            Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables[1];
            object beforeRow = newTable.Rows[1];
            newTable.Rows.Add( ref beforeRow );
            Microsoft.Office.Interop.Word.Cell cell = newTable.Cell( 2, 1 );//2��1�кϲ�2��2��Ϊһ��
            cell.Merge( newTable.Cell( 2, 2 ) );
            //cell.Merge( newTable.Cell( 1, 3 ) );
        }
        #endregion
        #region ���뵥Ԫ��
        ///
        /// ���뵥Ԫ��
        ///
        public static void SeparateCell()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            object start = 0;
            object end = 0;
            Microsoft.Office.Interop.Word.Range tableLocation = WordDoc.Range( ref start, ref end );
            WordDoc.Tables.Add( tableLocation, 3, 4, ref oMissing, ref oMissing );
            Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables[1];
            object beforeRow = newTable.Rows[1];
            newTable.Rows.Add( ref beforeRow );
            Microsoft.Office.Interop.Word.Cell cell = newTable.Cell( 1, 1 );
            cell.Merge( newTable.Cell( 1, 2 ) );
            object Rownum = 2;
            object Columnnum = 2;
            cell.Split( ref Rownum, ref  Columnnum );
        }
        #endregion

        #region ͨ��������Ʋ���Insert a paragraph at the beginning of the document.
        ///
        /// ͨ��������Ʋ���Insert a paragraph at the beginning of the document.
        ///
        public static void Insert()
        {
            object oMissing = System.Reflection.Missing.Value;
            //object oEndOfDoc = "\\endofdoc";
            //Start Word and create a new document.
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            WordApp.Visible = true;
            WordDoc = WordApp.Documents.Add( ref oMissing, ref oMissing, ref oMissing, ref oMissing );
            //Insert a paragraph at the beginning of the document.
            Microsoft.Office.Interop.Word.Paragraph oPara1;
            oPara1 = WordDoc.Content.Paragraphs.Add( ref oMissing );
            oPara1.Range.Text = "Heading 1";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();
        }
        #endregion

        #region word�ĵ����ü���ȡ���λ��
        ///
        /// word�ĵ����ü���ȡ���λ��
        ///
        public static void WordSet()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            #region �ĵ���ʽ����
            WordApp.ActiveDocument.PageSetup.LineNumbering.Active = 0;//�б��
            WordApp.ActiveDocument.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;//ҳ�淽��
            WordApp.ActiveDocument.PageSetup.TopMargin = WordApp.CentimetersToPoints( float.Parse( "2.54" ) );//��ҳ�߾�
            WordApp.ActiveDocument.PageSetup.BottomMargin = WordApp.CentimetersToPoints( float.Parse( "2.54" ) );//��ҳ�߾�
            WordApp.ActiveDocument.PageSetup.LeftMargin = WordApp.CentimetersToPoints( float.Parse( "3.17" ) );//��ҳ�߾�
            WordApp.ActiveDocument.PageSetup.RightMargin = WordApp.CentimetersToPoints( float.Parse( "3.17" ) );//��ҳ�߾�
            WordApp.ActiveDocument.PageSetup.Gutter = WordApp.CentimetersToPoints( float.Parse( "0" ) );//װ����λ��
            WordApp.ActiveDocument.PageSetup.HeaderDistance = WordApp.CentimetersToPoints( float.Parse( "1.5" ) );//ҳü
            WordApp.ActiveDocument.PageSetup.FooterDistance = WordApp.CentimetersToPoints( float.Parse( "1.75" ) );//ҳ��
            WordApp.ActiveDocument.PageSetup.PageWidth = WordApp.CentimetersToPoints( float.Parse( "21" ) );//ֽ�ſ��
            WordApp.ActiveDocument.PageSetup.PageHeight = WordApp.CentimetersToPoints( float.Parse( "29.7" ) );//ֽ�Ÿ߶�
            WordApp.ActiveDocument.PageSetup.FirstPageTray = Microsoft.Office.Interop.Word.WdPaperTray.wdPrinterDefaultBin;//ֽ����Դ
            WordApp.ActiveDocument.PageSetup.OtherPagesTray = Microsoft.Office.Interop.Word.WdPaperTray.wdPrinterDefaultBin;//ֽ����Դ
            WordApp.ActiveDocument.PageSetup.SectionStart = Microsoft.Office.Interop.Word.WdSectionStart.wdSectionNewPage;//�ڵ���ʼλ�ã��½�ҳ
            WordApp.ActiveDocument.PageSetup.OddAndEvenPagesHeaderFooter = 0;//ҳüҳ��-��żҳ��ͬ
            WordApp.ActiveDocument.PageSetup.DifferentFirstPageHeaderFooter = 0;//ҳüҳ��-��ҳ��ͬ
            WordApp.ActiveDocument.PageSetup.VerticalAlignment = Microsoft.Office.Interop.Word.WdVerticalAlignment.wdAlignVerticalTop;//ҳ�洹ֱ���뷽ʽ
            WordApp.ActiveDocument.PageSetup.SuppressEndnotes = 0;//������βע
            WordApp.ActiveDocument.PageSetup.MirrorMargins = 0;//��������ҳ������߾�
            WordApp.ActiveDocument.PageSetup.TwoPagesOnOne = false;//��˫���ӡ
            WordApp.ActiveDocument.PageSetup.BookFoldPrinting = false;//�������ֶ�˫�������ӡ
            WordApp.ActiveDocument.PageSetup.BookFoldRevPrinting = false;//�������ֶ�˫�汳���ӡ
            WordApp.ActiveDocument.PageSetup.BookFoldPrintingSheets = 1;//��ӡĬ�Ϸ���
            WordApp.ActiveDocument.PageSetup.GutterPos = Microsoft.Office.Interop.Word.WdGutterStyle.wdGutterPosLeft;//װ����λ�����
            WordApp.ActiveDocument.PageSetup.LinesPage = 40;//Ĭ��ҳ������
            WordApp.ActiveDocument.PageSetup.LayoutMode = Microsoft.Office.Interop.Word.WdLayoutMode.wdLayoutModeLineGrid;//��ʽģʽΪ��ָֻ��������
            #endregion
            #region �����ʽ�趨
            WordApp.Selection.ParagraphFormat.LeftIndent = WordApp.CentimetersToPoints( float.Parse( "0" ) );//������
            WordApp.Selection.ParagraphFormat.RightIndent = WordApp.CentimetersToPoints( float.Parse( "0" ) );//������
            WordApp.Selection.ParagraphFormat.SpaceBefore = float.Parse( "0" );//��ǰ���
            WordApp.Selection.ParagraphFormat.SpaceBeforeAuto = 0;//
            WordApp.Selection.ParagraphFormat.SpaceAfter = float.Parse( "0" );//�κ���
            WordApp.Selection.ParagraphFormat.SpaceAfterAuto = 0;//
            WordApp.Selection.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;//�����о�
            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;//����2�˶���
            WordApp.Selection.ParagraphFormat.WidowControl = 0;//���п���
            WordApp.Selection.ParagraphFormat.KeepWithNext = 0;//���¶�ͬҳ
            WordApp.Selection.ParagraphFormat.KeepTogether = 0;//���в���ҳ
            WordApp.Selection.ParagraphFormat.PageBreakBefore = 0;//��ǰ��ҳ
            WordApp.Selection.ParagraphFormat.NoLineNumber = 0;//ȡ���к�
            WordApp.Selection.ParagraphFormat.Hyphenation = 1;//ȡ������
            WordApp.Selection.ParagraphFormat.FirstLineIndent = WordApp.CentimetersToPoints( float.Parse( "0" ) );//��������
            WordApp.Selection.ParagraphFormat.OutlineLevel = Microsoft.Office.Interop.Word.WdOutlineLevel.wdOutlineLevelBodyText;
            WordApp.Selection.ParagraphFormat.CharacterUnitLeftIndent = float.Parse( "0" );
            WordApp.Selection.ParagraphFormat.CharacterUnitRightIndent = float.Parse( "0" );
            WordApp.Selection.ParagraphFormat.CharacterUnitFirstLineIndent = float.Parse( "0" );
            WordApp.Selection.ParagraphFormat.LineUnitBefore = float.Parse( "0" );
            WordApp.Selection.ParagraphFormat.LineUnitAfter = float.Parse( "0" );
            WordApp.Selection.ParagraphFormat.AutoAdjustRightIndent = 1;
            WordApp.Selection.ParagraphFormat.DisableLineHeightGrid = 0;
            WordApp.Selection.ParagraphFormat.FarEastLineBreakControl = 1;
            WordApp.Selection.ParagraphFormat.WordWrap = 1;
            WordApp.Selection.ParagraphFormat.HangingPunctuation = 1;
            WordApp.Selection.ParagraphFormat.HalfWidthPunctuationOnTopOfLine = 0;
            WordApp.Selection.ParagraphFormat.AddSpaceBetweenFarEastAndAlpha = 1;
            WordApp.Selection.ParagraphFormat.AddSpaceBetweenFarEastAndDigit = 1;
            WordApp.Selection.ParagraphFormat.BaseLineAlignment = Microsoft.Office.Interop.Word.WdBaselineAlignment.wdBaselineAlignAuto;
            #endregion
            #region �����ʽ�趨
            WordApp.Selection.Font.NameFarEast = "��������";
            WordApp.Selection.Font.NameAscii = "Times New Roman";
            WordApp.Selection.Font.NameOther = "Times New Roman";
            WordApp.Selection.Font.Name = "����";
            WordApp.Selection.Font.Size = float.Parse( "14" );
            WordApp.Selection.Font.Bold = 0;
            WordApp.Selection.Font.Italic = 0;
            WordApp.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            WordApp.Selection.Font.UnderlineColor = Microsoft.Office.Interop.Word.WdColor.wdColorAutomatic;
            WordApp.Selection.Font.StrikeThrough = 0;//ɾ����
            WordApp.Selection.Font.DoubleStrikeThrough = 0;//˫ɾ����
            WordApp.Selection.Font.Outline = 0;//����
            WordApp.Selection.Font.Emboss = 0;//����
            WordApp.Selection.Font.Shadow = 0;//��Ӱ
            WordApp.Selection.Font.Hidden = 0;//��������
            WordApp.Selection.Font.SmallCaps = 0;//С�ʹ�д��ĸ
            WordApp.Selection.Font.AllCaps = 0;//ȫ����д��ĸ
            WordApp.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorAutomatic;
            WordApp.Selection.Font.Engrave = 0;//����
            WordApp.Selection.Font.Superscript = 0;//�ϱ�
            WordApp.Selection.Font.Subscript = 0;//�±�
            WordApp.Selection.Font.Spacing = float.Parse( "0" );//�ַ����
            WordApp.Selection.Font.Scaling = 100;//�ַ�����
            WordApp.Selection.Font.Position = 0;//λ��
            WordApp.Selection.Font.Kerning = float.Parse( "1" );//���������
            WordApp.Selection.Font.Animation = Microsoft.Office.Interop.Word.WdAnimation.wdAnimationNone;//����Ч��
            WordApp.Selection.Font.DisableCharacterSpaceGrid = false;
            WordApp.Selection.Font.EmphasisMark = Microsoft.Office.Interop.Word.WdEmphasisMark.wdEmphasisMarkNone;
            #endregion
            #region ��ȡ���λ��
            ////get_Information
            WordApp.Selection.get_Information( WdInformation.wdActiveEndPageNumber );
            //�����к�-ҳ��-�к�-λ��
            //information ���� 
            //�����й�ָ������ѡ���ݻ��������Ϣ��variant ���ͣ�ֻ���� 
            //expression.information(type) 
            //expression ���衣�ñ��ʽ����һ�� range �� selection ���� 
            //type long ���ͣ����衣��Ҫ���ص���Ϣ����ȡ���� wdinformation ����֮һ�� 
            //wdactiveendadjustedpagenumber ����ҳ�룬�ڸ�ҳ�а���ָ������ѡ���ݻ�����Ļ��β�����������һ����ʼҳ�룬����ҳ��������ֹ��������򷵻ص�������ҳ�롣 
            //wdactiveendpagenumber ����ҳ�룬�ڸ�ҳ�а���ָ������ѡ���ݻ�����Ļ��β��ҳ����ĵ��Ŀ�ͷ��ʼ����������Ƕ�ҳ����κ��ֹ������� 
            //wdactiveendsectionnumber ���ؽںţ��ڸý��а�����ָ������ѡ���ݻ�����Ļ��β�� 
            //wdatendofrowmarker ���ָ������ѡ���ݻ�����λ�ڱ����н�β��Ǵ����򱾲������� true�� 
            //wdcapslock �����д��ĸ����ģʽ��Ч���򱾲������� true�� 
            //wdendofrangecolumnnumber ���ر���кţ��ڸñ�����а�����ָ������ѡ���ݻ�����Ļ��β�� 
            //wdendofrangerownumber ���ر���кţ��ڸñ���а�����ָ������ѡ���ݻ�����Ļ��β�� 
            //wdfirstcharactercolumnnumber ����ָ������ѡ���ݻ������е�һ���ַ���λ�á������ѡ���ݻ��������۵��ģ��򷵻���ѡ���ݻ������Ҳ�����ŵ��ַ���š� 
            //wdfirstcharacterlinenumber ������ѡ�����е�һ���ַ����кš���� pagination ����Ϊ false���� draft ����Ϊ true���򷵻� - 1�� 
            //wdframeisselected �����ѡ���ݻ�������һ��������ͼ�Ŀ��ı����򱾲������� true�� 
            //wdheaderfootertype ����һ��ֵ����ֵ����������ָ������ѡ���ݻ������ҳü��ҳ�ŵ����ͣ����±���ʾ�� ֵҳü��ҳ�ŵ����� 
            //- 1 �� 
            //0 ż��ҳҳü 
            //1 ����ҳҳü 
            //2 ż��ҳҳ�� 
            //3 ����ҳҳ�� 
            //4 ��һ��ҳü 
            //5 ��һ��ҳ�� 
            //wdhorizontalpositionrelativetopage ����ָ������ѡ���ݻ������ˮƽλ�á���λ������ѡ���ݻ�����������ҳ������֮��ľ��룬�԰�Ϊ��λ�������ѡ���ݻ����򲻿ɼ����򷵻� - 1�� 
            //wdhorizontalpositionrelativetotextboundary ����ָ������ѡ���ݻ������������Χ��������ı߽����ߵ�ˮƽλ�ã��԰�Ϊ��λ�������ѡ���ݻ�����û����ʾ�ڵ�ǰ��Ļ���򱾲������� - 1�� 
            //wdinclipboard �йش˳�������ϸ���ݣ������ microsoft office 98 macintosh ������Բο������� 
            //wdincommentpane ���ָ������ѡ���ݻ�����λ����ע�����򷵻� true�� 
            //wdinendnote ���ָ������ѡ���ݻ�����λ��ҳ����ͼ��βע���ڣ�����λ����ͨ��ͼ��βע�����У��򱾲������� true�� 
            //wdinfootnote ���ָ������ѡ���ݻ�����λ��ҳ����ͼ�Ľ�ע���ڣ�����λ����ͨ��ͼ�Ľ�ע�����У��򱾲������� true�� 
            //wdinfootnoteendnotepane ���ָ������ѡ���ݻ�����λ��ҳ����ͼ�Ľ�ע��βע���ڣ�����λ����ͨ��ͼ�Ľ�ע��βע�����У��򱾲������� true����ϸ���ݣ������ǰ��� wdinfootnote �� wdinendnote ��˵���� 
            //wdinheaderfooter ���ָ������ѡ���ݻ�����λ��ҳü��ҳ�Ŵ����У�����λ��ҳ����ͼ��ҳü��ҳ���У��򱾲������� true�� 
            //wdinmasterdocument ���ָ������ѡ���ݻ�����λ�������ĵ��У��򱾲������� true�� 
            //wdinwordmail ����һ��ֵ����ֵ��������ѡ���ݻ�����ĵ�λ�ã����±���ʾ��ֵλ�� 
            //0 ��ѡ���ݻ�������һ�������ʼ���Ϣ�С� 
            //1 ��ѡ���ݻ�����λ�����ڷ��͵ĵ����ʼ��С� 
            //2 ��ѡ���ݻ�����λ�������Ķ��ĵ����ʼ��С� 
            //wdmaximumnumberofcolumns ������ѡ���ݻ��������κ��е������������ 
            //wdmaximumnumberofrows ����ָ������ѡ���ݻ������б������������ 
            //wdnumberofpagesindocument ��������ѡ���ݻ�������������ĵ���ҳ���� 
            //wdnumlock ��� num lock ��Ч���򱾲������� true�� 
            //wdovertype �����дģʽ��Ч���򱾲������� true������ overtype ���Ըı��дģʽ��״̬�� 
            //wdreferenceoftype ����һ��ֵ����ֵ������ѡ��������ڽ�ע��βע����ע���õ�λ�ã����±���ʾ�� ֵ���� 
            //�� 1 ��ѡ���ݻ��������������ֻ�޶��ڽ�ע��βע����ע�����С� 
            //0 ��ѡ���ݻ������ڽ�ע��βע����ע����֮ǰ�� 
            //1 ��ѡ���ݻ�����λ�ڽ�ע����֮ǰ�� 
            //2 ��ѡ���ݻ�����λ��βע����֮ǰ�� 
            //3 ��ѡ���ݻ�����λ����ע����֮ǰ�� 
            //wdrevisionmarking ����޶����ܴ��ڻ״̬���򱾲������� true�� 
            //wdselectionmode ����һ��ֵ����ֵ������ǰ��ѡ��ģʽ�����±���ʾ�� ֵѡ��ģʽ 
            //0 ����ѡ�� 
            //1 ��չѡ�� 
            //2 ��ѡ�� 
            //wdstartofrangecolumnnumber ������ѡ���ݻ������������ڵı����кš� 
            //wdstartofrangerownumber ������ѡ���ݻ������������ڵı����кš� 
            //wdverticalpositionrelativetopage ������ѡ���ݻ�����Ĵ�ֱλ�ã�����ѡ���ݵ��ϱ���ҳ����ϱ�֮��ľ��룬�԰�Ϊ��λ�������ѡ���ݻ�����û����ʾ����Ļ�ϣ��򱾲������� - 1�� 
            //wdverticalpositionrelativetotextboundary ������ѡ���ݻ������������Χ��������ı߽���ϱߵĴ�ֱλ�ã��԰�Ϊ��λ�������ѡ���ݻ�����û����ʾ����Ļ�ϣ��򱾲������� - 1�� 
            //wdwithintable �����ѡ����λ��һ������У��򱾲������� true�� 
            //wdzoompercentage ������ percentage �������õĵ�ǰ�ķŴ�ٷֱȡ�
            #endregion
            #region ����ƶ�
            //�ƶ����
            //�������3�� ����3��
            object unit = Microsoft.Office.Interop.Word.WdUnits.wdLine;
            object count = 3;
            WordApp.Selection.MoveEnd( ref unit, ref count );
            WordApp.Selection.MoveUp( ref unit, ref count, ref oMissing );
            //Microsoft.Office.Interop.Word.WdUnits˵��
            //wdCell                  A cell. 
            //wdCharacter             A character. 
            //wdCharacterFormatting   Character formatting. 
            //wdColumn                A column. 
            //wdItem                  The selected item. 
            //wdLine                  A line. //��
            //wdParagraph             A paragraph. 
            //wdParagraphFormatting   Paragraph formatting. 
            //wdRow                   A row. 
            //wdScreen                The screen dimensions. 
            //wdSection               A section. 
            //wdSentence              A sentence. 
            //wdStory                 A story. 
            //wdTable                 A table. 
            //wdWindow                A window. 
            //wdWord                  A word.
            //¼�Ƶ�vb��
            //     ,�ƶ��������ǰ����
            //    Selection.HomeKey unit:=wdLine
            //    '�ƶ��������ǰ��β
            //    Selection.EndKey unit:=wdLine
            //    'ѡ��ӹ������ǰ���׵�����
            //    Selection.HomeKey unit:=wdLine, Extend:=wdExtend
            //    'ѡ��ӹ������ǰ��β������
            //    Selection.EndKey unit:=wdLine, Extend:=wdExtend
            //    'ѡ��ǰ��
            //    Selection.HomeKey unit:=wdLine
            //    Selection.EndKey unit:=wdLine, Extend:=wdExtend
            //    '�ƶ�������ĵ���ʼ
            //    Selection.HomeKey unit:=wdStory
            //    '�ƶ�������ĵ���β
            //    Selection.EndKey unit:=wdStory
            //    'ѡ��ӹ�����ĵ���ʼ������
            //    Selection.HomeKey unit:=wdStory, Extend:=wdExtend
            //    'ѡ��ӹ�����ĵ���β������
            //    Selection.EndKey unit:=wdStory, Extend:=wdExtend
            //    'ѡ���ĵ�ȫ�����ݣ���WholeStory�ɲ³�StoryӦ�ǵ�ǰ�ĵ�����˼��
            //    Selection.WholeStory
            //    '�ƶ��������ǰ����Ŀ�ʼ
            //    Selection.MoveUp unit:=wdParagraph
            //    '�ƶ��������ǰ����Ľ�β
            //    Selection.MoveDown unit:=wdParagraph
            //    'ѡ��ӹ������ǰ���俪ʼ������
            //    Selection.MoveUp unit:=wdParagraph, Extend:=wdExtend
            //    'ѡ��ӹ������ǰ�����β������
            //    Selection.MoveDown unit:=wdParagraph, Extend:=wdExtend
            //    'ѡ�������ڶ��������
            //    Selection.MoveUp unit:=wdParagraph
            //    Selection.MoveDown unit:=wdParagraph, Extend:=wdExtend
            //    '��ʾѡ�����Ŀ�ʼ�������λ�ã�ע�⣺�ĵ���1���ַ���λ����0
            //    MsgBox ("��" & Selection.Start & "���ַ�����" & Selection.End & "���ַ�")
            //    'ɾ����ǰ��
            //    Selection.HomeKey unit:=wdLine
            //    Selection.EndKey unit:=wdLine, Extend:=wdExtend
            //    Selection.Delete
            //    'ɾ����ǰ����
            //    Selection.MoveUp unit:=wdParagraph
            //    Selection.MoveDown unit:=wdParagraph, Extend:=wdExtend
            //    Selection.Delete

            //���Ĺ���ƶ�
            //��굽��ǰ������ڱ��ĵص�Ԫ��
            WordApp.Selection.Tables[1].Cell( 1, 1 ).Select();
            //unit������
            object unith = Microsoft.Office.Interop.Word.WdUnits.wdRow;//����з�ʽ
            object extend = Microsoft.Office.Interop.Word.WdMovementType.wdExtend;///extend�Թ���ƶ����������չѡ��
            object unitu = Microsoft.Office.Interop.Word.WdUnits.wdLine;//�ĵ��з�ʽ,���Կ��ɱ��һ��.������wdRow������
            object unitp = Microsoft.Office.Interop.Word.WdUnits.wdParagraph;//���䷽ʽ,���ڱ�����ѡ�񵽱���к�Ļ�����,���ڿ��кϲ�����ѡ��,�����ҵ�����򵥷�ʽ
            //object count = 1;//����ƶ���
            #endregion
        }
        #endregion

        #region ��ȡWord�����ĳ����Ԫ������ݡ����еĲ����ֱ�Ϊ�ļ���������·�������кţ��кš�
        ///
        /// ��ȡWord�����ĳ����Ԫ������ݡ����еĲ����ֱ�Ϊ�ļ���������·�������кţ��кš�
        ///
        /// word�ĵ�
        /// ��
        /// ��
        /// ��������
        public static string ReadWord_tableContentByCell( string fileName, int rowIndex, int colIndex )
        {
            ApplicationClass cls = null;
            Document doc = null;
            Table table = null;
            object missing = Missing.Value;
            object path = fileName;
            cls = new ApplicationClass();
            try
            {
                doc = cls.Documents.Open
                  ( ref path, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing );
                table = doc.Tables[1];
                string text = table.Cell( rowIndex, colIndex ).Range.Text.ToString();
                text = text.Substring( 0, text.Length - 2 );����//ȥ��β����mark
                return text;
            }
            catch( Exception ex )
            {
                return ex.Message;
            }
            finally
            {
                if( doc != null )
                    doc.Close( ref missing, ref missing, ref missing );
                cls.Quit( ref missing, ref missing, ref missing );
            }
        }
        #endregion
        
        #region �޸�word�����ָ����Ԫ�������
        ///
        /// �޸�word�����ָ����Ԫ�������
        ///
        /// word�ĵ�����·��
        /// ��
        /// ��
        ///
        ///
        public static bool UpdateWordTableByCell( string fileName, int rowIndex, int colIndex, string content )
        {
            ApplicationClass cls = null;
            Document doc = null;
            Table table = null;
            object missing = Missing.Value;
            object path = fileName;
            cls = new ApplicationClass();
            try
            {
                doc = cls.Documents.Open
                    ( ref path, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing );
                table = doc.Tables[1];
                //doc.Range( ref 0, ref 0 ).InsertParagraphAfter();//����س�
                table.Cell( rowIndex, colIndex ).Range.InsertParagraphAfter();//.Text = content;
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if( doc != null )
                {
                    doc.Close( ref missing, ref missing, ref missing );
                    cls.Quit( ref missing, ref missing, ref missing );
                }
            }
        }
        #endregion 
        
        #region ���word����
        ///
        /// ���word����
        ///
        public static void KillWordProcess()
        {
            System.Diagnostics.Process[] myPs;
            myPs = System.Diagnostics.Process.GetProcesses();
            foreach( System.Diagnostics.Process p in myPs )
            {
                if( p.Id != 0 )
                {
                    string myS = "WINWORD.EXE" + p.ProcessName + "  ID:" + p.Id.ToString();
                    try
                    {
                        if( p.Modules != null )
                            if( p.Modules.Count > 0 )
                            {
                                System.Diagnostics.ProcessModule pm = p.Modules[0];
                                myS += "\n Modules[0].FileName:" + pm.FileName;
                                myS += "\n Modules[0].ModuleName:" + pm.ModuleName;
                                myS += "\n Modules[0].FileVersionInfo:\n" + pm.FileVersionInfo.ToString();
                                if( pm.ModuleName.ToLower() == "winword.exe" )
                                    p.Kill();
                            }
                    }
                    catch
                    { }
                    finally
                    {
                        ;
                    }
                }
            }
        }
        #endregion 
        
        #region ���excel����
        ///
        /// ���excel����
        ///
        public static void KillExcelProcess()
        {
            System.Diagnostics.Process[] myPs;
            myPs = System.Diagnostics.Process.GetProcesses();
            foreach( System.Diagnostics.Process p in myPs )
            {
                if( p.Id != 0 )
                {
                    string myS = "excel.EXE" + p.ProcessName + "  ID:" + p.Id.ToString();
                    try
                    {
                        if( p.Modules != null )
                            if( p.Modules.Count > 0 )
                            {
                                System.Diagnostics.ProcessModule pm = p.Modules[0];
                                myS += "\n Modules[0].FileName:" + pm.FileName;
                                myS += "\n Modules[0].ModuleName:" + pm.ModuleName;
                                myS += "\n Modules[0].FileVersionInfo:\n" + pm.FileVersionInfo.ToString();
                                if( pm.ModuleName.ToLower() == "excel.exe" )
                                    p.Kill();
                            }
                    }
                    catch
                    { }
                    finally
                    {
                        ;
                    }
                }
            }
        }
        #endregion 
        
        #region ��ҳ���ݻ���word��excel
        ///
        /// ��ҳ���ݱ���򵼳�Ϊword��excel
        ///
        /// ��ҳ��ַ
        /// 0Ϊ����word,1Ϊ����excel
        public static void SaveOrOutData( string url, int num )//�������ݵĺ���0Ϊword,1ΪExcel 
        {
            WebRequest req = WebRequest.Create( url );
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader( resp.GetResponseStream(), System.Text.Encoding.UTF8 );
            string x = sr.ReadToEnd();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding( "gb2312" );
            string fName = DateTime.Now.ToString( "yyyy-MM-dd-ss" );
            if( num == 0 )
            {
                fName = HttpUtility.UrlEncode( fName, System.Text.Encoding.GetEncoding( "gb2312" ) ) + ".doc";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-word";
            }
            else
            {
                fName = HttpUtility.UrlEncode( fName, System.Text.Encoding.GetEncoding( "gb2312" ) ) + ".xls";
                System.Web.HttpContext.Current.Response.ContentType = "application nd.xls";
            }
            System.Web.HttpContext.Current.Response.AddHeader( "content-disposition", "attachment;filename=" + fName );
            System.Web.HttpContext.Current.Response.Write( getBodyContent( x ) );//��ȡtable��ǩ
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        ///
        /// ��ȡ��ҳtable��ǩ������
        ///
        /// html����
        ///
        private static string getBodyContent( string input )
        {
            string pattern = @"";
            Regex reg = new Regex( pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase );
            Match mc = reg.Match( input );
            string bodyContent = "";
            if( mc.Success )
            {
                bodyContent = mc.Value;
            }
            return bodyContent;
        }
        #endregion  
        
        #region �ж�ϵͳ�Ƿ�װexcel
        ///
        /// �ж�ϵͳ�Ƿ�װexcel
        ///
        ///
        public static bool IsInstallExcel()
        {
            RegistryKey machineKey = Registry.LocalMachine;
            if( IsInstallExcelByVersion( "12.0", machineKey ) )
            {
                return true;
            }
            if( IsInstallExcelByVersion( "11.0", machineKey ) )
            {
                return true;
            }
            return false;
        }
        ///
        /// �ж�ϵͳ�Ƿ�װĳ�汾��excel
        ///
        /// �汾��
        ///
        ///
        private static bool IsInstallExcelByVersion( string strVersion, RegistryKey machineKey )
        {
            try
            {
                RegistryKey installKey = machineKey.OpenSubKey( "Software" ).OpenSubKey( "Microsoft" ).OpenSubKey( "Office" ).OpenSubKey( strVersion ).OpenSubKey( "Excel" ).OpenSubKey( "InstallRoot" );
                if( installKey == null )
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion 
        
        #region �ж�ϵͳ�Ƿ�װword
        ///
        /// �ж�ϵͳ�Ƿ�װword
        ///
        ///
        public static bool IsInstallWord()
        {
            RegistryKey machineKey = Registry.LocalMachine;
            if( IsInstallExcelByVersion( "12.0", machineKey ) )
            {
                return true;
            }
            if( IsInstallExcelByVersion( "11.0", machineKey ) )
            {
                return true;
            }
            return false;
        }
        ///
        /// �ж�ϵͳ�Ƿ�װĳ�汾��word
        ///
        /// �汾��
        ///
        ///
        private static bool IsInstallWordByVersion( string strVersion, RegistryKey machineKey )
        {
            try
            {
                RegistryKey installKey = machineKey.OpenSubKey( "Software" ).OpenSubKey( "Microsoft" ).OpenSubKey( "Office" ).OpenSubKey( strVersion ).OpenSubKey( "Word" ).OpenSubKey( "InstallRoot" );
                if( installKey == null )
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion               
    }
}