using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matlab.DataModel;

namespace Matlab.ArticleImageSuggestionTool
{
    public partial class Form1 : Form
    {
        MatlabDb Db = new MatlabDb();
        private int? _currentArticleId = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveCurrent();
            NextArticle();
        }

        private  void SaveCurrent()
        {
            if (listBox1.Items.Count > 0)
            {
                foreach (string item in listBox1.Items)
                {
                    using (FileStream fs = new FileStream(item, FileMode.Open))
                    {
                        Debug.Assert(_currentArticleId != null, nameof(_currentArticleId) + " != null");
                         Upload(_currentArticleId.Value, fs);
                    }
                }
            }
        }
        private void  Upload(int articleId, FileStream paramFileStream)
        {
            string actionUrl = "http://31.25.130.239/api/articles/addImage";
            HttpContent stringContent = new StringContent(articleId.ToString());
            HttpContent fileStreamContent = new StreamContent(paramFileStream);
            //HttpContent bytesContent = new ByteArrayContent(paramFileBytes);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {

                formData.Add(stringContent, "id");
                formData.Add(fileStreamContent, "ImageFile", paramFileStream.Name);
                //formData.Add(bytesContent, "file2", "file2");
                var x =  client.PostAsync(actionUrl, formData);
                x.RunSynchronously();
                var response = x.Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("nashod upload konam");
                }
               // return await response.Content.ReadAsStreamAsync();
            }
        }

        //static async Task MyAPIGet(HttpClient cons,int articleId)
        //{
        //    using (cons)
        //    {
        //        HttpContent hc = new MultipartFormDataContent();
        //        hc.Headers.Add("id", articleId.ToString());
        //        hc.Headers.Add();
        //        HttpResponseMessage res = await cons.PostAsync("api/articles/SuggestedImageList");
        //        res.EnsureSuccessStatusCode();
        //        if (res.IsSuccessStatusCode)
        //        {
        //            tblTag tag = await res.Content.ReadAsAsync<tblTag>();
        //            Console.WriteLine("\n");
        //            Console.WriteLine("---------------------Calling Get Operation------------------------");
        //            Console.WriteLine("\n");
        //            Console.WriteLine("tagId tagName tagDescription");
        //            Console.WriteLine("-----------------------------------------------------------");
        //            Console.WriteLine("{0}\t{1}\t\t{2}", tag.tagId, tag.tagName, tag.tagDescription);
        //            Console.ReadLine();
        //        }
        //    }
        //}

        private void NextArticle()
        {
            var article = Db.Articles.FirstOrDefault(x => x.ImageSuggests.Count == 0);
            if (article != null)
            {
                textBoxArticleTitle.Text = article.Title;
                textBoxQuestionTitle.Text = article.Questions.FirstOrDefault()?.Title + Environment.NewLine;
                var collection = article.Questions.FirstOrDefault()?.Answers;
                if (collection != null)
                    foreach (Answer answer in collection)
                    {
                        textBoxQuestionTitle.Text += answer.Text + Environment.NewLine;
                    }

                labelCategory.Text = article.Box.Package.Catergory.Title;
                labelPackage.Text = article.Box.Package.Title;
                _currentArticleId = article.Id;
                listBox1.Items.Clear();

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            NextArticle();
        }

        private void buttonRemoveImage_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(openFileDialog1.FileName);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                pictureBox1.ImageLocation = listBox1.SelectedItem.ToString();
            }
        }

        private void textBoxQuestionTitle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
