﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using MudBlazor.Extensions;
using MudBlazor.UnitTests.TestData;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Extensions
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        [TestCase(typeof(Adornment), new[] { "None", "Start", "End" })]
        [TestCase(typeof(Adornment?), new[] { "None", "Start", "End" })]
        [TestCase(typeof(string), new string[0])]
        public void GetSafeEnumValues_Test(Type type, string[] expectedNames)
        {
            var values = MudBlazor.Extensions.EnumExtensions.GetSafeEnumValues(type);
            var stringValues = values.Select(x => x.ToString());
            stringValues.Should().BeEquivalentTo(expectedNames);
        }

        [Test]
        public void ToDescriptionStringNew()
        {
            Adornment.Start.ToDescriptionString().Should().Be("start");
            Align.Inherit.ToDescriptionString().Should().Be("inherit");
            Breakpoint.Sm.ToDescriptionString().Should().Be("sm");
        }

        [Test]
        [TestCase(typeof(EnumFormTraining), new[] { "Бюджетная оплата", "Внебюджетная оплата" })]
        public void GetEnumDisplayName_Test(Type type, string[] expectedDisplayNames)
        {
            var enumValues = EnumExtensions.GetSafeEnumValues(type).ToArray();
            var displayNames = enumValues
                .Select(e => e.GetType().GetField(e.ToString())
                    ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() as DisplayAttribute)
                .Select(attr => attr?.Name)
                .ToArray();

            displayNames.Should().BeEquivalentTo(expectedDisplayNames);
        }
    }
}
