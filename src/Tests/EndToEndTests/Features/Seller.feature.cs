﻿//// ------------------------------------------------------------------------------
////  <auto-generated>
////      This code was generated by SpecFlow (https://www.specflow.org/).
////      SpecFlow Version:3.9.0.0
////      SpecFlow Generator Version:3.9.0.0
//// 
////      Changes to this file may cause incorrect behavior and will be lost if
////      the code is regenerated.
////  </auto-generated>
//// ------------------------------------------------------------------------------
//#region Designer generated code
//#pragma warning disable
//namespace PriceAnalytics.EndToEndTests.Features
//{
//    using TechTalk.SpecFlow;
//    using System;
//    using System.Linq;
    
    
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
//    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
//    [NUnit.Framework.TestFixtureAttribute()]
//    [NUnit.Framework.DescriptionAttribute("seller")]
//    public partial class SellerFeature
//    {
        
//        private TechTalk.SpecFlow.ITestRunner testRunner;
        
//        private string[] _featureTags = ((string[])(null));
        
//#line 1 "Seller.feature"
//#line hidden
        
//        [NUnit.Framework.OneTimeSetUpAttribute()]
//        public virtual void FeatureSetup()
//        {
//            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
//            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "seller", "A short summary of the feature", ProgrammingLanguage.CSharp, ((string[])(null)));
//            testRunner.OnFeatureStart(featureInfo);
//        }
        
//        [NUnit.Framework.OneTimeTearDownAttribute()]
//        public virtual void FeatureTearDown()
//        {
//            testRunner.OnFeatureEnd();
//            testRunner = null;
//        }
        
//        [NUnit.Framework.SetUpAttribute()]
//        public virtual void TestInitialize()
//        {
//        }
        
//        [NUnit.Framework.TearDownAttribute()]
//        public virtual void TestTearDown()
//        {
//            testRunner.OnScenarioEnd();
//        }
        
//        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
//        {
//            testRunner.OnScenarioInitialize(scenarioInfo);
//            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
//        }
        
//        public virtual void ScenarioStart()
//        {
//            testRunner.OnScenarioStart();
//        }
        
//        public virtual void ScenarioCleanup()
//        {
//            testRunner.CollectScenarioErrors();
//        }
        
//        [NUnit.Framework.TestAttribute()]
//        [NUnit.Framework.DescriptionAttribute("Add seller")]
//        public virtual void AddSeller()
//        {
//            string[] tagsOfScenario = ((string[])(null));
//            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
//            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add seller", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
//#line 5
//this.ScenarioInitialize(scenarioInfo);
//#line hidden
//            bool isScenarioIgnored = default(bool);
//            bool isFeatureIgnored = default(bool);
//            if ((tagsOfScenario != null))
//            {
//                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
//            }
//            if ((this._featureTags != null))
//            {
//                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
//            }
//            if ((isScenarioIgnored || isFeatureIgnored))
//            {
//                testRunner.SkipScenario();
//            }
//            else
//            {
//                this.ScenarioStart();
//#line 6
// testRunner.Given("seller with name \"seller11\" and description \"seller11 description\" is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
//#line hidden
//#line 7
// testRunner.When("get seller", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
//#line hidden
//#line 8
// testRunner.Then("the seller get result should be 200 and name \"seller11\" and description \"seller11" +
//                        " description\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
//#line hidden
//#line 9
// testRunner.Then("delete seller", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
//#line hidden
//            }
//            this.ScenarioCleanup();
//        }
        
//        [NUnit.Framework.TestAttribute()]
//        [NUnit.Framework.DescriptionAttribute("Update seller")]
//        public virtual void UpdateSeller()
//        {
//            string[] tagsOfScenario = ((string[])(null));
//            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
//            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update seller", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
//#line 11
//this.ScenarioInitialize(scenarioInfo);
//#line hidden
//            bool isScenarioIgnored = default(bool);
//            bool isFeatureIgnored = default(bool);
//            if ((tagsOfScenario != null))
//            {
//                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
//            }
//            if ((this._featureTags != null))
//            {
//                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
//            }
//            if ((isScenarioIgnored || isFeatureIgnored))
//            {
//                testRunner.SkipScenario();
//            }
//            else
//            {
//                this.ScenarioStart();
//#line 12
//    testRunner.Given("seller with name \"seller1\" and description \"seller1 description\" is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
//#line hidden
//#line 13
// testRunner.Given("seller with name \"seller2\" and description \"seller2 description\" is updated", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
//#line hidden
//#line 14
// testRunner.When("get seller", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
//#line hidden
//#line 15
// testRunner.Then("the seller get result should be 200 and name \"seller2\" and description \"seller2 d" +
//                        "escription\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
//#line hidden
//#line 16
// testRunner.Then("delete seller", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
//#line hidden
//            }
//            this.ScenarioCleanup();
//        }
        
//        [NUnit.Framework.TestAttribute()]
//        [NUnit.Framework.DescriptionAttribute("Delete seller")]
//        public virtual void DeleteSeller()
//        {
//            string[] tagsOfScenario = ((string[])(null));
//            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
//            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete seller", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
//#line 18
//this.ScenarioInitialize(scenarioInfo);
//#line hidden
//            bool isScenarioIgnored = default(bool);
//            bool isFeatureIgnored = default(bool);
//            if ((tagsOfScenario != null))
//            {
//                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
//            }
//            if ((this._featureTags != null))
//            {
//                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
//            }
//            if ((isScenarioIgnored || isFeatureIgnored))
//            {
//                testRunner.SkipScenario();
//            }
//            else
//            {
//                this.ScenarioStart();
//#line 19
//    testRunner.Given("seller with name \"seller3\" and description \"seller3 description\" is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
//#line hidden
//#line 20
// testRunner.Given("delete seller", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
//#line hidden
//#line 21
// testRunner.When("get seller", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
//#line hidden
//#line 22
// testRunner.Then("the seller delete result should be 404 after delete", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
//#line hidden
//            }
//            this.ScenarioCleanup();
//        }
//    }
//}
//#pragma warning restore
//#endregion
