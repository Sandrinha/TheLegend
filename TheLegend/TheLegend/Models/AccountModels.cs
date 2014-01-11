using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Security;

namespace TheLegend.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Ask> Asks { get; set; }

        public DbSet<Humor> Humors { get; set; }

        public DbSet<Mission> Missions { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Introdution> Introdutions { get; set; }

        public DbSet<TagRelation> TagRelations { get; set; }

        public DbSet<RelationShip> RelationShips { get; set; }

        private IQueryable<UserProfile> ListTagCloud()
        {
            var query = from e in UserProfiles
                        select e;
                        
            return query;
        }
        public TagCloud GetTagCloud()
        {
            var tagCloud = new TagCloud();
            tagCloud.EventsCount = ListTagCloud().Count();
            var query = from t in Tags
                        where t.User.Count() > 0
                        orderby t.TagName
                        select new MenuTag
                        {
                            Tag = t.TagName,
                            Count = t.User.Count()
                        };
            tagCloud.MenuTags = query.ToList();
            return tagCloud;
        }

        private IQueryable<RelationShip> ListTagRelationCloud()
        {
            var query = from e in RelationShips
                        select e;

            return query;
        }
        public TagCloud GetTagRelationCloud()
        {
            var tagCloud = new TagCloud();
            tagCloud.EventsCount = ListTagRelationCloud().Count();
            var query = from t in TagRelations
                        where t.TagRelations.Count() > 0
                        orderby t.Name
                        select new MenuTag
                        {
                            Tag = t.Name,
                            Count = t.TagRelations.Count()
                        };
            tagCloud.MenuTags = query.ToList();
            return tagCloud;
        }

           
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Birth { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public int HumorId { get; set; }

        public virtual Humor Humor { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime Birth { get; set; }

        public string Sex { get; set; }

        public string Email { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
