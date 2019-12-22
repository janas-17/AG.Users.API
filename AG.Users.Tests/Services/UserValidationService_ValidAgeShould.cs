using AG.Users.Data.Services;
using AG.Users.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AG.Users.Tests.Services
{
    /// <summary>
    /// Test of Age Validation method for User saving
    /// </summary>
    public class UserValidationService_ValidAgeShould
    {
        private readonly UserValidationService<AUser> _userValidationService;

        public UserValidationService_ValidAgeShould()
        {
            _userValidationService = new UserValidationService<AUser>();
        }

        [Fact]
        public void ValidAge_InputIs17_ThrowsException()
        {
            Assert.Throws<Exception>(() => _userValidationService.ValidAge(DateTime.Today.AddYears(-17)));
        }

        [Fact]
        public void ValidAge_InputIs18_ReturnTrue()
        {
            var result = _userValidationService.ValidAge(DateTime.Today.AddYears(-18));

            Assert.True(result, "18 is a valid age");
        }

        [Fact]
        public void ValidAge_InputIs121_ThrowsException()
        {
            Assert.Throws<Exception>(() => _userValidationService.ValidAge(DateTime.Today.AddYears(-121)));
        }

        [Fact]
        public void ValidAge_InputIs120_ReturnTrue()
        {
            var result = _userValidationService.ValidAge(DateTime.Today.AddYears(-120));

            Assert.True(result, "120 is pushing it, but it's a valid age!");
        }
    }
}
