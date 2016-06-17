using System.ComponentModel.DataAnnotations;

namespace EvaluatingTool.WEB.Models
{
    public class SiteMapModel
    {
        [Required(ErrorMessage = "Enter the URL")]
        [RegularExpression(@"^((https?|ftp)\:\/\/)?([a-z0-9]{1})((\.[a-z0-9-])|([a-z0-9-]))*\.([a-z]{2,6})(\/?)$",
            ErrorMessage = "Incorrect URL")]
        public string Host { get; set; }       
    }
}