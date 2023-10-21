using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace HangFireSample.Controllers;

[ApiController]
[Route("api")]
public class HomeController : Controller
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<HomeController> _logger;
    private readonly IRecurringJobManager _recurringJobManager;

    public HomeController(IBackgroundJobClient backgroundJobClient,
        ILogger<HomeController> logger,
        IRecurringJobManager recurringJobManager)
	{
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
        _recurringJobManager = recurringJobManager;
    }
    [HttpPost]
    [Route("welcome")]
    public IActionResult Welcome(string userName)
    {
        var jobId = _backgroundJobClient.Enqueue(() => SendWelcomeMail(userName));
        //var jobId = _backgroundJobClient.Schedule(() => SendWelcomeMail(userName), TimeSpan.FromMinutes(10));
        //var jobId = _backgroundJobClient.Schedule(() => SendWelcomeMail(userName), DateTimeOffset(dateAndTime));
        return Ok($"Job Id {jobId} Completed. Welcome Mail Sent!");
    }
    [HttpPost]
    [Route("BackUp")]
    public IActionResult BackUp(string userName)
    {
        _recurringJobManager.AddOrUpdate("test", () => BackUpDataBase(), Cron.Weekly);
        return Ok();
    }
    public void SendWelcomeMail(string userName)
    {
        //Logic to Mail the user
        _logger.LogInformation($"Welcome to our application, {userName}");
    }
    public void BackUpDataBase()
    {
        // ...
    }
    #region helpers
    /*
     ***
     
_recurringJobManager.AddOrUpdate("test", () => job , Cron.Minutely); هر یک دقیقه یکبار اجرا میشود
هر ساعت (Cron.Hourly) :
این Cron هر یک ساعت یکبار و بصورت پیش‌فرض در دقیقه اول هر ساعت اجرا میشود. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Hourly);
اما میتوانید یک ورودی دقیقه به آن بدهید که در اینصورت در N اُمین دقیقه از هر ساعت اجرا شود. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Hourly(10));
هر روز (Cron.Daily) :
این Cron بصورت روزانه و در حالت پیشفرض، در اولین ساعت و اولین دقیقه‌ی هر روز اجرا خواهد شد. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Daily);
در حالتی دیگر میتوانید ورودی ساعت و دقیقه را به آن بدهید تا در ساعت و دقیقه‌ای مشخص، در هر روز اجرا شود. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Daily(3,10));
هر هفته (Cron.Weekly) :
این Cron هفتگی است. بصورت پیشفرض هر هفته، شنبه در اولین ساعت و در اولین دقیقه، اجرا میشود. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Weekly);
در حالتی دیگر چندمین روز هفته و ساعت و دقیقه مشخصی را در ورودی میگیرد و حول آن میچرخد. 
_recurringJobManager.AddOrUpdate("test", () => Job,Cron.Weekly(DayOfWeek.Monday,3,10));
هر ماه (Cron.Monthly) :
این Cron بصورت ماهانه اولین روز ماه در اولین ساعت روز و در اولین دقیقه ساعت، زمانبندی خود را اعمال میکند. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Monthly);
و در صورت دادن ورودی میتوانید زمانبندی آن در چندمین روز ماه، در چه ساعت و دقیقه‌ای را نیز تنظیم کنید. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Monthly(10,3,10));
هر سال (Cron.Yearly) :
و در نهایت این Cron بصورت سالانه و در اولین ماه، روز، ساعت و دقیقه هر سال، وظیفه خود را انجام خواهد داد. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Yearly);
که این‌هم مانند بقیه، ورودی‌هایی دریافت میکند که به ترتیب شامل ماه، روز، ساعت و دقیقه است. 
_recurringJobManager.AddOrUpdate("test", () => Job, Cron.Yearly(2,4,3,10));
     ***
     */
    #endregion



    ///<summary>
    /// samples are from https://www.dntips.ir/post/3340/%D8%A2%D9%85%D9%88%D8%B2%D8%B4-%D8%B2%D9%85%D8%A7%D9%86%D8%A8%D9%86%D8%AF%DB%8C-%DA%A9%D8%A7%D8%B1%D9%87%D8%A7-%D8%A8%D8%A7-hangfire-%D8%AF%D8%B1-asp-net-core
    ///</summary>
}
