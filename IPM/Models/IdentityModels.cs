using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
namespace IPM.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        public string NameCN { set; get; }
        public string StaffId { set; get; }
        public string CreatedBy { set; get; }
        public DateTime? CreatedDate { set; get; }
    }
    public class ApplicationRole : IdentityRole
    {
        public string Description { set; get; }
        public string CreatedBy { set; get; }
        public DateTime? CreatedDate { set; get; }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<IPM.Models.ApplicationUser> ApplicationUsers { get; set; }
        public System.Data.Entity.DbSet<IPM.Models.ApplicationRole> ApplicationRoles { get; set; }
    }
    public class IdentityManger
    {
        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public bool RoleExists(string RoleName)
        {
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));
            return rm.RoleExists(RoleName);
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public bool CreateRole(ApplicationRole role)
        {
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));
            var idReturn = rm.Create(role);
            return idReturn.Succeeded;
        }
        /// <summary>
        /// 将用户增加到指定角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }
        /// <summary>
        /// 清除用户所有角色
        /// </summary>
        /// <param name="userId"></param>
        public async void ClearUserRoles(string userId)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));
            var user = um.FindById(userId);
            //var currentRoles = new List<IdentityUserRole>();
            var currentRoles = await rm.Roles.ToListAsync();
            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, role.Name);
            }
        }
        /// <summary>
        /// 移除用户指定角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool ClearUserRole(string userId,string roleName)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
             var idResult = um.RemoveFromRole(userId, roleName);
             return idResult.Succeeded;
        }
    }

    
}