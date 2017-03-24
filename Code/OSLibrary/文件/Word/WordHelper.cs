using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;
 
namespace GS.WebSites.BP.Report
{
    /*
     *  Create By:xuzhihong
     *  Create Date:2010-08-25
     *  Description:����Word����������  
     */
    public partial class WordHelper
    {
        #region ˽�г�Ա
 
        private Word.ApplicationClass _wordApplication;
        private Word.Document _wordDocument;
        object missing = System.Reflection.Missing.Value;
 
        #endregion
 
        #region  ��������
 
        /// <summary>
        /// ApplciationClass
        /// </summary>
        public Word.ApplicationClass WordApplication
        {
            get
            {
                return _wordApplication;
            }
        }
 
        /// <summary>
        /// Document
        /// </summary>
        public Word.Document WordDocument
        {
            get
            {
                return _wordDocument;
            }
        }
 
        #endregion
 
        #region  ���캯��
 
        public WordHelper()
        {
            _wordApplication = new Word.ApplicationClass();
        }
        public WordHelper(Word.ApplicationClass wordApplication)
        {
            _wordApplication = wordApplication;
        }
 
        #endregion
 
        #region �����������½����򿪡����桢�رգ�
 
        /// <summary>
        /// �½�����һ���ĵ���Ĭ��ȱʡֵ��
        /// </summary>
        public void CreateAndActive()
        {
            _wordDocument = CreateOneDocument(missing, missing, missing, missing);
            _wordDocument.Activate();
        }
 
        /// <summary>
        /// ��ָ���ļ�
        /// </summary>
        /// <param name="FileName">�ļ���������·����</param>
        /// <param name="IsReadOnly">�򿪺��Ƿ�ֻ��</param>
        /// <param name="IsVisibleWin">�򿪺��Ƿ����</param>
        /// <returns>���Ƿ�ɹ�</returns>
        public bool OpenAndActive(string FileName, bool IsReadOnly, bool IsVisibleWin)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return false;
            }
            try
            {
                _wordDocument = OpenOneDocument(FileName, missing,IsReadOnly, missing, missing, missing, missing, missing, missing, missing, missing, IsVisibleWin, missing, missing, missing, missing);
                _wordDocument.Activate();
                return true;
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// �ر�
        /// Closes the specified document or documents.
        /// </summary>
        public void Close()
        {
            if (_wordDocument != null)
            {
                _wordDocument.Close(ref missing,ref missing,ref missing);
                _wordApplication.Application.Quit(ref missing, ref missing, ref missing);
            }
        }
 
        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            if (_wordDocument == null)
            {
                _wordDocument = _wordApplication.ActiveDocument;
            }           
            _wordDocument.Save();
        }
 
        /// <summary>
        /// ����Ϊ...
        /// </summary>
        /// <param name="FileName">�ļ���������·����</param>
        public void SaveAs(string FileName)
        {
            if (_wordDocument == null)
            {
                _wordDocument = _wordApplication.ActiveDocument;
            }
            object objFileName = FileName;
 
            _wordDocument.SaveAs(ref objFileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }
 
        /// <summary>
        /// �½�һ��Document
        /// </summary>
        /// <param name="template">Optional Object. The name of the template to be used for the new document. If this argument is omitted, the Normal template is used.</param>
        /// <param name="newTemplate">Optional Object. True to open the document as a template. The default value is False.</param>
        /// <param name="documentType">Optional Object. Can be one of the following WdNewDocumentType constants: wdNewBlankDocument, wdNewEmailMessage, wdNewFrameset, or wdNewWebPage. The default constant is wdNewBlankDocument.</param>
        /// <param name="visible">Optional Object. True to open the document in a visible window. If this value is False, Microsoft Word opens the document but sets the Visible property of the document window to False. The default value is True.</param> 
        public Word.Document CreateOneDocument(object template,object newTemplate,object documentType,object visible)
        {
             return _wordApplication.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
        }
 
        /// <summary>
        /// ��һ�������ĵ�
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ConfirmConversions"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="AddToRecentFiles"></param>
        /// <param name="PasswordDocument"></param>
        /// <param name="PasswordTemplate"></param>
        /// <param name="Revert"></param>
        /// <param name="WritePasswordDocument"></param>
        /// <param name="WritePasswordTemplate"></param>
        /// <param name="Format"></param>
        /// <param name="Encoding"></param>
        /// <param name="Visible"></param>
        /// <param name="OpenAndRepair"></param>
        /// <param name="DocumentDirection"></param>
        /// <param name="NoEncodingDialog"></param>
        /// <param name="XMLTransform"></param>
        /// <returns></returns>
        public Word.Document OpenOneDocument(object FileName,object ConfirmConversions,object ReadOnly,
            object AddToRecentFiles,object PasswordDocument,object PasswordTemplate,object Revert,
            object WritePasswordDocument,object WritePasswordTemplate,object Format,object Encoding,
            object Visible, object OpenAndRepair,object DocumentDirection,object NoEncodingDialog,object XMLTransform)
        {
            try
            {
                return _wordApplication.Documents.Open(ref FileName, ref ConfirmConversions, ref ReadOnly, ref AddToRecentFiles,
                    ref PasswordDocument, ref PasswordTemplate, ref Revert, ref WritePasswordDocument, ref WritePasswordTemplate,
                    ref Format, ref Encoding, ref Visible,ref OpenAndRepair, ref DocumentDirection, ref NoEncodingDialog, ref XMLTransform);
            }
            catch
            {
                return null;
            }
        }
 
        #endregion
 
        #region �ƶ����λ��
 
        /// <summary>
        /// ����ƶ���ָ����ǩλ�ã���ǩ������ʱ���ƶ�
        /// </summary>
        /// <param name="bookMarkName"></param>
        /// <returns></returns>
        public bool GoToBookMark(string bookMarkName)
        {
            //�Ƿ������ǩ
            if (_wordDocument.Bookmarks.Exists(bookMarkName)) 
            {
                object what = Word.WdGoToItem.wdGoToBookmark;
                object name = bookMarkName;
                GoTo(what, missing, missing, name);
                return true;
            }
            return false;
        }
 
        /// <summary>
        /// �ƶ����
        /// Moves the insertion point to the character position immediately preceding the specified item.
        /// </summary>
        /// <param name="what">Optional Object. The kind of item to which the selection is moved. Can be one of the WdGoToItem constants.</param>
        /// <param name="which">Optional Object. The item to which the selection is moved. Can be one of the WdGoToDirection constants. </param>
        /// <param name="count">Optional Object. The number of the item in the document. The default value is 1.</param>
        /// <param name="name">Optional Object. If the What argument is wdGoToBookmark, wdGoToComment, wdGoToField, or wdGoToObject, this argument specifies a name.</param>
        public void GoTo(object what,object which,object count,object name)
        {
            _wordApplication.Selection.GoTo(ref what, ref which, ref count, ref name);
        }
 
        /// <summary>
        /// �����ƶ�һ���ַ�
        /// </summary>
        public void MoveRight()
        {
            MoveRight(1);
        }
 
        /// <summary>
        /// �����ƶ�N���ַ�
        /// </summary>
        /// <param name="num"></param>
        public void MoveRight(int num)
        {
            object unit = Word.WdUnits.wdCharacter;
            object count = num;
            MoveRight(unit, count, missing);
        }
 
        /// <summary>
        /// �����ƶ�һ���ַ�
        /// </summary>
        public void MoveDown()
        {
            MoveDown(1);
        }
 
        /// <summary>
        /// �����ƶ�N���ַ�
        /// </summary>
        /// <param name="num"></param>
        public void MoveDown(int num)
        {
            object unit = Word.WdUnits.wdCharacter;
            object count = num;
            MoveDown(unit, count, missing);
        }
        /// <summary>
        /// ������� 
        /// Moves the selection up and returns the number of units it's been moved.
        /// </summary>
        /// <param name="unit">Optional Object. The unit by which to move the selection. Can be one of the following WdUnits constants: wdLine, wdParagraph, wdWindow or wdScreen etc. The default value is wdLine.</param>
        /// <param name="count">Optional Object. The number of units the selection is to be moved. The default value is 1.</param>
        /// <param name="extend">Optional Object. Can be either wdMove or wdExtend. If wdMove is used, the selection is collapsed to the end point and moved up. If wdExtend is used, the selection is extended up. The default value is wdMove.</param>
        /// <returns></returns>
        public int MoveUp(object unit,object count,object extend)
        {
            return _wordApplication.Selection.MoveUp(ref unit, ref count, ref extend);
        }
 
        /// <summary>
        /// ������� 
        /// Moves the selection down and returns the number of units it's been moved.
        /// ����˵�����MoveUp
        /// </summary>
        public int MoveDown(object unit, object count, object extend)
        {
            return _wordApplication.Selection.MoveDown(ref unit, ref count, ref extend);
        }
 
        /// <summary>
        /// ������� 
        /// Moves the selection to the left and returns the number of units it's been moved.
        /// ����˵�����MoveUp
        /// </summary>
        public int MoveLeft(object unit, object count, object extend)
        {
            return _wordApplication.Selection.MoveLeft(ref unit, ref count, ref extend);
        }
 
        /// <summary>
        /// ������� 
        /// Moves the selection to the left and returns the number of units it's been moved.
        /// ����˵�����MoveUp
        /// </summary>
        public int MoveRight(object unit, object count, object extend)
        {
            return _wordApplication.Selection.MoveRight(ref unit, ref count, ref extend);
        }
 
        #endregion
 
        #region ���ҡ��滻
 
        /// <summary>
        /// �滻��ǩ����
        /// </summary>
        /// <param name="bookMarkName">��ǩ��</param>
        /// <param name="text">�滻�������</param>
        public void ReplaceBookMark(string bookMarkName, string text)
        {
            bool isExist = GoToBookMark(bookMarkName);
            if (isExist)
            {
                InsertText(text);
            }
        }
 
        /// <summary>
        /// �滻
        /// </summary>
        /// <param name="oldText">���滻���ı�</param>
        /// <param name="newText">�滻����ı�</param>
        /// <param name="replaceType">All:�滻���С�None:���滻��FirstOne:�滻��һ��</param>
        /// <param name="isCaseSensitive">��Сд�Ƿ�����</param>
        /// <returns></returns>
        public bool Replace(string oldText,string newText,string replaceType,bool isCaseSensitive)
        {
            if (_wordDocument == null)
            {
                _wordDocument = _wordApplication.ActiveDocument;
 
            }
            object findText = oldText;
            object replaceWith =newText;
            object wdReplace;
            object matchCase = isCaseSensitive;
            switch (replaceType)
            {
                case "All":
                    wdReplace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    break;
                case "None":
                    wdReplace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceNone;
                    break;
                case "FirstOne":
                    wdReplace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceOne;
                    break;
                default:
                    wdReplace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceOne;
                    break;
            }
            _wordDocument.Content.Find.ClearFormatting();//�Ƴ�Find�������ı��Ͷ����ʽ����
 
            return _wordDocument.Content.Find.Execute(ref findText,ref matchCase, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing, ref missing,ref replaceWith,
                  ref wdReplace, ref missing, ref missing, ref missing, ref missing);
        }
 
        #endregion
 
        #region ���롢ɾ������
 
        /// <summary>
        /// �����ı� Inserts the specified text.
        /// </summary>
        /// <param name="text"></param>
        public void InsertText(string text)
        {
            _wordApplication.Selection.TypeText(text);
        }
 
        /// <summary>
        /// Enter(����) Inserts a new, blank paragraph.
        /// </summary>
        public void InsertLineBreak()
        {
            _wordApplication.Selection.TypeParagraph();
        }
        /// <summary>
        /// ����ͼƬ��ͼƬ��С����Ӧ��
        /// </summary>
        /// <param name="fileName">ͼƬ��������·����</param>
        public void InsertPic(string fileName)
        {
             object range = _wordApplication.Selection.Range;
             InsertPic(fileName, missing, missing, range);
        }
 
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="fileName">ͼƬ��������·����</param>
        /// <param name="width">���ÿ��</param>
        /// <param name="height">���ø߶�</param>
        public void InsertPic(string fileName,float width,float height)
        {
            object range = _wordApplication.Selection.Range;
            InsertPic(fileName, missing, missing, range,width,height);
        }
 
        /// <summary>
        /// ����ͼƬ�������⣩
        /// </summary>
        /// <param name="fileName">ͼƬ��������·����</param>
        /// <param name="width">���ÿ��</param>
        /// <param name="height">���ø߶�<</param>
        /// <param name="caption">�����ע����</param>
        public void InsertPic(string fileName,float width,float height,string caption)
        {
            object range = _wordApplication.Selection.Range;
            InsertPic(fileName, missing, missing, range, width, height,caption);
        }
 
        /// <summary>
        /// ����ͼƬ�������⣩
        /// </summary>
        public void InsertPic(string FileName, object LinkToFile, object SaveWithDocument, object Range, float Width, float Height,string caption)
        {
            _wordApplication.Selection.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Range).Select();
            if (Width>0)
            {
                _wordApplication.Selection.InlineShapes[1].Width = Width;
            }
            if (Height>0)
            {
                _wordApplication.Selection.InlineShapes[1].Height = Height;
            }          
           
            object Label = Word.WdCaptionLabelID.wdCaptionFigure;
            object Title = caption;
            object TitleAutoText = "";
            object Position = Word.WdCaptionPosition.wdCaptionPositionBelow;
            object ExcludeLabel = true;
            _wordApplication.Selection.InsertCaption(ref Label, ref Title, ref TitleAutoText, ref Position, ref ExcludeLabel);
            MoveRight();
        }
 
        /// <summary>
        /// Adds a picture to a document.
        /// </summary>
        /// <param name="FileName">Required String. The path and file name of the picture.</param>
        /// <param name="LinkToFile">Optional Object. True to link the picture to the file from which it was created. False to make the picture an independent copy of the file. The default value is False.</param>
        /// <param name="SaveWithDocument">Optional Object. True to save the linked picture with the document. The default value is False.</param>
        /// <param name="Range">Optional Object. The location where the picture will be placed in the text. If the range isn't collapsed, the picture replaces the range; otherwise, the picture is inserted. If this argument is omitted, the picture is placed automatically.</param>
        /// <param name="Width">Sets the width of the specified object, in points. </param>
        /// <param name="Height">Sets the height of the specified inline shape. </param>
        public void InsertPic(string FileName,object LinkToFile,object SaveWithDocument,object Range,float Width,float Height)
        {
            _wordApplication.Selection.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Range).Select();
            _wordApplication.Selection.InlineShapes[1].Width = Width;
            _wordApplication.Selection.InlineShapes[1].Height = Height;
            MoveRight();
        }
 
        /// <summary>
        /// Adds a picture to a document.
        /// </summary>
        /// <param name="FileName">Required String. The path and file name of the picture.</param>
        /// <param name="LinkToFile">Optional Object. True to link the picture to the file from which it was created. False to make the picture an independent copy of the file. The default value is False.</param>
        /// <param name="SaveWithDocument">Optional Object. True to save the linked picture with the document. The default value is False.</param>
        /// <param name="Range">Optional Object. The location where the picture will be placed in the text. If the range isn't collapsed, the picture replaces the range; otherwise, the picture is inserted. If this argument is omitted, the picture is placed automatically.</param>
        public void InsertPic(string FileName, object LinkToFile, object SaveWithDocument, object Range)
        {
            _wordApplication.Selection.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Range);
        }
 
        /// <summary>
        /// ������ǩ
        /// �������ͬ����ǩ������ɾ���ٲ���
        /// </summary>
        /// <param name="bookMarkName">��ǩ��</param>
        public void InsertBookMark(string bookMarkName)
        {
            //��������ɾ��
            if (_wordDocument.Bookmarks.Exists(bookMarkName))
            {
                DeleteBookMark(bookMarkName);
            }
            object range = _wordApplication.Selection.Range;
            _wordDocument.Bookmarks.Add(bookMarkName, ref range);
 
        }
 
        /// <summary>
        /// ɾ����ǩ
        /// </summary>
        /// <param name="bookMarkName">��ǩ��</param>
        public void DeleteBookMark(string bookMarkName)
        {
            if (_wordDocument.Bookmarks.Exists(bookMarkName))
            {               
                var bookMarks =_wordDocument.Bookmarks;
                for (int i = 1; i<=bookMarks.Count;i++ )
                {
                    object index = i;
                    var bookMark =bookMarks.get_Item(ref index);
                    if (bookMark.Name==bookMarkName)
                    {
                        bookMark.Delete();
                        break;
                    }
                }
            }
        }
 
        /// <summary>
        /// ɾ��������ǩ
        /// </summary>
        public void DeleteAllBookMark()
        {
            for (;_wordDocument.Bookmarks.Count>0;)
            {
                object index = _wordDocument.Bookmarks.Count;
                var bookmark = _wordDocument.Bookmarks.get_Item(ref index);
                bookmark.Delete();
            }
        }
        #endregion
 
    }
}