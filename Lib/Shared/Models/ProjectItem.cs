using Blazor_App.Shared.Enums;
using Blazor_App.Shared.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blazor_App.Shared.Models
{
    public class ProjectItem
    {
        public ProjectItem()
        {
            
        }
        public string Id { get; set; }
        string title = "";
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                UpdateId();
            }
        }
        string artist = "";
        public string Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                UpdateId();
            }
        }
        public string Data { get; set; }
        public string DataLong { get; set; }
        public string Notes { get; set; }
        public string[] Tags { get; set; } = new string[] {"hillsong" };
        public string YoutubeUrl { get; set; }
        public string ExternalUrl { get; set; }
        public LanguageType Language { get; set; } = SiteInfo.Language;
        public List<Category> Categories { get; set; } = new List<Category>() { Category.LOVE };
        public string SubmittedBy { get; set; }
       
        public bool IsValid()
        {
            if (Title.IsValidString() == false)
                return false;
            if (Data.IsValidString() == false)
                return false;
            if (YoutubeUrl.IsValidString() == false)
                return false;
            return true;
        }
        public string GetQueryContent(bool advance = false)
        {
            var query = this.Title + " " + this.Notes + " " + Data + " " + this.SubmittedBy;
            if (advance)
            {
                query += " " + this.Data;
            }
            return query.ToLower();
        }
        string UpdateId()
        {
            if(Artist.IsValidString())
            {
                Id = Artist + "-" + Title;
            }
            else
            {
                Id = Title;
            }
            Id = HttpUtility.UrlEncode(Id);
            return Id;
        }
        public string GetEncodedId()
        {
            return HttpUtility.UrlEncode(this.UpdateId());
        }
        public string GetDecodedId()
        {
            return HttpUtility.UrlDecode(this.UpdateId());
        }
    }
}
