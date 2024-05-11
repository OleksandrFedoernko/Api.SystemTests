using FluentAssertions;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions;
[Binding]
public class CommonSteps
{
    private readonly ScenarioContext _scenarioContext;
    public CommonSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"requesting_user_type is ""([^""]*)""")]
    public void GivenRequesting_User_TypeIs(string value)
    {
        _scenarioContext.Add("requesting_user_type", value);
    }

    [Given(@"header user_id is ""([^""]*)""")]
    public void GivenHeader_User_IdIs(string value)
    {
        _scenarioContext.Add("user_id", value);
    }

    [Given(@"requesting_user_id is ""([^""]*)""")]
    public void GivenRequesting_User_IdIs(string value)
    {
        _scenarioContext.Add("requesting_user_id", value);
    }

    [Then(@"status code should be (.*)")]
    public void ThenStatusCodeShouldBe(int value)
    {
        int code = _scenarioContext.Get<int>("code");
        code.Should().Be(value);
    }

    [Then(@"error code should be ""([^""]*)""")]
    public void ThenErrorCodeShouldBe(string value)
    {
        var code = _scenarioContext.Get<string>("error_code");
        code.Should().Be(value);
    }
}
