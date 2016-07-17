using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using HtmlAgilityPack;
using System.Data.OleDb;

namespace MyZhihuClawer
{
    class Program
    {
        static void Main(string[] args)
        {
            GetUserInfo gui = new GetUserInfo();
            gui.BuildCookie();
            //gui.GetMethod();
            //gui.GetFollowerList();
            //gui.MergeFile();
            //gui.GetList();
        }
    }
    class GetUserInfo
    {
        CookieContainer cookieContainer = new CookieContainer();
        public void BuildCookie()
        {
            Cookie c = new Cookie("__utma", "51854390.2061686372.1459752138.1468401538.1468401538.1", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("__utmb", "51854390.6.10.1468401538", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("__utmc", "51854390", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("__utmt", "1", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("__utmv", "51854390.100-1|2=registration_date=20130227=1^3=entry_date=20130227=1", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("__utmz", "51854390.1468401538.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)	", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("_ga", "GA1.2.2061686372.1459752138", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("_xsrf", "77e01c051bbab5811c43bc4241555330", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("_za", "e390225c-2425-493d-935a-b410d2cf5064", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("_zap", "bc6fd266-0841-4bba-8ee7-cbe9bb0acf7f", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("a_t", "\"2.0AABAJWMaAAAXAAAAuI6tVwAAQCVjGgAAACAAe43rpQkXAAAAYQJVTZWQlVcAAKA9TYtn-eSvwX1ApzzqZsnKHjhvCyHepKP95OwV9dez2bs8LOUPVA==\"", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("cap_id", "\"NzZkM2ZkZWMwODk0NDVlNzk1ODUxNmEwOTRkMGUzZGM=|1466827655|853f1cddde440c9c4c15a1812f1c6d1be2149d66\"", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("d_c0", "\"ACAAe43rpQmPTgdfH57pmxUnuDTSPirIMbY=|1461254400\"", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("l_cap_id", "\"NjU4Yzc4YWI5YWJiNDk1YzhiYzMyYTU0MjU5ODVhMmY=|1466827655|f93927bc39f1423fc098fccf2a0cf87244fed32b\"", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("login", "\"NjUwYTY2NWQ1MzkxNDliYTliYTc1ZjMwNDNmYjQyYjQ=|1466827669|3cfd0b9e3091f6e092dd28643ddc1061a13f07c4\"", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("q_c1", "dae415376b99445fb4a2fd03c0298a38|1466314504000|1456891021000", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("udid", "\"ACCA-b5glAmPTlb8dmvqmfhBlJbxDfctawM=|1457584740\"", "/", "www.zhihu.com");
            cookieContainer.Add(c);
            c = new Cookie("z_c0", "Mi4wQUFCQUpXTWFBQUFBSUFCN2pldWxDUmNBQUFCaEFsVk5sWkNWVndBQW9EMU5pMmY1NUtfQmZVQ25QT3BteWNvZU9B|1466827669|57b0c6b2eca60f0fe86eb49b625150a30ef7ce58", "/", "www.zhihu.com");
            cookieContainer.Add(c);
        }
        public void GetMethod()
        {
            string url = "https://www.zhihu.com/people/andyliao92/followers";
            WebRequest request = WebRequest.Create(url);
            HttpWebRequest rq = (HttpWebRequest)request;
            rq.Method = "GET";
            rq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //rq.Headers.Set("Accept-Encoding", "gzip,deflate");
            rq.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8");
            rq.Headers.Set("Cache-Control", "max-age=0");
            rq.KeepAlive = true;
            rq.CookieContainer = cookieContainer;        
            rq.Host = "www.zhihu.com";
            rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36 SE 2.X MetaSr 1.0";
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                response = rq.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            string sr = reader.ReadToEnd();
            reader.Close();
            FileStream fs = new FileStream("page.html", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(sr);
            sw.Close();
            fs.Close();
            Console.WriteLine("Page Read Finished!");
            Console.ReadKey();
        }
        public void GetFollowerList()
        {
            for (int i = 20; i <= 960; i += 20)
            {
                string param = "method=next&params=%7B%22offset%22%3A" + i + "%2C%22order_by%22%3A%22created%22%2C%22hash_id%22%3A%22ec9a04670819c4b09138954c6777c9b4%22%7D";
                byte[] bs = Encoding.ASCII.GetBytes(param);
                string url = "https://www.zhihu.com/node/ProfileFollowersListV2";
                WebRequest request = WebRequest.Create(url);
                HttpWebRequest rq = (HttpWebRequest)request;
                rq.Method = "POST";
                rq.Accept = "*/*";
                //rq.Headers.Set("Accept-Encoding", "gzip,deflate");
                rq.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8");
                //rq.Headers.Set("Cache-Control", "max-age=0");
                rq.ContentLength = bs.Length;
                rq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                rq.KeepAlive = true;
                rq.CookieContainer = cookieContainer;
                rq.Host = "www.zhihu.com";
                rq.Headers.Set("Origin", "https://www.zhihu.com");
                rq.Referer = "https://www.zhihu.com/people/andyliao92/followers";
                rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36 SE 2.X MetaSr 1.0";
                rq.Headers.Set("X-Requested-With", "XMLHttpRequest");
                rq.Headers.Set("X-Xsrftoken", "77e01c051bbab5811c43bc4241555330");
                using (Stream reqStream = rq.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
                WebResponse response = null;
                StreamReader reader = null;
                try
                {
                    response = rq.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                string sr = reader.ReadToEnd();
                reader.Close();
                FileStream fs = new FileStream("list\\"+i+".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(sr);
                sw.Close();
                fs.Close();
                Console.WriteLine(i + "Read Finished!");
                System.Threading.Thread.Sleep(375);
            }
            Console.ReadKey();
        }
        public void MergeFile()
        {
            for (int i = 20; i <= 940; i += 20)
            {
                string filename ="list\\"+ i + ".txt";
                FileStream fs = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string data = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                StreamWriter sw = File.AppendText("list.txt");
                sw.Write(data);
                sw.WriteLine();
                sw.Close();
                fs.Close();
            }
        }
        public void GetList()
        {
            List<User> UserList = new List<User>();
            XmlTextReader xr = new XmlTextReader("1.xml");
            User currentUser = null;
            while (xr.Read())
            {
                xr.MoveToContent();
                if (xr.NodeType == XmlNodeType.EndElement && xr.Name == "file")
                    break;
                if (!xr.IsStartElement())
                    continue;
                if (xr.Name == "a")
                {
                    if (xr["class"] == "zg-link")
                    {
                        currentUser = new User();
                        currentUser.UserName = xr["title"];
                        currentUser.Url = xr["href"];
                        UserList.Add(currentUser);
                        while (xr.Read())
                        {
                            xr.MoveToContent();
                            if (!xr.IsStartElement())
                                continue;
                            if (xr.Name == "div" && xr["class"] == "zg-big-gray")
                            {
                                xr.Read();
                                currentUser.Signature = xr.Value.ToString();
                                break;
                            }
                        }
                    }
                }
            }
            xr.Close();
            Console.WriteLine("Finished Get Name List!");
            foreach (User u in UserList)
            {
                ParseUserPage(u);
                Console.WriteLine("User \"" + u.UserName + "\" finished parse!");
            }
            string sqlStr = "";
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Data.accdb");
            OleDbCommand cmd = new OleDbCommand(sqlStr, conn);
            StringBuilder sb = new StringBuilder();
            foreach (User u in UserList)
            {
                sb.Append("insert into tb values(");
                sb.Append("'" + u.UserName + "','");
                sb.Append(u.Url + "','");
                //sb.Append(u.Signature + "','");
                sb.Append(u.Location + "','");
                sb.Append(u.Business + "','");
                sb.Append(u.Gender + "','");
                sb.Append(u.Education + "','");
                sb.Append(u.EducationExtra + "','");
                //sb.Append(u.Illustration + "','");
                sb.Append(u.QuesNum + "','");
                sb.Append(u.AnsNum + "','");
                sb.Append(u.PassageNum + "','");
                sb.Append(u.CollectionNum + "','");
                sb.Append(u.PublicEdit + "','");
                sb.Append(u.LikeNum + "','");
                sb.Append(u.ThankNum + "','");
                sb.Append(u.CollectedNum + "','");
                sb.Append(u.ShareNum + "','");
                sb.Append(u.Following + "','");
                sb.Append(u.Follower + "','");
                sb.Append(u.ILikeHim + "','");
                sb.Append(u.IThankHim + "','");
                sb.Append(u.HeLikeMe + "','");
                sb.Append(u.HeThankMe + "','");
                sb.Append(u.BrowseTimes + "');");
                sqlStr = sb.ToString();
                sb = new StringBuilder();
                cmd = new OleDbCommand(sqlStr, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void GetUserPage(User currentUser)
        {
            WebRequest request = WebRequest.Create(currentUser.Url+"/about");
            HttpWebRequest rq = (HttpWebRequest)request;
            rq.Method = "GET";
            rq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //rq.Headers.Set("Accept-Encoding", "gzip,deflate");
            rq.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8");
            rq.Headers.Set("Cache-Control", "max-age=0");
            rq.KeepAlive = true;
            rq.CookieContainer = cookieContainer;
            rq.Host = "www.zhihu.com";
            rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36 SE 2.X MetaSr 1.0";
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                response = rq.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            string sr = reader.ReadToEnd();
            reader.Close();
            FileStream fs = new FileStream("page\\"+currentUser.UserName+"-page.html", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(sr);
            sw.Close();
            fs.Close();
            Console.WriteLine("User \"" + currentUser.UserName + "\" Page Read Finished!");
        }
        public void ParseUserPage(User currentUser)
        {
            string file = "page\\" + currentUser.UserName + "-page.html";
            HtmlDocument doc = new HtmlDocument();
            StreamReader sr = File.OpenText(file);
            doc.Load(sr);
            HtmlNodeCollection nc = doc.DocumentNode.SelectNodes("//span");
            foreach (HtmlNode child in nc)
            {
                if (child.Attributes["class"]!=null)
                {
                    switch(child.Attributes["class"].Value)
                    {
                        case "location item":
                            currentUser.Location = child.Attributes["title"].Value;
                            break;
                        case "business item":
                            currentUser.Business = child.Attributes["title"].Value;
                            break;
                        case "item gender":
                            {
                                foreach (HtmlNode cn in child.ChildNodes)
                                {
                                    if (cn.Name == "i" && cn.Attributes["class"].Value == "icon icon-profile-male")
                                    {
                                        currentUser.Gender = "male";
                                    }
                                    else if (cn.Name == "i" && cn.Attributes["class"].Value == "icon icon-profile-male")
                                    {
                                        currentUser.Gender = "female";
                                    }
                                }
                            }
                            break;
                        case "education item":
                            currentUser.Education = child.Attributes["title"].Value;
                            break;
                        case "education-extra item":
                            currentUser.EducationExtra = child.Attributes["title"].Value;
                            break;
                        case "description unfold-item":
                            {
                                foreach (HtmlNode cn in child.ChildNodes)
                                {
                                    if (cn.Name == "span" && cn.Attributes["class"].Value == "content")
                                    {
                                        currentUser.Illustration = cn.InnerText;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            nc = doc.DocumentNode.SelectNodes("//a");
            foreach (HtmlNode child in nc)
            {
                if (child.Attributes["class"] == null)
                    continue;
                if (child.Attributes["class"].Value == "item ")
                {
                    if(child.InnerText.Contains("提问"))
                        currentUser.QuesNum = child.ChildNodes[1].InnerText;
                    else if (child.InnerText.Contains("回答"))
                        currentUser.AnsNum = child.ChildNodes[1].InnerText;
                    else if (child.InnerText.Contains("文章"))
                        currentUser.PassageNum = child.ChildNodes[1].InnerText;
                    else if (child.InnerText.Contains("收藏"))
                        currentUser.CollectionNum = child.ChildNodes[1].InnerText;
                    else if (child.InnerText.Contains("公共编辑"))
                        currentUser.PublicEdit = child.ChildNodes[1].InnerText;
                }
            }
            nc = doc.DocumentNode.SelectNodes("//div");
            foreach (HtmlNode child in nc)
            {
                if (child.Attributes["class"] == null)
                    continue;
                if (child.Attributes["class"].Value == "zm-profile-module zm-profile-details-reputation")
                {
                    HtmlNodeCollection ico = child.ChildNodes[3].ChildNodes;
                    foreach (HtmlNode cn in ico)
                    {
                        if (cn.Name=="span" && cn.Attributes["class"]==null && cn.InnerText.Contains("赞同"))
                            currentUser.LikeNum = cn.ChildNodes[1].InnerText;
                        if (cn.Name == "span" && cn.Attributes["class"] == null && cn.InnerText.Contains("感谢"))
                            currentUser.ThankNum = cn.ChildNodes[1].InnerText;
                        if (cn.Name == "span" && cn.Attributes["class"] == null && cn.InnerText.Contains("收藏"))
                            currentUser.CollectedNum = cn.ChildNodes[1].InnerText;
                        if (cn.Name == "span" && cn.Attributes["class"] == null && cn.InnerText.Contains("分享"))
                            currentUser.ShareNum = cn.ChildNodes[1].InnerText;
                    }
                }
                else if(child.Attributes["class"].Value=="zm-profile-side-following zg-clear")
                {
                    foreach(HtmlNode cn in child.ChildNodes)
                    {
                        if(cn.Name=="a")
                        {
                            if(cn.ChildNodes[1].InnerText=="关注了")
                            {
                                currentUser.Following = cn.ChildNodes[4].InnerText;
                            }
                            else if(cn.ChildNodes[1].InnerText=="关注者")
                            {
                                currentUser.Follower=cn.ChildNodes[4].InnerText;
                            }
                        }
                    }
                }
                else if (child.Attributes["class"].Value == "vote-thanks-relation zg-gray-normal")
                {
                    foreach (HtmlNode cn in child.ChildNodes)
                    {
                        if (cn.Name == "p")
                        {
                            if (cn.ChildNodes[1].Attributes["class"].Value == "zg-icon vote")
                            {
                                for(int i=0;i<cn.ChildNodes.Count;i++)
                                {
                                    if(cn.ChildNodes[i].InnerText.Contains("赞同"))
                                    {
                                        currentUser.ILikeHim = cn.ChildNodes[i + 1].InnerText;
                                    }
                                    if (cn.ChildNodes[i].InnerText.Contains("感谢"))
                                    {
                                        currentUser.IThankHim = cn.ChildNodes[i + 1].InnerText;
                                    }
                                }
                            }
                            else if (cn.ChildNodes[1].Attributes["class"].Value == "zg-icon be-voted")
                            {
                                for (int i = 0; i < cn.ChildNodes.Count; i++)
                                {
                                    if (cn.ChildNodes[i].InnerText.Contains("赞同"))
                                    {
                                        currentUser.HeLikeMe = cn.ChildNodes[i + 1].InnerText;
                                    }
                                    if (cn.ChildNodes[i].InnerText.Contains("感谢"))
                                    {
                                        currentUser.HeThankMe = cn.ChildNodes[i + 1].InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (child.Attributes["class"].Value == "zm-side-section-inner")
                {
                    for (int i = 0; i < child.ChildNodes.Count; i++)
                    {
                        if (child.ChildNodes[i].InnerText.Contains("个人主页被"))
                        {
                            currentUser.BrowseTimes = child.ChildNodes[i].ChildNodes[1].InnerText;
                        }
                    }
                }
            }
            sr.Close();
        }
    }
    public class User
    {
        public string UserName="null";
        public string Url = "null";
        public string Signature = "null";
        public string Location = "null";
        public string Business = "null";
        public string Gender = "null";
        public string Education = "null";
        public string EducationExtra = "null";
        public string Illustration="null";
        public string QuesNum = "null";
        public string AnsNum = "null";
        public string PassageNum = "null";
        public string CollectionNum = "null";
        public string PublicEdit = "null";
        public string LikeNum = "null";
        public string ThankNum = "null";
        public string CollectedNum = "null";
        public string ShareNum = "null";
        public string Following = "null";
        public string Follower = "null";
        public string ILikeHim = "null";
        public string IThankHim = "null";
        public string HeLikeMe = "null";
        public string HeThankMe = "null";
        public string BrowseTimes = "null";
    }
}
