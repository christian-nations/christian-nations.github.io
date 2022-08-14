using Blazor_App.Shared.Enums;
using Blazor_App.Shared.Extensions;
using Newtonsoft.Json;
using SQLite;
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
            if(GuidId == null)
               GuidId = Guid.NewGuid().ToString(); 
        }
        public string GuidId { get; set; }  
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UniqueId { get; set; }
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
        public string Notes { get; set; }

        public string Tags { get; set; } = "hillsong" ;
        public string YoutubeUrl { get; set; }
        public string ExternalUrl { get; set; }
        public string Language { get; set; } = SiteInfo.Language;

        public string Categories { get; set; } = "Love";
        public string SubmittedBy { get; set; }

        [Ignore]
        public List<Comment> Comments { get; set; }

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
        void UpdateId()
        {
            UniqueId = GetId();
            UniqueId = Uri.EscapeDataString(UniqueId);
            if (GuidId == null)
            {
                GuidId = Guid.NewGuid().ToString();
            }
        }
        string GetId()
        {
            if (Artist.IsValidString())
            {
                UniqueId = Artist + "-" + Title;
            }
            else
            {
                UniqueId = Title;
            }
            return UniqueId;
        }
        public string GetEncodedId()
        {
            return Uri.EscapeDataString(this.GetId());
        }
        public string GetArtist()
        {
            if (Artist.IsValidString())
                return "" + this.Artist;
            return "";
        }
        public void CopyDataTo(ProjectItem item)
        {
            if (this.Title.IsValidString())
                item.Title = this.Title;
            if (Data.IsValidString())
            {
                if (item.Data != this.Data)
                {
                    item.Data = this.Data;
                }
            }
            if (this.Artist.IsValidString())
                item.Artist = this.Artist;
            if (this.YoutubeUrl.IsValidString())
                item.YoutubeUrl = this.YoutubeUrl;

            item.Categories = this.Categories;
            if (this.ExternalUrl.IsValidString())
                item.ExternalUrl = this.ExternalUrl;
            if (this.SubmittedBy.IsValidString())
                item.SubmittedBy = this.SubmittedBy;
            if (this.Notes.IsValidString())
                item.Notes = this.Notes;
            if(this.Comments?.Count > 0)
            {
                item.Comments = this.Comments;
            }
        }
        public void CopyDataFrom(ProjectItem item)
        {
            this.Title = item.Title;
            if (item.Data.IsValidString())
            {
                if (this.Data != item.Data)
                {
                    this.Data = item.Data;
                }
            }
            this.Artist = item.Artist;
            this.Language = item.Language;
            this.YoutubeUrl = item.YoutubeUrl;
            this.Categories = item.Categories;
            this.Tags = item.Tags;
            this.ExternalUrl = item.ExternalUrl;
            this.SubmittedBy = item.SubmittedBy;
            this.Notes = item.Notes;
            this.Comments = item.Comments;
        }
    }
}
