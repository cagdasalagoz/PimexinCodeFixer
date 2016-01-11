using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace pimexin_code_fixer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Find&Edit all .html files in the given folder.
        /// </summary>
        /// <param name="fromPath">Source Path</param>
        /// <param name="toPath">Destination Path</param>
        /// <param name="findText">Text to find in the html files.</param>
        /// <param name="replaceText">Text to replace with found texts.</param>
        private string EditHtmlByConfigFile(String input) {
            string[,] html = new string[100, 2];
            html = GetReplaceArray(Ayarlar.Default.HmlConfigPath);
            /*
             * Read all the text inside "from" file.
             * Change txt_find's to txt_replace'es.
             * Write the changes to output string.
             * Write the output String to "to" file.
             */

            string output = input;
                for (int i = 0; i < html.Length; i++)
                {
                    if (html[i, 0] == null || html[i, 1] == null) { break; }
                    output = output.Replace(html[i,0], html[i,1]);
                    }

                return output;
        }
        /// <summary>
        /// Find&Edit all .css files in the given folder and copy
        /// to destination folder.
        /// </summary>
        /// <param name="fromPath">Source Path</param>
        /// <param name="toPath">Destination Path</param>
        /// <param name="findText">Text to find in the html files.</param>
        /// <param name="replaceText">Text to replace with found texts.</param>
        private string EdictCssByConfigFile(String input) {
            string[,] css = new string[100, 2];
            css = GetReplaceArray(Ayarlar.Default.CssConfigPath);

                string output = input;
                for (int i = 0; i < css.Length; i++)
                {
                    if (css[i, 0] == null || css[i, 1] == null) { break; }
                    output = output.Replace(css[i, 0], css[i, 1]);

                }

                return output;
            
        }
        /// <summary>
        /// Move jpg files from one folder to another.
        /// </summary>
        /// <param name="fromPath">Source Path</param>
        /// <param name="toPath">Destination Path</param>
        private void MoveJpgFiles(String fromPath, String toPath){

            /*Copy all jpg files to the destination directory.*/
            string[] filePaths_jpg = Directory.GetFiles(fromPath + "\\", "*.jpg");
            foreach (string item in filePaths_jpg)
            {
                String pathWithFileName = toPath + "\\" + Path.GetFileName(item);
                System.IO.File.Copy(item, pathWithFileName);
            }
    }
        /// <summary>
        /// Gets settings for replaceses from a text file.
        /// Returns a 2D string array
        /// </summary>
        /// <param name="pathOfText">config file path</param>
        /// <returns>2D string array, [first,old]</returns>
        private string[,] GetReplaceArray(string pathOfText) {
            String input = File.ReadAllText(pathOfText);

            int i = 0, j = 0;
            string[,] result = new string[100, 2];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(','))
                {
                    result[i, j] = col.Trim();
                    j++;
                }
                i++;
            }
            return result;
        }
        /// <summary>
        /// Returns used Css file names with extention.
        /// </summary>
        /// <param name="htmlpath">path of html file.</param>
        /// <returns>css names as arraylist with order.</returns>
        private List<string> GetCssNames(string input) {
            List<string> list = new List<string>();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(input);
            var nodes = doc.DocumentNode.SelectNodes("//link[@type='text/css']");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    list.Add(node.GetAttributeValue("href", ""));
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null)
                    {

                        list[i] = list[i].Remove(0, 4);
                    }
                }
            }
            
            return list;
        }
        /// <summary>
        /// Merges given css files into one.
        /// </summary>
        /// <param name="pathOfCssFiles">Path of the css files as an string array.</param>
        /// <param name="outputPath">Output path.</param>
        private string MergeCssFiles(List<string> pathOfCssFiles,string outputPath) {
            string output = "";
            for (int i = 0; i < pathOfCssFiles.Count; i++)
            {
                string content = System.IO.File.ReadAllText(pathOfCssFiles[i]);
                output = output + "\r\n" + content;
            }
            return output;
        }
        /// <summary>
        /// Gets all class names as array from an html file
        /// </summary>
        private List<string> GetClassNamesFromHtml(string input) {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(input);
            List<string> nameslist = new List<string>();
           
            var nodes = doc.DocumentNode.SelectNodes("//*[@class]");
            if (nodes != null) {

                foreach (var node in nodes)
                {
                    if (!nameslist.Contains(node.GetAttributeValue("class", "")))
                    {
                        nameslist.Add(node.GetAttributeValue("class", ""));
                    }

                }
            }
            return SeperateRowsIntoList(nameslist);
        }
        /// <summary>
        /// Seperate a string by ' ' char to a new list.
        /// Returns the new seperated, large list.
        /// </summary>
        /// <param name="oldList">old string list</param>
        /// <returns>new longer seperated string list</returns>
        private List<string> SeperateRowsIntoList(List<string> oldList) {
            List<string> newList = new List<string>();

            for (int i = 0; i < oldList.Count; i++)
            {
                List<string> temp = new List<string>();
                temp = oldList[i].Split(' ').ToList();
                for (int j = 0; j < temp.Count; j++)
                {
                    if (!newList.Contains(temp[j]))
                    {
                        newList.Add(temp[j]);   
                    }
                }
            }

            return newList;
        }
        /// <summary>
        /// Renames every element in a string array,
        /// oldname to pimexin-oldname
        /// </summary>
        /// <param name="oldNames">Old names as a string</param>
        /// <returns>new array with newnames</returns>
        private List<string> RenameClassNames(List<string> oldNames) {
           
            List<string> newNames = new List<string>();
            for (int i = 0; i < oldNames.Count; i++)
            {  
                newNames.Add("pimexin-" + oldNames[i]);
            }
            return newNames;
        }
        /// <summary>
        /// Change all class names to new unique names.
        /// x to pimexin-x
        /// y to pimexin-y etc.
        /// Save the new html do destination path.
        /// </summary>
        /// <param name="oldnames">names to look and change for</param>
        /// <param name="newnames">new names. In the same order with olds' names.</param>
        /// <param name="sourcePath">Source html path</param>
        /// <param name="destinationPath">Destination html file path.</param>
        private string FixHtmlClassNames(string input){
            
            List<string> oldnames = GetClassNamesFromHtml(input);
            
            List<string> newnames = RenameClassNames(oldnames);

            string output = input;

            for (int i = 0; i < oldnames.Count; i++)
            {
                if (oldnames[i] != "") {
                    output = output.Replace("\"" + oldnames[i], "\"" + newnames[i]); //replace old names with news.
                     output = output.Replace(" " + oldnames[i], " " + newnames[i]); 

                    output = output.Replace("pimexin-pimexin-", "pimexin-"); //fix secondary renames.
                }
                
            }

            return output;
        }
        /// <summary>
        /// Rename generic tags.
        /// </summary>
        /// <param name="input">content as string</param>
        /// <returns>Content with renamed tags</returns>
        private string FixHtmlGenericClassNames(string input) {
            string output =input;

            string[,] replacearray = new string[100,2];

            replacearray = GetReplaceArray(Ayarlar.Default.HmlConfigPath);

            for (int i = 0; i < replacearray.Length; i++)
            {
                if (replacearray[i, 0] == null || replacearray[i, 1] == null) { break; }

                    output = output.Replace(replacearray[i,0], replacearray[i,1]); //replace old names with news.
                    output = output.Replace("pimexin-pimexin-", "pimexin-"); //fix secondary renames.
                    
            }

            return output;
        }
        /// <summary>
        /// Deletes the firs X rows from a string.
        /// </summary>
        /// <param name="input">String input</param>
        /// <param name="linesToRemove">Line count</param>
        /// <returns>String without first x row</returns>
        private string DeleteCommentsFromHtml(string input) {
         string output = input;

         HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
         doc.LoadHtml(input);
         if (doc.DocumentNode.SelectNodes("//comment()")!=null)
         foreach (HtmlNode comment in doc.DocumentNode.SelectNodes("//comment()"))
         {
             comment.ParentNode.RemoveChild(comment);
         }

         return doc.DocumentNode.OuterHtml;
        }
        /// <summary>
        /// Deletes the css file lines from html file and
        /// adds a new css file line with a new name.
        /// </summary>
        /// <param name="input">html input as string</param>
        /// <param name="newCssName">New css file name for new line</param>
        /// <returns>New html file as string.</returns>
        private string CleanCssLinesFromHtml(string input,string newCssName) {
            string output = input;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(output);

            List<string> cssNames = new List<string>();
            cssNames = GetCssNames(input);
            
            for (int i = 0; i < cssNames.Count; i++)
            {
                string old = @"<link rel=""stylesheet"" type=""text/css"" href=""css/"+cssNames[i]+"\">";

                if (cssNames[i] == cssNames.Last())
                {
                    output = output.Replace(old, @"<link rel=""stylesheet"" type=""text/css"" href=""css/" + newCssName + "\">");
                }
                else {
                    output = output.Replace(old, " ");
                }
                
            }

            return output;
        }
        /// <summary>
        /// Deletes the <HEAD> tag and recreates with  new values.
        /// New Head has 2 row: charset and css file path.
        /// </summary>
        /// <param name="input">Html page as string</param>
        /// <param name="newCssName">new file name for css line</param>
        /// <param name="charset">charset</param>
        /// <returns>Edited html as string.</returns>
        private string CleanHtmlHeadTag(string input) {
            string output = input;
           
            List<string> newCssName = new List<string>();
            newCssName = GetCssNames(input);

            string pattern = @"(?<=\<head\>)(.*)(?=\<\/head>)";

            Regex betweenSelectorsRegex = new Regex(pattern, RegexOptions.Singleline);
           string insideOfhead = betweenSelectorsRegex.Match(output).ToString();
           
           string newhead= "\r\n" +
                @"<meta charset=""utf-8"">" +
                "\r\n";

                foreach (string item in newCssName)
                {
                    newhead += @"<link rel=""stylesheet"" type=""text/css"" href=""css/" + item + "\">" +
                "\r\n";
                }
                if (insideOfhead != "") {
                    output = output.Replace(insideOfhead, newhead);
                
                }
          
 
            return output;
        }
        /// <summary>
        /// Deletes script tags from html file
        /// </summary>
        /// <param name="input">string input html file</param>
        /// <returns>string output html without scripts</returns>
        private string RemoveScripts(string input) {
            string output = input;

           
            output = Regex.Replace(output, @"(\<script)(.*)(\<\/script>)", string.Empty, RegexOptions.Singleline);

            return output;
        }

        private string RemoveFontface(string input){
        string output = input;

        output = Regex.Replace(output, @"(@font-face)(.*?)(?=\[)", string.Empty, RegexOptions.Singleline);

            return output;
        }

        /// <summary>
        /// Returns an Html file as a string
        /// </summary>
        /// <param name="sourcePath">Source file path</param>
        /// <returns>string html</returns>
        private string FileToString(string sourcePath) { 
        string stringHtml = File.ReadAllText(sourcePath);
        return stringHtml;
        }
        /// <summary>
        /// Function handles all edits for Html file.
        /// Calls functions and creates a output file.
        /// </summary>
        /// <param name="sourcePath">Source path as string</param>
        /// <param name="destinationPath">Destination path as string</param>
        private void HtmlHandler(string sourcePath,string destinationPath,List<string> cssNames) {
            
            string input = FileToString(sourcePath);
            string output = input;

            output = FixHtmlClassNames(output);
            output = FixHtmlGenericClassNames(output);
            output = CleanHtmlHeadTag(output);
            output = DeleteCommentsFromHtml(output);
            output = RemoveScripts(output);
          //  output = CleanCssLinesFromHtml(output, "newcssname");
            List<string> ImageNames = new List<string>();
            List<string> ImagePaths = new List<string>();
            output = DeleteHtmlTag(output);
            
            
           // ImagePaths = GetImagePaths(output);
           // ImageNames = GetNamesFromPaths(ImagePaths);
            File.WriteAllText(destinationPath, output);
        }
        /// <summary>
        /// Gets images path from html file.
        /// </summary>
        /// <param name="input">html file as string</param>
        /// <returns>string list of paths of images used in html.</returns>
        private List<string> GetImagePaths(string input) {
            List<string> listOfImagePaths = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(input);

            var nodes = doc.DocumentNode.SelectNodes("//img");
            if (nodes != null) {
                foreach (var node in nodes)
                {
                    if(!listOfImagePaths.Contains(node.GetAttributeValue("src","")))
                    listOfImagePaths.Add(node.GetAttributeValue("src",""));
                }
            }

            return listOfImagePaths;
        }
        /// <summary>
        /// Gets images names from image paths list.
        /// </summary>
        /// <param name="imagePaths">paths of images as string list</param>
        /// <returns>string name list of image files.</returns>
        private List<string> GetNamesFromPaths(List<string> imagePaths) {
            List<string> names = new List<string>();

            foreach (string path in imagePaths)
	    {
        names.Add(Path.GetFileName(path));
	    }
            return names;
        }
        /// <summary>
        /// Deletes the <HTML> from given string html.
        /// </summary>
        /// <param name="input">html page as string</param>
        /// <returns>html page without html tag as string</returns>
        private string DeleteHtmlTag(string input) { 
            string output = input;
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
           doc.LoadHtml(input);
           var node = doc.DocumentNode.SelectSingleNode("html");

           if (node != null) {
               output = node.InnerHtml;
           }
           return output;
        }
        /// <summary>
        /// Applies HtmlHandler function to all html files in given folder.
        /// Creates the new html files to given destination folder.
        /// </summary>
        /// <param name="sourceFolder">Path of html files</param>
        /// <param name="destinationFolder">Path of destination folder</param>
        private void HtmlFolderHandler(string sourceFolder,string destinationFolder,List<string> cssNames) {
        
            string[] filePaths_html = Directory.GetFiles(sourceFolder + "\\", "*.html");
            foreach (string item in filePaths_html)
            {
                String pathWithFileName = destinationFolder + "\\" + Path.GetFileName(item);
                HtmlHandler(item,pathWithFileName,cssNames);
            }
        }
        /// <summary>
        /// Deletes all comments from a css file.
        /// </summary>
        /// <param name="input">String css file</param>
        /// <returns>Css file without comment lines.</returns>
        private string CleanCssComments(string input) {
            string output = input;

            output = Regex.Replace(input, @"/\*.+?\*/", string.Empty, RegexOptions.Singleline);
 
            return output;
        }
        /// <summary>
        /// Returns css selectors as a string list. 
        /// Some items may have 2 line.
        /// </summary>
        /// <param name="input">Css file as string</param>
        /// <returns>Css selectors as string list</returns>
        private List<string> GetCssSelectors(string input) {
            List<string> list = new List<string>();

            MatchCollection matchList = Regex.Matches(input, @"([^\r\n,{}]+)(,(?=[^}]*{)|\s*{)");
            list = matchList.Cast<Match>().Select(match => match.Value).ToList();

            return list;
        }
        /// <summary>
        /// Splits the list items that have 2 line in one.
        /// </summary>
        /// <param name="names">List of the selectors.</param>
        /// <returns>Corrected list of the selectors.</returns>
        private List<string> CssNamesListSplitter(List<string> names)
        {
            List<string> list = new List<string>();

            foreach (string name in names)
            {
                foreach (string item in name.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries))
	                {
                                var ch = '.';
                                if (item.IndexOf(ch) != item.LastIndexOf(ch))
                                {
                                    //if string has 2 '.'
                                    foreach (string subitem in item.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                            list.Add(subitem.Trim());
                                    }
                                }
                                else { list.Add(item.Trim()); }
	                } 
            }
            list = list.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            return list;
        }
        /// <summary>
        /// Finds and replaces and fixes the errors made by him.
        /// </summary>
        /// <param name="input">Css file as string</param>
        /// <param name="oldNames">Selectors</param>
        /// <returns>New css file with new names as string.</returns>
        private string CssFindAndReplace(string input,List<string> oldNames) {
            string output = input;
            string replacement;
            for (int i = 0; i < oldNames.Count; i++)
            {
                if (oldNames[i] != null || oldNames[i] != "") {
                    if (oldNames[i][oldNames[i].Length-1].Equals('{'))
                    {
                        replacement = ".pimexin-" + oldNames[i] + "{";
                        output = output.Replace(oldNames[i], replacement );
                    }
                    else if(oldNames[i][oldNames[i].Length-1].Equals(' ')){
                        output = output.Replace(oldNames[i]+".", ".pimexin-" + oldNames[i]+".");
                    
                    }
                   
                    output = output.Replace(",{", ","); //config file uses comma too! Thats why i did not include this line to config file
                    output = EdictCssByConfigFile(output);

                    string alphabet = "abcdfghijkmnopqrsuvwxyz"; //except(l-t-e) label.pimexin and text.pimexin and image.pimexin
                    foreach (char c in alphabet)
	                        {
		                         string search = c + ".pimexin-";
                                 output = output.Replace(search, c.ToString());
	                        }
                        }
                //a.pimexin-b yi temizliyor.
                for (char c = 'a'; c <= 'z'; c++)
                {
                    for (char b = 'a'; b <= 'z'; b++)
                    {
                        string search2 = c + ".pimexin-" + b;
                        output = output.Replace(search2, c.ToString() + b.ToString());
                    }
                } 

                    
                    string exeptionalchars = "%@0123456789";
                    foreach (char item in exeptionalchars)
                    {
                        string search =".pimexin-"+item;
                        output = output.Replace(search, item.ToString());
                    }
                    
            

            }
            return output;

        }
        /// <summary>
        /// Handles a css files edits. Calls the other functions.
        /// </summary>
        /// <param name="cssFilesUrl">Css files full path.</param>
        /// <param name="outputPath">Destination path for css file with name.</param>
        private void CssHandler(List<string> cssFilesUrl,string outputPath) {
            string output;
            List<string> nameslist = new List<string>();

            
            output = MergeCssFiles(cssFilesUrl, outputPath); //merged css file

            output = CleanCssComments(output);

            nameslist = GetCssSelectors(output);
            nameslist = CssNamesListSplitter(nameslist);
          //  output = CssListCleanItemSpaces(output);
            output = CssFindAndReplace(output,nameslist);
            

            File.WriteAllText(outputPath, output);
        }
        /// <summary>
        /// Deletes the duplicated items from list.
        /// </summary>
        /// <param name="names">List to check</param>
        /// <returns>New list</returns>
        private List<string> CssNamesListTrimmer(List<string> names)
        {
            List<string> list = new List<string>();

            foreach (var name in names)
            {
                if (!list.Contains(name))
                {
                    //bastan sondan karakter silme eklenecek
                    list.Add(name);
                }
            }
            return list;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, true);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        
        private List<string> CssSelectorsGet2 (string input){
        List<string> selectors = new List<string>();

        input = RemoveFontface(input);

        MatchCollection matchList = Regex.Matches(input, @"([^{}]+)((?=[^}]*{)|(\s)*{)");
            //following line causing delay for font and background lines.
        selectors = matchList.Cast<Match>().Select(match => match.Value).ToList(); 
            return selectors;
        }

        private string CssFileFixer2(List<string> selectors,string input) {
            string output="";
            string insideOfSelector = "not getted!",
                   newselectorname,
                   insideLoopInput=input,
                   secondselector;
                    
            for (int i = 0; i < selectors.Count; i++)
            {       //birinci ve ikinci eleman atamaları
                    newselectorname = selectors[i];
                    if (i != selectors.Count-1)
                    {
                        secondselector = selectors[i + 1];
                    }
                    else {
                        secondselector = "$";
                    }
                    
                    //eğer birinci ya da ikinci eleman media ise ona göre gereken düzenlemeler.
                    if (newselectorname.Contains("@media") || newselectorname.Contains("[") ||
                        newselectorname.Contains("(") || newselectorname.Contains("*") || newselectorname.Contains("^") || newselectorname.Contains("+"))
                    {
                        newselectorname = newselectorname + "{";
                        newselectorname = newselectorname.Replace("(", @"\(");
                        newselectorname = newselectorname.Replace(")", @"\)");
                        newselectorname = newselectorname.Replace("]", @"\]");
                        newselectorname = newselectorname.Replace("[", @"\[");
                        newselectorname = newselectorname.Replace("*", @"\*");
                        newselectorname = newselectorname.Replace("^", @"\^");
                        newselectorname = newselectorname.Replace("+", @"\+");
                    }
                    if (secondselector.Contains("@media") || secondselector.Contains("[") ||
                        secondselector.Contains("(") || secondselector.Contains("*") || secondselector.Contains("^") || secondselector.Contains("+"))
                    {
                        secondselector = secondselector.Replace("(", @"\(");
                        secondselector = secondselector.Replace(")", @"\)");
                        secondselector = secondselector.Replace("]", @"\]");
                        secondselector = secondselector.Replace("[", @"\[");
                        secondselector = secondselector.Replace("*", @"\*");
                        secondselector = secondselector.Replace("^", @"\^");
                        secondselector = secondselector.Replace("+", @"\+");
                    }

                    //birinci ile ikinci arasındaki değerleri alan regex.
                    string pattern = "(?<=" + newselectorname + ")(.*?)(?=" + secondselector + ")";
                    Regex betweenSelectorsRegex = new Regex(pattern, RegexOptions.Singleline);
                    insideOfSelector = betweenSelectorsRegex.Match(insideLoopInput).ToString() + "\n";
                    

                    //eğer birinci ya da ikinci isim font ya da daha değişik bir tanım değilse fonksiyonun içine gir.
                    if (!newselectorname.Contains("@font") && !newselectorname.Contains('[')
                        && !newselectorname.Contains("\\* ") && !newselectorname.Contains("%") && !newselectorname.Contains("@"))
                    {
                        newselectorname = newselectorname.TrimStart();

                        //eğer virgül varsa yani alt alta tanımlama yapılmışsa her birinin başına pimexin ekle.
                        if (newselectorname.Contains(",\n"))
                        {
                            newselectorname = newselectorname.Replace(",\n", ",\n.pimexin-");
                        }
                        if (newselectorname.Contains(".\n."))
                        {
                            newselectorname = newselectorname.Replace(".\n.", ".\n.pimexin-");
                        }
                        // eğer ilk karakter nokta ise başına pimexin ekle. Tanımlama var demektir.
                        if (newselectorname[0].Equals('.'))
                        {
                            newselectorname = newselectorname.Replace(".", ".pimexin-");
                        }
                        //ilk karakter nokta değilse generic bir selector olduğu için başına direk pimexin ekle.
                        else
                        {
                            newselectorname = ".pimexin-" + newselectorname;
                        }
                    }
                   // if (i == selectors.Count - 1) {// insideOfSelector += "}"; }
                    //eğer media ise önceden eklediğin \ işaretlerini kaldır.
                    if (newselectorname.Contains("@media") || newselectorname.Contains("[") || newselectorname.Contains("(")
                        || newselectorname.Contains("*") || newselectorname.Contains("^") || newselectorname.Contains("+"))
                    {
                        insideOfSelector = insideOfSelector.Replace(".", ".pimexin-");
                        newselectorname = newselectorname.Replace(@"\(", "(");
                        newselectorname = newselectorname.Replace(@"\)", ")");
                        newselectorname = newselectorname.Replace(@"\]","]" );
                        newselectorname = newselectorname.Replace(@"\[", "[");
                        newselectorname = newselectorname.Replace(@"\*","*" );
                        newselectorname = newselectorname.Replace(@"\^","^" );
                        newselectorname = newselectorname.Replace(@"\+", "+");
                        insideOfSelector = EditHtmlByConfigFile(insideOfSelector);
                    }
                    //çıktının sonuna ekle selector adını ve içini
                    output += newselectorname + insideOfSelector.Trim() + "\n";
                    //işi biten kısmı ana dosyadan sil.
                    insideLoopInput = betweenSelectorsRegex.Replace(insideLoopInput, " ", 1);
                    var regex = new Regex(Regex.Escape(selectors[i]));
                    insideLoopInput = regex.Replace(insideLoopInput, " ", 1);
			}

            //hata olarak oluşan arka arkaya pimexinleri düzelt.
            output = EdictCssByConfigFile(output);
            
            return output;
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            /*
             * Open a dialog for selecting file.
             * Set selected path to txt_from_path TextView 
             * as text.
             */
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select the source folder.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_from_path.Text = dialog.SelectedPath;
            }
        }

        private void btn_browse2_Click(object sender, EventArgs e)
        {
            /*
             * Open a dialog for selecting file.
             * Set selected path to txt_to_path TextView 
             * as text.
             */
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select the destination folder.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_to_path.Text = dialog.SelectedPath;
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            string inputPath = txt_from_path.Text;
            string outputPath = txt_to_path.Text;
            string inputImagesFolder = txt_from_path.Text+"\\images\\";
            string inputCssFolder = txt_from_path.Text + "\\css\\";
            string outputImagesFolder = txt_to_path.Text + "\\images\\";
            string outputCssFolder = txt_to_path.Text + "\\css\\";
            string output;

            //Copy image folder to output image folder.
            DirectoryCopy(inputImagesFolder, outputImagesFolder, true);
            
            //get css files paths to an array.
            string[] cssFilesPaths = Directory.GetFiles(inputCssFolder,"*css");
            
            //create a css file if it doesn't exist.
            bool exists = System.IO.Directory.Exists(outputCssFolder);

            if (!exists)
            { System.IO.Directory.CreateDirectory(outputCssFolder); }

            foreach (string item in cssFilesPaths)
            {
             
             string filename=Path.GetFileName(item);

            String input = File.ReadAllText(item);
            input = CleanCssComments(input);
            List<string> selectors = CssSelectorsGet2(input);
            
            output = CssFileFixer2(selectors, input);

            File.WriteAllText(outputCssFolder+filename, output);
            }

            //get html files paths.
            string[] htmlFilesPaths = Directory.GetFiles(inputPath,"*html");


            //create a array for css file names for adding to hmtl head tag.
           List<string> cssFileNames = new List<string>();
            
            foreach (string item in cssFilesPaths)
	        {
		        cssFileNames.Add(Path.GetFileName(item));
	        }

            foreach (string item in htmlFilesPaths)
            {
                string filename = Path.GetFileName(item);

            HtmlHandler(item,outputPath+"\\"+filename,cssFileNames); 
            }

            MessageBox.Show("Output created.", "Important Message");
        }

       
    }
}
