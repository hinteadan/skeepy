using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API;
using FluentAssertions;

namespace H.Skeepy.Testicles.API
{
    [TestClass]
    public class TemplateParserOperations
    {
        [TestMethod]
        public void TemplateParser_LoadsAndReplacesTemplateTagsWithEmptyStringsIfNoReplacementIsGiven()
        {
            new TemplateParser("@{Test}").Compile().Should().Be(string.Empty);
            new TemplateParser("@{Test}bla bla@{blabla}").Compile().Should().Be("bla bla");
        }

        [TestMethod]
        public void TemplateParser_CorrectlyReplacesTags()
        {
            new TemplateParser("My name is @{Name}").Compile(("Name", "Hintee")).Should().Be("My name is Hintee");
            new TemplateParser("My name is @{FirstName} @{LastName}").Compile(("FirstName", "Hintea"), ("LastName", "Dan")).Should().Be("My name is Hintea Dan");
        }
    }
}
