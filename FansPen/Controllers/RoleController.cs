﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;

namespace FansPen.Web.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public FanficRepository FanficRepository;
        public CommentRepository CommentRepository;
        public RatingRepository RatingRepository;
        public LikeRepository LikeRepository;
        public ApplicationUserRepository ApplicationUserRepository;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            CommentRepository = new CommentRepository(context);
            RatingRepository = new RatingRepository(context);
            LikeRepository = new LikeRepository(context);
            ApplicationUserRepository = new ApplicationUserRepository(context);
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SetAdmin(string returnUrl, string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                }
                return RedirectPermanent(returnUrl);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SetBan(string id, string returnUrl)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "ban"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "ban");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "ban");
                }
                return RedirectPermanent(returnUrl);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            ApplicationUser userForDelete = await _userManager.FindByIdAsync(id);
            if (userForDelete != null)
            {
                ApplicationUser admin = await _userManager.FindByNameAsync("admin");
                CommentRepository.DeleteAllUserComments(id);
                RatingRepository.DeleteUserRating(id);
                LikeRepository.DeleteUserLikes(id);
                FanficRepository.SetDefaultUser(id, admin.Id);
                ApplicationUserRepository.DeleteUser(id);
                return Redirect("/");
            }
            return NotFound();
        }
    }
}

