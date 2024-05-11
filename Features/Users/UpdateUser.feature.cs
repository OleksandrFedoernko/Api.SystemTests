﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace VismaIdella.Vips.TaskManagement.Api.SystemTests.Features.Users
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class UpdateUserByIdFeature : object, Xunit.IClassFixture<UpdateUserByIdFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "UpdateUser.feature"
#line hidden
        
        public UpdateUserByIdFeature(UpdateUserByIdFeature.FixtureData fixtureData, VismaIdella_Vips_TaskManagement_Api_SystemTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Users", "UpdateUserById", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
#line 4
 testRunner.Given("header user id which will be used for updating user is \"id\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 5
 testRunner.And("requesting user id for updating user is \"testId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 6
 testRunner.And("requesting_user_type which will be created  for upd \"BACKOFFICE_PROFESSIONAL\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 7
 testRunner.And("user id which will be created for upd is \"userid\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 8
 testRunner.And("user name which will be created for upd is \"Name\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 9
 testRunner.When("I send POST user request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Then("I save user id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Update user using allowed user types")]
        [Xunit.TraitAttribute("FeatureTitle", "UpdateUserById")]
        [Xunit.TraitAttribute("Description", "Update user using allowed user types")]
        [Xunit.InlineDataAttribute("updname", "upddesc", "BACKOFFICE_PROFESSIONAL", new string[0])]
        [Xunit.InlineDataAttribute("updname", "", "BACKOFFICE_PROFESSIONAL", new string[0])]
        [Xunit.InlineDataAttribute("updname", "upddesc", "CLIENT_SYSTEM", new string[0])]
        public void UpdateUserUsingAllowedUserTypes(string name, string description, string requesting_User_Type, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("description", description);
            argumentsOfScenario.Add("requesting_user_type", requesting_User_Type);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update user using allowed user types", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 12
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 13
 testRunner.Given("user id which will be used for updating user is \"userid417\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 14
 testRunner.And(string.Format("user name which will be used for updating user is {0}", name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 15
 testRunner.And(string.Format("user icon which will be used for updating user is {0}", description), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 16
 testRunner.And(string.Format("requesting user type for updating user is {0}", requesting_User_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 17
 testRunner.When("update user request is sent", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 18
 testRunner.Then("status code should be 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 19
 testRunner.And(string.Format("response body from update user should be {0}, {1}", name, description), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Update user using forbidden user types")]
        [Xunit.TraitAttribute("FeatureTitle", "UpdateUserById")]
        [Xunit.TraitAttribute("Description", "Update user using forbidden user types")]
        [Xunit.InlineDataAttribute("PARTICIPANT", "Name", new string[0])]
        [Xunit.InlineDataAttribute("EMPLOYER", "Name", new string[0])]
        public void UpdateUserUsingForbiddenUserTypes(string requesting_User_Type, string name, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("requesting_user_type", requesting_User_Type);
            argumentsOfScenario.Add("name", name);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update user using forbidden user types", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 26
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 27
 testRunner.Given("user id which will be used for updating user is \"userid417\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 28
 testRunner.And(string.Format("requesting user type for updating user is {0}", requesting_User_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 29
 testRunner.And(string.Format("user name which will be used for updating user is {0}", name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 30
 testRunner.When("update user request is sent", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 31
 testRunner.Then("status code should be 403", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
 testRunner.And("forbidden request message from update user request should have text \"Access to th" +
                        "is resource is denied.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 33
 testRunner.And("error code should be \"INVALID_ACCESS_RIGHTS\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Update user by inserting wrong data")]
        [Xunit.TraitAttribute("FeatureTitle", "UpdateUserById")]
        [Xunit.TraitAttribute("Description", "Update user by inserting wrong data")]
        [Xunit.InlineDataAttribute("", "testId", "BACKOFFICE_PROFESSIONAL", "id", "\'User Name\' must not be empty.", "name", new string[0])]
        [Xunit.InlineDataAttribute("name", "", "BACKOFFICE_PROFESSIONAL", "id", "Field cannot be empty.", "requesting_user_id", new string[0])]
        [Xunit.InlineDataAttribute("name", "testId", "", "id", "Field cannot be empty.", "requesting_user_type", new string[0])]
        [Xunit.InlineDataAttribute("name", "testId", "BACKOFFICE_PROFESSIONAL", "", "Field cannot be empty.", "user_id", new string[0])]
        [Xunit.InlineDataAttribute("name", "testId", "BACKOFFICE_PROFESSIONAL", "useriddoesntexist", "User with this ID doesnt exist.", "user_id", new string[0])]
        public void UpdateUserByInsertingWrongData(string name, string requesting_User_Id, string requesting_User_Type, string header_User_Id, string message, string field, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("requesting_user_id", requesting_User_Id);
            argumentsOfScenario.Add("requesting_user_type", requesting_User_Type);
            argumentsOfScenario.Add("header_user_id", header_User_Id);
            argumentsOfScenario.Add("message", message);
            argumentsOfScenario.Add("field", field);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update user by inserting wrong data", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 39
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 40
 testRunner.Given("user id which will be used for updating user is \"userid417\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 41
 testRunner.And(string.Format("user name which will be used for updating user is {0}", name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 42
 testRunner.And(string.Format("bad requesting user id for updating user is {0}", requesting_User_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 43
 testRunner.And(string.Format("requesting user type for updating user is {0}", requesting_User_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 44
 testRunner.And(string.Format("bad header user id which will be used for updating user is {0}", header_User_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 45
 testRunner.When("update user request is sent", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 46
 testRunner.Then("status code should be 400", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 47
 testRunner.And(string.Format("bad request message from update user request should have text {0} in the field {1" +
                            "}", message, field), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 48
 testRunner.And("error code should be \"INVALID_MODEL_RECIEVED_BAD_REQUEST\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Update user by id that doesn\'t exist", Skip="Ignored")]
        [Xunit.TraitAttribute("FeatureTitle", "UpdateUserById")]
        [Xunit.TraitAttribute("Description", "Update user by id that doesn\'t exist")]
        [Xunit.InlineDataAttribute("BACKOFFICE_PROFESSIONAL", new string[0])]
        public void UpdateUserByIdThatDoesntExist(string requesting_User_Type, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("requesting_user_type", requesting_User_Type);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update user by id that doesn\'t exist", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 57
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 58
 testRunner.Given("user id which will be used for updating user is \"doesntexist\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 59
 testRunner.And(string.Format("requesting user type for updating user is {0}", requesting_User_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 60
 testRunner.When("update user request is sent", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 61
 testRunner.Then("status code should be 404", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 62
 testRunner.And("not found request message from update user request should have text \"Resource Not" +
                        " Found\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 63
 testRunner.And("error code should be \"INVALID_RESOURCE_REQUESTED\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                UpdateUserByIdFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                UpdateUserByIdFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
